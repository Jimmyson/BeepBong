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
                Assert.Single(context.Programmes);
                Assert.Single(context.Tracks);
                Assert.Single(context.Samples);

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

        [Fact]
        public void ProgrammeRelationshipsRemove()
        {
            Track t = new Track() {
                Name = "Track"
            };
            Programme p = new Programme() {
                Name = "Test"
            };
            Library l = new Library() {
                AlbumName = "Record"
            };

            LibraryProgramme lp = new LibraryProgramme();

            p.Tracks.Add(t);
            p.LibraryProgrammes.Add(lp);
            l.LibraryProgrammes.Add(lp);

            var options = InMemoryContext.ContextGenerator("ProgrammeRelationshipsRemove");
            
            using (var context = new BeepBongContext(options))
            {
                context.Programmes.Add(p);
                context.Libraries.Add(l);
                context.SaveChanges();
            }

            using (var context = new BeepBongContext(options))
            {
                Assert.Single(context.Programmes);
                Assert.Single(context.Tracks);
                Assert.Single(context.LibraryProgrammes);
                
                Assert.Single(context.Libraries);

                var programme = context.Programmes
                                .Include(pr => pr.Tracks)
                                .Include(pr => pr.LibraryProgrammes).First();

                context.Programmes.Remove(programme);
                context.SaveChanges();
            }

            using (var context = new BeepBongContext(options))
            {
                Assert.Empty(context.Programmes);
                Assert.Empty(context.Tracks);
                Assert.Empty(context.LibraryProgrammes);

                Assert.Single(context.Libraries);
            }
        }
    }
}