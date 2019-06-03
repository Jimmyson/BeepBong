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

        public bool Exists(Library model)
        {
            return _context.Libraries.Any(l => l.AlbumName.ToLower() == model.AlbumName.ToLower()
                    && l.Catalog == model.Catalog
                    && l.Label == model.Label);
        }
    }
}