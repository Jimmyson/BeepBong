using System;
using System.Linq;
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
            Broadcaster b = new Broadcaster()
            {
                BroadcasterId = viewModel.BroadcasterId,
                Name = viewModel.Name,
                Country = viewModel.Country,
                ImageId = viewModel.ImageId
            };

            // Remove old image from System
            if (viewModel.ImageChange && viewModel.Image == null)
                new ImageDeleteCommand(_context).SendCommand(viewModel.ImageId.Value);

            // Attach Image for Edit
            if (viewModel.Image != null && viewModel.ImageId != null)
            {
                // Add new image
                b.Image = _context.Images.Where(i => i.ImageId == viewModel.ImageId).First();
                // new ImageEditCommand(_context).SendCommand(viewModel.Image);
                b.Image.Base64 = viewModel.Image.Base64;
                b.Image.Height = viewModel.Image.Height;
                b.Image.MimeType = viewModel.Image.MimeType;
                b.Image.Width = viewModel.Image.Width;
            }
            else if (viewModel.Image != null && viewModel.ImageId == null)
            {
                b.Image = viewModel.Image;
            }

            bool isNew = (viewModel.BroadcasterId == Guid.Empty);

            // Attach Entites
            _context.Attach(b).State = (isNew) ? EntityState.Added : EntityState.Modified;
        }
    }
}