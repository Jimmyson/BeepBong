using System;
using System.Linq;
using Xunit;
using BeepBong.Domain.Models;
using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.DataAccess.Test
{
    public class TrackDatabaseTest
    {
        [Fact]
        public void TrackDeleteSamplesWithInclude()
        {
            Sample s1 = new Sample() {
                SampleCount = 10232
            };
            Sample s2 = new Sample() {
                SampleCount = 182
            };

            Track t = new Track() {
                Name = "Track"
            };
            t.Samples.Add(s1);
            t.Samples.Add(s2);

            var options = InMemoryContext.ContextGenerator("TrackDeleteSamplesWithInclude");
            
            using (var context = new BeepBongContext(options))
            {
                context.Tracks.Add(t);
                context.SaveChanges();
            }

            using (var context = new BeepBongContext(options))
            {
                Assert.Equal<int>(1, context.Tracks.Count());
                Assert.Equal<int>(2, context.Samples.Count());

                var tracks = context.Tracks
                                .Include(tr => tr.Samples).First();

                context.Tracks.Remove(tracks);
                context.SaveChanges();
            }

            using (var context = new BeepBongContext(options))
            {
                Assert.Empty(context.Tracks);
                Assert.Empty(context.Samples);
            }
        }

        // This test fails with the InMemory tester as Cascade Delete does not work unless
        // you explicitly "Include" the children 
        //
        // [Fact]
        // public void TrackDeleteSamplesWithoutInclude()
        // {
        //     Sample s1 = new Sample() {
        //         SampleCount = 10232
        //     };
        //     Sample s2 = new Sample() {
        //         SampleCount = 182
        //     };

        //     Track t = new Track() {
        //         Name = "Track"
        //     };
        //     t.Samples.Add(s1);
        //     t.Samples.Add(s2);

        //     var options = InMemoryContext.ContextGenerator("TrackDeleteSamplesWithoutInclude");
            
        //     using (var context = new BeepBongContext(options))
        //     {
        //         context.Tracks.Add(t);
        //         context.SaveChanges();
        //     }

        //     using (var context = new BeepBongContext(options))
        //     {
        //         Assert.Equal<int>(1, context.Tracks.Count());
        //         Assert.Equal<int>(2, context.Samples.Count());

        //         var tracks = context.Tracks.First();

        //         context.Tracks.Remove(tracks);
        //         context.SaveChanges();
        //     }

        //     using (var context = new BeepBongContext(options))
        //     {
        //         var result = context.Samples.ToList();

        //         Assert.Equal<int>(0, context.Tracks.Count());
        //         Assert.Equal<int>(0, context.Samples.Count());
        //     }
        // }
    }
}