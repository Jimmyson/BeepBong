using System;
using System.Collections.Generic;
using System.Linq;
using BeepBong.Application.Interfaces;
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