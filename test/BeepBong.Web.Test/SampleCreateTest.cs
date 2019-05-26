using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BeepBong.Domain.Models;
using BeepBong.DataAccess;
using BeepBong.Web.Pages.Samples;
// using BeepBong.Web.ViewModels;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using BeepBong.Application.ViewModels;

namespace BeepBong.Web.Test
{
    public class SampleCreateTest
    {
        public UploadModel model;

        [Fact]
        public async Task ConcatTrackNameAndSubtitleAsync() {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            TrackList tl = new TrackList() {
                Name = "test"
            };
            Track t = new Track() {
                Name = "Track test",
                Variant = "Special"
            };
            tl.Tracks.Add(t);

            try {
                var options = new DbContextOptionsBuilder<BeepBongContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new BeepBongContext(options))
                {
                    // Create the schema in the database
                    context.Database.EnsureCreated();

                    context.TrackLists.Add(tl);
                    context.SaveChanges();
                }

                SampleCreateViewModel s = new SampleCreateViewModel() {
                    SampleCount = "1721",
                    SampleRate = "271",
                    AudioChannelCount = "2",
                    Codec = "MP3",
                    BitRate = "128",
                    BitDepth = "24",
                    BitRateMode = BitRateModeEnum.CBR,
                    Compression = CompressionEnum.Lossy
                };

                using (var context = new BeepBongContext(options))
                {
                    Guid id = context.Tracks.FirstOrDefault().TrackId;
                    model = new UploadModel(context);

                    s.TrackId = id;
                    model.Sample = s;

                    await model.OnPostAsync();

                    Assert.NotEmpty(context.Samples);

                    context.Database.EnsureDeleted();
                }
            }
            finally {
                connection.Close();
            }
        }
    }
}