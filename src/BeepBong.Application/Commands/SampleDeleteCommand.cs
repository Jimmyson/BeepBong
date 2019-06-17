using System;
using BeepBong.Application.Interfaces;
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
            Sample s = _context.Samples.Find(id);

            _context.Remove(s);
        }
    }
}