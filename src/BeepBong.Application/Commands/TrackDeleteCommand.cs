using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeepBong.Application.Interfaces;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Application.Commands
{
    public class TrackDeleteCommand : ICommand<Guid>
    {
        private readonly BeepBongContext _context;

        public TrackDeleteCommand(BeepBongContext context) => _context = context;

        public void SendCommand(Guid id)
        {
            Action(id);

            // Save Database
            _context.SaveChanges();
        }

        public async Task SendCommandAsync(Guid id)
        {
            Action(id);

            // Save Database
            await _context.SaveChangesAsync();
        }

        //@TODO: Add logic to skip if null
        //@TODO: Make Async
        private void Action(Guid id)
        {
            Track t = _context.Tracks.Find(id);

            _context.Remove(t);
        }
    }
}