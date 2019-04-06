using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BeepBong.Domain.Models;
using BeepBong.DataAccess;
using BeepBong.Web.Pages.Tracks;
using BeepBong.Web.ViewModels;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Web.Test
{
    public class TrackDetailTest
    {
        public DetailsModel model;

        [Fact]
        public async Task ListTrackAndSampleAsync()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            Programme p = new Programme() {
                Name = "Test"
            };
            Track t = new Track() {
                Name = "Test Track"
            };
            Sample s = new Sample() {
                SampleCount = 127362
            };
            t.Samples.Add(s);
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
                    Guid id = context.Tracks.FirstOrDefault().TrackId;

                    model = new DetailsModel(context);

                    await model.OnGetAsync(id);

                    Assert.NotNull(model.Track);
                    Assert.IsType<TrackSampleListViewModel>(model.Track);
                    Assert.NotEmpty(model.Track.Samples);
                    Assert.IsType<Sample>(model.Track.Samples.FirstOrDefault());
                }
            }
            finally {
                connection.Close();
            }
        }
        
        [Fact]
        public async Task ListTrackAndNoSampleLibraryAsync()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            Programme p = new Programme() {
                Name = "Test",
                IsLibraryMusic = true
            };
            Track t = new Track() {
                Name = "Test Track"
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
                    Guid id = context.Tracks.FirstOrDefault().TrackId;

                    model = new DetailsModel(context);

                    await model.OnGetAsync(id);

                    Assert.NotNull(model.Track);
                    Assert.IsType<TrackSampleListViewModel>(model.Track);
                    Assert.Empty(model.Track.Samples);
                }
            }
            finally {
                connection.Close();
            }
        }

        [Fact]
        public async Task ListTrackAndSampleLibraryAsync()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            Programme p = new Programme() {
                Name = "Test",
                IsLibraryMusic = true
            };
            Track t = new Track() {
                Name = "Test Track"
            };
            Sample s = new Sample() {
                SampleCount = 127362
            };
            t.Samples.Add(s);
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
                    Guid id = context.Tracks.FirstOrDefault().TrackId;

                    model = new DetailsModel(context);

                    await model.OnGetAsync(id);

                    Assert.NotNull(model.Track);
                    Assert.IsType<TrackSampleListViewModel>(model.Track);
                    Assert.Empty(model.Track.Samples);

                    Assert.NotEmpty(context.Samples);
                }
            }
            finally {
                connection.Close();
            }
        }
    }
}