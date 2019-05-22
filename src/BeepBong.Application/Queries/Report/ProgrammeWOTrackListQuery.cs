using BeepBong.Application.ViewModels;
using BeepBong.Application.ViewModels.Report;
using BeepBong.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace BeepBong.Application.Queries.Report
{
    public class ProgrammeWOTrackListQuery
    {
        private readonly BeepBongContext _context;

        public ProgrammeWOTrackListQuery(BeepBongContext context) => _context = context;

        public IQueryable<ProgrammeWOTrackListViewModel> GetQuery()
        {
            return _context.Programmes
                .Where(p => !p.ProgrammeTrackLists.Any())
                .Select(p => new ProgrammeWOTrackListViewModel() {
                    ProgrammeId = p.ProgrammeId,
                    Name = p.Name,
                    Year = p.AirDate.Year.ToString(),
                    ChannelName = p.Channel.Name
                })
                .OrderBy(ls => ls.Name)
                .ThenBy(ls => ls.Year)
                .AsQueryable();
        }
    }
}