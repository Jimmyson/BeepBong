using System;
using System.Linq;
using Xunit;
using BeepBong.Domain.Models;
using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.DataAccess.Test
{
    public class ShadowPropertiesTest
    {
        [Fact]
        public void CreateRecordTest()
        {
            var options = InMemoryContext.ContextGenerator("CreateRecordTest");

            using (var context = new BeepBongContext(options)) {
                context.Programmes.Add(new Programme()
                {
                    Name = "Hello World",
                    Year = "2000"
                });
                context.SaveChanges();
            };

            using (var context = new BeepBongContext(options)) {
                var result = context.Programmes.OrderBy(p => EF.Property<DateTime>(p, "Created")).ToList();

                Assert.Single(result);

                var createdValue = context.Entry(result.First()).Property("Created").CurrentValue;
                var lastModifiedValue = context.Entry(result.First()).Property("LastModified").CurrentValue;

                Assert.NotNull(createdValue);
                Assert.Null(lastModifiedValue);
            }
        }

        
        [Fact]
        public void UpdateRecordTest()
        {
            var options = InMemoryContext.ContextGenerator("UpdateRecordTest");

            using (var context = new BeepBongContext(options)) {
                context.Programmes.Add(new Programme()
                {
                    Name = "Hello World",
                    Year = "2000"
                });
                context.SaveChanges();
            };

            using (var context = new BeepBongContext(options)) {
                var p = context.Programmes.First();

                p.Channel = "Sample Channel";

                context.SaveChanges();
            };

            using (var context = new BeepBongContext(options)) {
                var result = context.Programmes.OrderBy(p => EF.Property<DateTime>(p, "Created")).ToList();

                Assert.Single(result);

                var createdValue = context.Entry(result.First()).Property("Created").CurrentValue;
                var lastModifiedValue = context.Entry(result.First()).Property("LastModified").CurrentValue;

                Assert.NotNull(createdValue);
                Assert.NotNull(lastModifiedValue);
            }
        }
    }
}