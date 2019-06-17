using System;
using BeepBong.Application.Interfaces;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application.Commands
{
    public class LibraryEditCommand : ICommand<Library>
    {
        private readonly BeepBongContext _context;

        public LibraryEditCommand(BeepBongContext context) => _context = context;

        public void SendCommand(Library viewModel)
        {
            Library l = new Library() {
                LibraryId = viewModel.LibraryId,
                AlbumName = viewModel.AlbumName,
                Catalog = viewModel.Catalog,
                MBID = viewModel.MBID,
                Label = viewModel.Label
            };

            bool isNew = (viewModel.LibraryId == Guid.Empty);

            _context.Attach(l).State = (isNew) ? EntityState.Added : EntityState.Modified;
        }
    }
}