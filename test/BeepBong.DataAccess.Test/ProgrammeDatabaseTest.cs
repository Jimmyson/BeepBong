using System;
using System.Linq;
using Xunit;
using BeepBong.Domain.Models;
using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.DataAccess.Test
{
    public class ProgrammeDatabaseTest
    {
        [Fact]
        public void ProgrammeTrackSampleCascadeDelete()
        {
            Programme p = new Programme() {
                Name = "Test"
            };
            Track t = new Track() {
                Name = "Track"
            };
            Sample s = new Sample() {
                SampleCount = 12723
            };

            t.Samples.Add(s);
            p.Tracks.Add(t);

            var options = InMemoryContext.ContextGenerator("ProgrammeTrackSampleCascadeDelete");
            
            using (var context = new BeepBongContext(options))
            {
                context.Programmes.Add(p);
                context.SaveChanges();
            }

            using (var context = new BeepBongContext(options))
            {
                Assert.Equal<int>(1, context.Programmes.Count());
                Assert.Equal<int>(1, context.Tracks.Count());
                Assert.Equal<int>(1, context.Samples.Count());

                var programme = context.Programmes
                                .Include(pr => pr.Tracks)
                                .ThenInclude(tr => tr.Samples).First();

                context.Programmes.Remove(programme);
                context.SaveChanges();
            }

            using (var context = new BeepBongContext(options))
            {
                Assert.Empty(context.Programmes);
                Assert.Empty(context.Tracks);
                Assert.Empty(context.Samples);
            }
        }
    }
}