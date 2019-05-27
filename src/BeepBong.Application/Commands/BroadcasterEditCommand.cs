using System;
using System.Threading.Tasks;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application.Commands
{
    public class BroadcasterEditCommand : ICommand<BroadcasterEditViewModel>
    {
        private readonly BeepBongContext _context;

        public BroadcasterEditCommand(BeepBongContext context) => _context = context;

        public void SendCommand(BroadcasterEditViewModel viewModel)
        {
            Action(viewModel);

            // Save Changes
            _context.SaveChanges();
        }

        public async Task SendCommandAsync(BroadcasterEditViewModel viewModel)
        {
            Action(viewModel);

            // Save Changes
            await _context.SaveChangesAsync();
        }

        private void Action(BroadcasterEditViewModel viewModel)
        {
            Broadcaster b = new Broadcaster()
            {
                BroadcasterId = viewModel.BroadcasterId,
                Name = viewModel.Name,
                Country = viewModel.Country
            };

            bool isNew = (viewModel.BroadcasterId == Guid.Empty);

            // Attach Entites
            _context.Attach(b).State = (isNew) ? EntityState.Added : EntityState.Modified;
        }
    }
}