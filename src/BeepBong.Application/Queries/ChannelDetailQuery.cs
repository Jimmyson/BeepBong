using System;
using System.Linq;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class ChannelDetailQuery
    {
        private readonly BeepBongContext _context;

        public ChannelDetailQuery(BeepBongContext context) => _context = context;
        
        public IQueryable<ChannelDetailViewModel> GetQuery(Guid channelId)
        {
            return _context.Channels
                .Where(c => c.ChannelId == channelId)
                .Select(c => new ChannelDetailViewModel() {
                    ChannelId = c.ChannelId,
                    Name = c.Name,
                    Commencement = c.Commencement,
                    Closed = c.Closed,
                    Programmes = c.Programmes.Select(p => new SimpleProgramme() {
                        Name = p.Name,
                        Year = p.AirDate.ToString("y")
                    }).ToList()
                });
        }
        
    }
}