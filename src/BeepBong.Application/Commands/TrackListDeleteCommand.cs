using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeepBong.Application.Interfaces;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Application.Queries
{
    public class TrackListDeleteCommand : ICommand<Guid>
    {
        private readonly BeepBongContext _context;

        public TrackListDeleteCommand(BeepBongContext context) => _context = context;

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
            List<ProgrammeTrackList> list = _context.ProgrammeTrackLists.Where(ptl => ptl.TrackListId == id).ToList();

            TrackList tl = _context.TrackLists.Find(id);

            if (list.Any())
            {
                _context.ProgrammeTrackLists.RemoveRange(list);
            }

            if (tl != null)
            {
                _context.TrackLists.Remove(tl);
            }
        }
    }
}