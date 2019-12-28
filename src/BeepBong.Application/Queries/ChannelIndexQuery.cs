using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class ChannelIndexQuery : IQuery<ChannelIndexViewModel>
    {
        private readonly BeepBongContext _context;

        public ChannelIndexQuery(BeepBongContext context) => _context = context;

        public IQueryable<ChannelIndexViewModel> GetQuery(Guid? id)
        {
            return _context.Channels
                .WhereIf(id != null, c => c.BroadcasterId == id.Value) // Filter on Broadcaster ID
                .OrderBy(c => c.Name)
                .Select(c => new ChannelIndexViewModel() {
                    ChannelId = c.ChannelId,
                    Name = c.Name,
                    Opened = c.Opened.HasValue ? c.Opened.Value.ToLongDateString() : null,
                    Closed = c.Closed.HasValue ? c.Closed.Value.ToLongDateString() : null,
                    BroadcasterName = c.Broadcaster.Name,
                    ProgrammeCount = c.Programmes.Count
                });
        }
    }
}