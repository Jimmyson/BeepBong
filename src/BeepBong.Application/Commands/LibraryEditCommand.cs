using System;
using System.Collections.Generic;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application.Commands
{
    public class LibraryEditCommand
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

            bool isNew = (viewModel.LibraryId == null);

            _context.Attach(l).State = (isNew) ? EntityState.Added : EntityState.Modified;

            // Save Database
            _context.SaveChanges();
        }
    }
}