using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BeepBong.Domain.Models;
using BeepBong.DataAccess;
using BeepBong.Web.Pages.Programmes;
using BeepBong.Web.ViewModels;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

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
            Track t = new Track() {
                Name = "Track test"
            };
            p.Tracks.Add(t);

            try {
                var options = new DbContextOptionsBuilder<BeepBongContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new BeepBongContext(options))
                {
                    // Create the schema in the database
                    context.Database.EnsureCreated();

                    context.Programmes.Add(p);
                    context.SaveChanges();
                }

                using (var context = new BeepBongContext(options))
                {
                    Guid id = context.Programmes.FirstOrDefault().ProgrammeId;

                    model = new DetailsModel(context);

                    await model.OnGetAsync(id);

                    Assert.NotNull(model.Programme);
                    Assert.IsType<ProgrammeViewModel>(model.Programme);
                    Assert.NotEmpty(model.Programme.Tracks);
                    Assert.IsType<TrackViewModel>(model.Programme.Tracks.FirstOrDefault());
                }
            }
            finally {
                connection.Close();
            }
        }
    }
}