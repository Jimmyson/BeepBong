using System.Collections.Generic;
using System.Linq;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application.Commands
{
    public class TrackEditCommand
    {
        private readonly BeepBongContext _context;

        public TrackEditCommand(BeepBongContext context) => _context = context;

        public void SendCommand(TrackEditViewModel viewModel)
        {
            Track track = new Track() {
                TrackId = viewModel.TrackId,
                Name = viewModel.Name,
                Variant = viewModel.Variant,
                Description = viewModel.Description,
                TrackListId = viewModel.TrackListId
            };

            bool isNew = (viewModel.TrackId == null);
            
            // Attach Entites
            _context.Attach(track).State = (isNew) ? EntityState.Added : EntityState.Modified;

            // Save Database
            _context.SaveChanges();
        }
    }
}