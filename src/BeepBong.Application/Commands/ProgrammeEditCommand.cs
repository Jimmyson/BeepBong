using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application.Commands
{
    public class ProgrammeEditCommand : ICommand<ProgrammeEditViewModel>
    {
        private readonly BeepBongContext _context;

        public ProgrammeEditCommand(BeepBongContext context) => _context = context;

        public void SendCommand(ProgrammeEditViewModel viewModel)
        {
            Programme programme = new Programme()
            {
                ProgrammeId = viewModel.ProgrammeId,
                Name = viewModel.Name,
                AirDate = viewModel.AirDate,
                ChannelId = viewModel.ChannelId
            };

            List<ProgrammeTrackList> trackLists = (viewModel.TrackListIds != null) ?
                viewModel.TrackListIds.Select(tl => new ProgrammeTrackList()
                {
                    ProgrammeId = viewModel.ProgrammeId,
                    TrackListId = tl
                }).ToList() : new List<ProgrammeTrackList>();

            bool isNew = (viewModel.ProgrammeId == Guid.Empty);

            // Attach Entites
            _context.Attach(programme).State = (isNew) ? EntityState.Added : EntityState.Modified;

            // Update TrackList Relationship
            List<ProgrammeTrackList> existingPtl = _context.ProgrammeTrackLists.Where(ptl => ptl.ProgrammeId == viewModel.ProgrammeId).ToList();

            foreach (var trackList in existingPtl)
            {
                if (!trackLists.Contains(trackList))
                    // Delete Changes
                    _context.Attach(trackList).State = EntityState.Deleted;
                else
                    // Remove existing entites from ViewModel
                    trackLists.Remove(trackList);
            }
            // Add new itmes
            _context.ProgrammeTrackLists.AddRange(trackLists);
        }
    }
}