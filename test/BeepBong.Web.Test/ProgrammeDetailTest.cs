using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BeepBong.Domain.Models;
using BeepBong.DataAccess;
using BeepBong.Web.Pages.Programmes;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using BeepBong.Application.ViewModels;

namespace BeepBong.Web.Test
{
    public class ProgrammeDetailTest
    {
        public DetailsModel model;

        [Fact]
        public async Task ListProgrammeAndTrackAsync()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            Programme p = new Programme() {
                Name = "test"
            };
            TrackList tl = new TrackList() {
                Name = "Test List"
            };
            Track t = new Track() {
                Name = "Track test"
            };
            ProgrammeTrackList ptl = new ProgrammeTrackList() {
                ProgrammeId = p.ProgrammeId,
                TrackListId = tl.TrackListId
            };

            tl.Tracks.Add(t);
            ptl.Programme = p;
            ptl.TrackList = tl;

            try {
                var options = new DbContextOptionsBuilder<BeepBongContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new BeepBongContext(options))
                {
                    // Create the schema in the database
                    context.Database.EnsureCreated();

                    context.ProgrammeTrackLists.Add(ptl);
                    context.SaveChanges();
                }

                using (var context = new BeepBongContext(options))
                {
                    Assert.NotEmpty(context.Programmes);
                    Assert.NotEmpty(context.TrackLists);
                    Assert.NotEmpty(context.Tracks);
                    Assert.NotEmpty(context.ProgrammeTrackLists);
                }

                using (var context = new BeepBongContext(options))
                {
                    Guid id = context.Programmes.FirstOrDefault().ProgrammeId;

                    model = new DetailsModel(context);

                    await model.OnGetAsync(id);

                    Assert.NotNull(model.Programme);
                    Assert.IsType<ProgrammeDetailViewModel>(model.Programme);
                    Assert.NotEmpty(model.Programme.TrackLists);
                    Assert.IsType<SimpleTrackList>(model.Programme.TrackLists.FirstOrDefault());
                    Assert.NotEmpty(model.Programme.TrackLists.FirstOrDefault()?.Tracks);
                    Assert.IsType<SimpleTrack>(model.Programme.TrackLists.FirstOrDefault()?.Tracks.FirstOrDefault());
                }
            }
            finally {
                connection.Close();
            }
        }
    }
}