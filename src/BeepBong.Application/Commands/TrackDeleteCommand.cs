using System;
using System.Collections.Generic;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Application.Commands
{
    public class TrackDeleteCommand
    {
        private readonly BeepBongContext _context;

        public TrackDeleteCommand(BeepBongContext context) => _context = context;

        public void SendCommand(Guid id)
        {
            Track t = _context.Tracks.Find(id);
            
            _context.Remove(t);

            // Save Database
            _context.SaveChanges();
        }
    }
}