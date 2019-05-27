using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.Application.ViewModels.Report;
using BeepBong.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeepBong.Application.Queries.Report
{
    public class ProgrammeWOTrackListQuery : IQuery<ProgrammeWOTrackListViewModel>
    {
        private readonly BeepBongContext _context;

        public ProgrammeWOTrackListQuery(BeepBongContext context) => _context = context;

        public IQueryable<ProgrammeWOTrackListViewModel> GetQuery(Guid? id)
        {
            return _context.Programmes
                .Where(p => !p.ProgrammeTrackLists.Any())
                .Select(p => new ProgrammeWOTrackListViewModel() {
                    ProgrammeId = p.ProgrammeId,
                    Name = p.Name,
                    Year = (p.AirDate.HasValue) ? p.AirDate.Value.Year.ToString() : null,
                    ChannelName = p.Channel.Name
                })
                .OrderBy(ls => ls.Name)
                .ThenBy(ls => ls.Year)
                .AsQueryable();
        }
    }
}