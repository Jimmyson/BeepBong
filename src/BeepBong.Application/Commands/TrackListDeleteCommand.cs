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