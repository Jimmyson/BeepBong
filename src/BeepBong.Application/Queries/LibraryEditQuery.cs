using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Application.Queries
{
    public class LibraryEditQuery : IQuery<Library>, IExists<Library>
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
            return _context.Libraries.Any(
                l => l.LibraryId != model.LibraryId
                && string.Equals(l.AlbumName, model.AlbumName, StringComparison.OrdinalIgnoreCase)
                && l.Catalog == model.Catalog
                && l.Label == model.Label);
        }
    }
}