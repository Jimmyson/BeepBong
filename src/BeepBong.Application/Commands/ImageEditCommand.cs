using System;
using BeepBong.Application.Interfaces;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application.Commands
{
    public class ImageEditCommand : ICommand<Image>
    {
        private readonly BeepBongContext _context;

        public ImageEditCommand(BeepBongContext context) => _context = context;

        public void SendCommand(Image viewModel)
        {
            bool isNew = (viewModel.ImageId == Guid.Empty);

            _context.Attach(viewModel).State = (isNew) ? EntityState.Added : EntityState.Modified;
        }
    }
}