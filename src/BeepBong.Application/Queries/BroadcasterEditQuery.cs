using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class BroadcasterEditQuery : IQuery<BroadcasterEditViewModel>
    {
        private readonly BeepBongContext _context;

        public BroadcasterEditQuery(BeepBongContext context) => _context = context;

        public IQueryable<BroadcasterEditViewModel> GetQuery(Guid? id)
        {
            return _context.Broadcasters
                .Where(b => b.BroadcasterId == id.Value)
                .Select(b => new BroadcasterEditViewModel() {
                    BroadcasterId = b.BroadcasterId,
                    Name = b.Name,
                    Country = b.Country
                });
        }

        public bool Exists(BroadcasterEditViewModel model)
        {
            return _context.Broadcasters.Any(b => b.Name.ToLower() == model.Name.ToLower()
                    && b.Country == model.Country);
        }
    }
}