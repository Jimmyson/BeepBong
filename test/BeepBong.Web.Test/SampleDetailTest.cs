using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BeepBong.Domain.Models;
using BeepBong.DataAccess;
using BeepBong.Web.Pages.Samples;
using BeepBong.Web.ViewModels;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Web.Test
{
    public class SampleDetailTest
    {
        public DetailsModel model;

        [Fact]
        public async Task ConcatTrackNameAndSubtitleAsync() {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            Programme p = new Programme() {
                Name = "test"
            };
            Track t = new Track() {
                Name = "Track test",
                Subtitle = "Special"
            };
            Sample s = new Sample() {
                SampleCount = 287632
            };
            p.Tracks.Add(t);
            t.Samples.Add(s);

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
                    Guid id = context.Samples.FirstOrDefault().SampleId;
                    model = new DetailsModel(context);

                    await model.OnGetAsync(id);

                    Assert.NotNull(model.Sample);
                    Assert.IsType<SampleViewModel>(model.Sample);
                    Assert.Equal("Track test (Special)", model.Sample.TrackName);

                    context.Database.EnsureDeleted();
                }
            }
            finally {
                connection.Close();
            }
        }

        [Fact]
        public async Task ConcatTrackNameNoSubtitleAsync() {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            Programme p = new Programme() {
                Name = "test"
            };
            Track t = new Track() {
                Name = "Track test"
            };
            Sample s = new Sample() {
                SampleCount = 287632
            };
            p.Tracks.Add(t);
            t.Samples.Add(s);

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
                    Guid id = context.Samples.FirstOrDefault().SampleId;
                    model = new DetailsModel(context);

                    await model.OnGetAsync(id);

                    Assert.NotNull(model.Sample);
                    Assert.IsType<SampleViewModel>(model.Sample);
                    Assert.Equal("Track test", model.Sample.TrackName);
                    
                    context.Database.EnsureDeleted();
                }
            }
            finally {
                connection.Close();
            }
        }
    }
}