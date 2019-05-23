// using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BeepBong.Domain.Models;
using BeepBong.DataAccess;
using BeepBong.Web.Pages.Programmes;
// using BeepBong.Web.ViewModels;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using BeepBong.Application.ViewModels;

namespace BeepBong.Web.Test
{
    public class ProgrammeIndexTest
    {
        private IndexModel model;

        [Fact]
        public async Task ListProgrammeWithTrackCountAsync()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            Programme p = new Programme() {
                Name = "test"
            };

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

                    await model.OnGetAsync(null);

                    Assert.NotEmpty(model.Programme);
                    Assert.IsType<ProgrammeIndexViewModel>(model.Programme.FirstOrDefault());
                }
            }
            finally {
                connection.Close();
            }
        }
    }
}
