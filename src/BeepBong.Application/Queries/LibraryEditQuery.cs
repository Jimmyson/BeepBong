using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Application.Queries
{
    public class LibraryEditQuery : IQuery<Library>
    {
        private readonly BeepBongContext _context;

        public LibraryEditQuery(BeepBongContext context) => _context = context;

        public IQueryable<Library> GetQuery(Guid? libraryId)
        {
            return _context.Libraries
                .Where(l => l.LibraryId == libraryId.Value);
        }
    }
}