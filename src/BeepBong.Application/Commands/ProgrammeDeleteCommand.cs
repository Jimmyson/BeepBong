using System;
using System.Collections.Generic;
using System.Linq;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Application.Queries
{
    public class ProgrammeDeleteCommand
    {
        private readonly BeepBongContext _context;

        public ProgrammeDeleteCommand(BeepBongContext context) => _context = context;

        public void SendCommand(Guid id)
        {
            List<ProgrammeTrackList> list = _context.ProgrammeTrackLists.Where(ptl => ptl.ProgrammeId == id).ToList();

            Programme p = _context.Programmes.Find(id);

            if (list.Any())
            {
                _context.ProgrammeTrackLists.RemoveRange(list);
            }

            if (p != null)
            {
                _context.Programmes.Remove(p);
            }
            
            _context.SaveChanges();
        }
    }
}