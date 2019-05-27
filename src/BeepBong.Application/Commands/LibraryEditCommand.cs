using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
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
            Action(viewModel);

            // Save Database
            _context.SaveChanges();
        }

        public async Task SendCommandAsync(Library viewModel)
        {
            Action(viewModel);

            // Save Database
            await _context.SaveChangesAsync();
        }

        private void Action(Library viewModel)
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