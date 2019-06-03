using System;
using System.Linq;
using System.Threading.Tasks;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application.Commands
{
    public class ChannelEditCommand : ICommand<ChannelEditViewModel>
    {
        private readonly BeepBongContext _context;

        public ChannelEditCommand(BeepBongContext context) => _context = context;

        public void SendCommand(ChannelEditViewModel viewModel)
        {
            Channel c = new Channel()
            {
                ChannelId = viewModel.ChannelId,
                Name = viewModel.Name,
                Commencement = viewModel.Commencement,
                Closed = viewModel.Closed,
                BroadcasterId = viewModel.BroadcasterId
            };

            bool isNew = (viewModel.ChannelId == Guid.Empty);

            // Attach Entites
            _context.Attach(c).State = (isNew) ? EntityState.Added : EntityState.Modified;
        }
    }
}