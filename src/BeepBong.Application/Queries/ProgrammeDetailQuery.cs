using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeepBong.Application.Queries
{
    public class ProgrammeDetailQuery
    {
        private readonly BeepBongContext _context;

        public ProgrammeDetailQuery(BeepBongContext context) => _context = context;

        public IQueryable<ProgrammeDetailViewModel> GetQuery(Guid programmeId)
        {
            return _context.Programmes
                .Include(p => p.ProgrammeTrackLists)
                .ThenInclude(ptl => ptl.TrackList)
                .ThenInclude(tl => tl.Tracks)
                .Where(p => p.ProgrammeId == programmeId)
                .Select(p => new ProgrammeDetailViewModel() {
                    ProgrammeId = p.ProgrammeId,
                    Name = p.Name,
                    AirDate = (p.AirDate.HasValue) ? p.AirDate.Value.ToShortDateString() : null,
                    ChannelName = (p.Channel != null) ? p.Channel.Name : null,
                    Logo = p.LogoLocation,
                    TrackLists = p.ProgrammeTrackLists.Select(ptl => ptl.TrackList).Select(tl => new SimpleTrackList()
                    {
                        TrackListId = tl.TrackListId,
                        Name = tl.Name,
                        Composer = tl.Composer,
                        Library = tl.Library,
                        Tracks = tl.Tracks.Select(t => new SimpleTrack()
                        {
                            TrackId = t.TrackId,
                            Name = t.Name,
                            Variant = t.Variant,
                            Description = t.Description,
                            SampleCount = t.Samples.Count
                        }).ToList()
                    }).ToList()
                });
        }
    }
}