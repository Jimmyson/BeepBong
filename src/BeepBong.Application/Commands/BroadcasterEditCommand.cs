using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application.Commands
{
    public class BroadcasterEditCommand
    {
        private readonly BeepBongContext _context;

        public BroadcasterEditCommand(BeepBongContext context) => _context = context;

        public void SendCommand(BroadcasterEditViewModel viewModel)
        {
            Broadcaster b = new Broadcaster() {
                BroadcasterId = viewModel.BroadcasterId,
                Name = viewModel.Name,
                Country = viewModel.Country
            };

            bool isNew = (viewModel.BroadcasterId == null);
            
            // Attach Entites
            _context.Attach(b).State = (isNew) ? EntityState.Added : EntityState.Modified;
        }
    }
}