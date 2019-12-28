using System;
using System.Collections.Generic;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application.Commands
{
    public class TrackListEditCommand : ICommand<TrackListEditViewModel>
    {
        private readonly BeepBongContext _context;

        public TrackListEditCommand(BeepBongContext context) => _context = context;

        public void SendCommand(TrackListEditViewModel viewModel)
        {
            TrackList TrackList = new TrackList()
            {
                TrackListId = viewModel.TrackListId,
                Name = viewModel.Name,
                Composer = viewModel.Composer,
                Library = viewModel.Library
            };

			List<TrackEditViewModel> tracks = viewModel.Tracks?.Select(t => new TrackEditViewModel() {
				TrackId = t.TrackId,
				Name = t.Name,
				Variant = t.Variant,
				Description = t.Description,
				TrackListId = viewModel.TrackListId
			}).ToList();

            List<ProgrammeTrackList> programmeLists = (viewModel.Programmes != null) ?
                viewModel.Programmes.Select(p => new ProgrammeTrackList()
                {
                    TrackListId = viewModel.TrackListId,
                    ProgrammeId = p
                }).ToList() : new List<ProgrammeTrackList>();

            bool isNew = (viewModel.TrackListId == Guid.Empty);

            // Attach Entites
            _context.Attach(TrackList).State = (isNew) ? EntityState.Added : EntityState.Modified;

			// Remove Tracks
			List<Track> existingTracks = _context.Tracks.Where(t => t.TrackListId == viewModel.TrackListId).ToList();

			foreach (var track in existingTracks)
			{
				if (!tracks.Select(t => t.TrackId).Contains(track.TrackId))
				{
					// Delete Track
					_context.Attach(track).State = EntityState.Deleted;
				}
			}

			// Update Remaining Tracks
			var trackEditCommand = new TrackEditCommand(_context);
			tracks.ForEach(t => trackEditCommand.SendCommand(t));

            // Update Programme Relationship
            List<ProgrammeTrackList> existingPtl = _context.ProgrammeTrackLists.Where(ptl => ptl.TrackListId == viewModel.TrackListId).ToList();

            foreach (var programmeList in existingPtl)
            {
                if (!programmeLists.Contains(programmeList))
                {
                    // Delete Changes
                    _context.Attach(programmeList).State = EntityState.Deleted;
                }
                else
                {
                    // Remove existing entites from ViewModel
                    programmeLists.Remove(programmeList);
                }
            }

            // Add new itmes
            _context.ProgrammeTrackLists.AddRange(programmeLists);
        }
    }
}