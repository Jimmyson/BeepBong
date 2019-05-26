using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application.Commands
{
    public class ChannelDeleteCommand : ICommand<Guid>
    {
        private readonly BeepBongContext _context;

        public ChannelDeleteCommand(BeepBongContext context) => _context = context;

        public void SendCommand(Guid id)
        {
            Action(id);

            // Save Changes
            _context.SaveChanges();
        }

        public async Task SendCommandAsync(Guid id)
        {
            Action(id);

            // Save Changes
            await _context.SaveChangesAsync();
        }

        //@TODO: Add logic to skip if null
        //@TODO: Make Async
        private void Action(Guid id)
        {
            List<Programme> programmeList = _context.Programmes.Where(p => p.ChannelId == id).ToList();

            Channel c = _context.Channels.Find(id);

            // Remove Channel Link from Programme
            foreach (var p in programmeList)
            {
                p.ChannelId = null;
                _context.Attach(p).State = EntityState.Modified;
            }

            // Remove Channel
            if (c != null)
            {
                _context.Channels.Remove(c);
            }
        }
    }
}