using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class ChannelEditQuery : IQuery<ChannelEditViewModel>
    {
        private readonly BeepBongContext _context;

        public ChannelEditQuery(BeepBongContext context) => _context = context;
        
        public IQueryable<ChannelEditViewModel> GetQuery(Guid? channelId)
        {
            return _context.Channels
                .Where(c => c.ChannelId == channelId.Value)
                .Select(c => new ChannelEditViewModel() {
                    ChannelId = c.ChannelId,
                    Name = c.Name,
                    Commencement = c.Commencement,
                    Closed = c.Closed,
                    BroadcasterId = c.BroadcasterId
                });
        }

        public bool Exists(ChannelEditViewModel model)
        {
            return _context.Channels.Any(
                c => c.ChannelId != model.ChannelId
                && c.Name.ToLower() == model.Name.ToLower()
                && c.BroadcasterId == model.BroadcasterId);
        }
    }
}