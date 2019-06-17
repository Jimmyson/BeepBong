// using System;
using System.Linq;
using Xunit;
using BeepBong.Domain.Models;
// using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.DataAccess.Test
{
    public class ProgrammeDatabaseTest
    {
        // Issue testing with InMemory and SQLite tester
        // [Fact]
        // public void ProgrammeYearFiveCharFail()
        // {
        //     Programme p = new Programme() {
        //         Year = "20183"
        //     };

        //     var options = InMemoryContext.ContextGenerator("ProgrammeYearFiveCharFail");

        //     using (var context = new BeepBongContext(options))
        //     {
        //         context.Programmes.Add(p);
        //         context.SaveChanges();
        //     }

        //     using (var context = new BeepBongContext(options)) {
        //         Assert.Single(context.Programmes);

        //         Programme item = context.Programmes.First();
        //         Assert.Equal("2018", item.Year);
        //     }
        // }

        [Fact]
        public void ProgrammeTrackSampleCascadeDelete()
        {
            TrackList tl = new TrackList() {
                Name = "Test"
            };
            Track t = new Track() {
                Name = "Track"
            };
            Sample s = new Sample() {
                SampleCount = 12723
            };

            t.Samples.Add(s);
            tl.Tracks.Add(t);

            var options = InMemoryContext.ContextGenerator("ProgrammeTrackSampleCascadeDelete");

            using (var context = new BeepBongContext(options))
            {
                context.TrackLists.Add(tl);
                context.SaveChanges();
            }

            using (var context = new BeepBongContext(options))
            {
                Assert.Single(context.TrackLists);
                Assert.Single(context.Tracks);
                Assert.Single(context.Samples);

                var trackList = context.TrackLists
                                .Include(trl => trl.Tracks)
                                .ThenInclude(trl => trl.Samples).First();

                context.TrackLists.Remove(trackList);
                context.SaveChanges();
            }

            using (var context = new BeepBongContext(options))
            {
                Assert.Empty(context.TrackLists);
                Assert.Empty(context.Tracks);
                Assert.Empty(context.Samples);
            }
        }

        [Fact]
        public void ProgrammeRelationshipsRemove()
        {
            Track t = new Track() {
                Name = "Track"
            };
            Programme p = new Programme() {
                Name = "Test"
            };
            TrackList tl = new TrackList() {
                Name = "Record"
            };

            ProgrammeTrackList ptl = new ProgrammeTrackList();

            tl.Tracks.Add(t);
            p.ProgrammeTrackLists.Add(ptl);
            tl.ProgrammeTrackLists.Add(ptl);

            var options = InMemoryContext.ContextGenerator("ProgrammeRelationshipsRemove");

            using (var context = new BeepBongContext(options))
            {
                context.Programmes.Add(p);
                context.TrackLists.Add(tl);
                context.SaveChanges();
            }

            using (var context = new BeepBongContext(options))
            {
                Assert.Single(context.TrackLists);
                Assert.Single(context.Tracks);
                Assert.Single(context.ProgrammeTrackLists);

                Assert.Single(context.Programmes);

                var trackList = context.TrackLists
                                .Include(pr => pr.ProgrammeTrackLists)
                                .Include(pr => pr.Tracks).First();

                context.TrackLists.Remove(trackList);
                context.SaveChanges();
            }

            using (var context = new BeepBongContext(options))
            {
                Assert.Empty(context.TrackLists);
                Assert.Empty(context.Tracks);
                Assert.Empty(context.ProgrammeTrackLists);

                Assert.Single(context.Programmes);
            }
        }
    }
}