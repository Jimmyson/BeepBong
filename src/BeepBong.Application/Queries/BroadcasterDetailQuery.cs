using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class BroadcasterDetailQuery : IQuery<BroadcasterDetailViewModel>
    {
        private readonly BeepBongContext _context;

        public BroadcasterDetailQuery(BeepBongContext context) => _context = context;

        public IQueryable<BroadcasterDetailViewModel> GetQuery(Guid? id)
        {
            return _context.Broadcasters
                .Where(b => b.BroadcasterId == id)
                .Select(b => new BroadcasterDetailViewModel() {
                    BroadcasterId = b.BroadcasterId,
                    Name = b.Name,
                    Country = b.Country,
                    ImageId = b.ImageId,
                    ChannelNames = b.Channels.Select(c => c.Name).ToList()
                });
        }
    }
}