using System;
using System.Collections.Generic;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Application.Commands
{
    public class LibraryDeleteCommand
    {
        private readonly BeepBongContext _context;

        public LibraryDeleteCommand(BeepBongContext context) => _context = context;

        public void SendCommand(Guid id)
        {
            Library l = _context.Libraries.Find(id);
            
            _context.Remove(l);

            // Save Database
            _context.SaveChanges();
        }
    }
}