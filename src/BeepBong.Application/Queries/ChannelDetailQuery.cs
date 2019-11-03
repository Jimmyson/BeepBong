using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class ChannelDetailQuery : IQuery<ChannelDetailViewModel>
    {
        private readonly BeepBongContext _context;

        public ChannelDetailQuery(BeepBongContext context) => _context = context;

        public IQueryable<ChannelDetailViewModel> GetQuery(Guid? channelId)
        {
            return _context.Channels
                .Where(c => c.ChannelId == channelId.Value)
                .Select(c => new ChannelDetailViewModel() {
                    ChannelId = c.ChannelId,
                    Name = c.Name,
                    Opened = c.Opened,
                    Closed = c.Closed,
                    BroadcasterId = c.Broadcaster.BroadcasterId,
                    BroadcasterName = c.Broadcaster.Name,
                    Programmes = c.Programmes.Select(p => new SimpleProgramme() {
                        ProgrammeId = p.ProgrammeId,
                        Name = p.Name,
                        Year = (p.AirDate.HasValue) ? p.AirDate.Value.Year.ToString() : null
                    }).ToList()
                });
        }
    }
}