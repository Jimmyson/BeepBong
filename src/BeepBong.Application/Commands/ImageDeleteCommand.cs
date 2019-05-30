using System;
using BeepBong.Application.Interfaces;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Application.Commands
{
    public class ImageDeleteCommand : ICommand<Guid>
    {
        private readonly BeepBongContext _context;

        public ImageDeleteCommand(BeepBongContext context) => _context = context;

        public void SendCommand(Guid id)
        {
            Image i = _context.Images.Find(id);

            _context.Images.Remove(i);
        }
    }
}