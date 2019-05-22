using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Application.Commands
{
    public class SampleDeleteCommand : ICommand<Guid>
    {
        private readonly BeepBongContext _context;

        public SampleDeleteCommand(BeepBongContext context) => _context = context;

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

        private void Action(Guid id)
        {
            Sample s = _context.Samples.Find(id);

            _context.Remove(s);
        }
    }
}