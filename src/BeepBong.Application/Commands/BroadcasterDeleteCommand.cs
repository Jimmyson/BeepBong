using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application.Commands
{
    public class BroadcasterDeleteCommand : ICommand<Guid>
    {
        private readonly BeepBongContext _context;
        private readonly ChannelDeleteCommand _channelDeleteCommand;

        public BroadcasterDeleteCommand(BeepBongContext context)
        {
            _context = context;
            _channelDeleteCommand = new ChannelDeleteCommand(_context);
        }

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
            List<Guid> channelList = _context.Channels
                .Where(c => c.BroadcasterId == id)
                .Select(c => c.ChannelId)
                .ToList();

            Broadcaster b = _context.Broadcasters.Find(id);

            // Remove Channel Link from Programme
            foreach (var c in channelList)
            {
                _channelDeleteCommand.SendCommand(c);
            }

            // Remove Channel
            if (b != null)
            {
                _context.Broadcasters.Remove(b);
            }
        }
    }
}