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
            return _context.ProgrammeTrackLists
                .Include(ptl => ptl.Programme)
                .Include(ptl => ptl.TrackList)
                .Where(ptl => ptl.ProgrammeId == programmeId)
                .GroupBy(ptl => ptl.Programme, ptl => ptl.TrackList, (key, g) => new {Programme = key, TrackList = g.ToList()})
                .Select(ptl => new ProgrammeDetailViewModel() {
                    ProgrammeId = ptl.Programme.ProgrammeId,
                    Name = ptl.Programme.Name,
                    AirDate = ptl.Programme.AirDate,
                    ChannelName = (ptl.Programme.Channel != null) ? ptl.Programme.Channel.Name : null,
                    Logo = ptl.Programme.LogoLocation,
                    TrackLists = ptl.TrackList.Select(tl => new SimpleTrackList()
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