using System;
using System.Linq;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class ChannelEditQuery
    {
        private readonly BeepBongContext _context;

        public ChannelEditQuery(BeepBongContext context) => _context = context;
        
        public IQueryable<ChannelEditViewModel> GetQuery(Guid channelId)
        {
            return _context.Channels
                .Where(c => c.ChannelId == channelId)
                .Select(c => new ChannelEditViewModel() {
                    ChannelId = channelId,
                    Name = c.Name,
                    Commencement = c.Commencement,
                    Closed = c.Closed,
                    BroadcasterId = c.BroadcasterId
                });
        }
    }
}