using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class ChannelEditQuery : IQuery<ChannelEditViewModel>, IExists<ChannelEditViewModel>
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
                    Opened = c.Opened,
                    Closed = c.Closed,
                    BroadcasterId = c.BroadcasterId
                });
        }

        public bool Exists(ChannelEditViewModel model)
        {
            return _context.Channels.Any(
                c => c.ChannelId != model.ChannelId
                && string.Equals(c.Name, model.Name, StringComparison.OrdinalIgnoreCase)
                && c.BroadcasterId == model.BroadcasterId);
        }
    }
}