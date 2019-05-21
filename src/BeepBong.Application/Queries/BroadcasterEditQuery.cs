using System.Linq;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class BroadcasterEditQuery
    {
        private readonly BeepBongContext _context;

        public BroadcasterEditQuery(BeepBongContext context) => _context = context;

        public IQueryable<BroadcasterEditViewModel> GetQuery()
        {
            return _context.Broadcasters
                .Select(b => new BroadcasterEditViewModel() {
                    BroadcasterId = b.BroadcasterId,
                    Name = b.Name,
                    Country = b.Country
                });
        }
    }
}