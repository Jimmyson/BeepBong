using System;
using System.Linq;
using Xunit;
using BeepBong.Domain.Models;
using BeepBong.DataAccess;
using BeepBong.Web.Pages.Programmes;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Web.Test
{
    public class ProgrammeIndexTest
    {
        private IndexModel model;

        [Fact]
        public async System.Threading.Tasks.Task ListProgrammeWithTrackCountAsync()
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
                    model = new IndexModel(context);

                    await model.OnGetAsync();

                    Assert.NotEmpty(model.Programme);
                    Assert.NotNull(model.Programme.First().TrackCount);
                }
            }
            finally {
                connection.Close();
            }

        }
    }
}
