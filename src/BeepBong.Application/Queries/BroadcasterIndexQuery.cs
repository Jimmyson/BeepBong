using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class BroadcasterIndexQuery : IQuery<BroadcasterIndexViewModel>
    {
        private readonly BeepBongContext _context;

        public BroadcasterIndexQuery(BeepBongContext context) => _context = context;

        public IQueryable<BroadcasterIndexViewModel> GetQuery(Guid? id)
        {
            return _context.Broadcasters
                .Select(b => new BroadcasterIndexViewModel() {
                    BroadcasterId = b.BroadcasterId,
                    Name = b.Name,
                    Country = b.Country,
                    ImageId = b.ImageId,
                    ChannelList = b.Channels.Select(c => new SimpleChannel() {
                        ChannelId = c.ChannelId,
                        Name = c.Name,
                        Commencement = c.Commencement,
                        Closed = c.Closed,
                        ProgrammeCount = c.Programmes.Count
                    }).ToList()
                });
        }
    }
}