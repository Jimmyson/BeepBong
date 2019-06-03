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

            List<ProgrammeTrackList> programmeLists = (viewModel.Programmes != null) ?
                viewModel.Programmes.Select(p => new ProgrammeTrackList()
                {
                    TrackListId = viewModel.TrackListId,
                    ProgrammeId = p
                }).ToList() : new List<ProgrammeTrackList>();

            bool isNew = (viewModel.TrackListId == Guid.Empty);

            // Attach Entites
            _context.Attach(TrackList).State = (isNew) ? EntityState.Added : EntityState.Modified;

            // Update Programme Relationship
            List<ProgrammeTrackList> existingPtl = _context.ProgrammeTrackLists.Where(ptl => ptl.TrackListId == viewModel.TrackListId).ToList();

            foreach (var programmeList in existingPtl)
            {
                if (!programmeLists.Contains(programmeList))
                    // Delete Changes
                    _context.Attach(programmeList).State = EntityState.Deleted;
                else
                    // Remove existing entites from ViewModel
                    programmeLists.Remove(programmeList);
            }
            // Add new itmes
            _context.ProgrammeTrackLists.AddRange(programmeLists);
        }
    }
}