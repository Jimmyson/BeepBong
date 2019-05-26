using System;
using System.Linq;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Application.Queries
{
    public class LibraryDetailQuery
    {
        private readonly BeepBongContext _context;

        public LibraryDetailQuery(BeepBongContext context) => _context = context;

        public IQueryable<Library> GetQuery(Guid libraryId)
        {
            return _context.Libraries
                .Where(l => l.LibraryId == libraryId);
        }
    }
}