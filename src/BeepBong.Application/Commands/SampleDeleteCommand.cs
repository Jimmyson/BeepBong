using System;
using System.Collections.Generic;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Application.Commands
{
    public class SampleDeleteCommand
    {
        private readonly BeepBongContext _context;

        public SampleDeleteCommand(BeepBongContext context) => _context = context;

        public void SendCommand(Guid id)
        {
            Sample s = _context.Samples.Find(id);
            
            _context.Remove(s);

            // Save Database
            _context.SaveChanges();
        }
    }
}