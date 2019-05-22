using System.Collections.Generic;
using System.Linq;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application.Commands
{
    public class TrackListEditCommand
    {
        private readonly BeepBongContext _context;

        public TrackListEditCommand(BeepBongContext context) => _context = context;

        public void SendCommand(TrackListEditViewModel viewModel)
        {
            TrackList TrackList = new TrackList() {
                TrackListId = viewModel.TrackListId,
                Name = viewModel.Name,
                Composer = viewModel.Composer
            };

            List<ProgrammeTrackList> programmeLists =
                viewModel.Programmes.Select(p => new ProgrammeTrackList() {
                    TrackListId = viewModel.TrackListId,
                    ProgrammeId = p
                }).ToList();

            bool isNew = (viewModel.TrackListId == null);
            
            // Attach Entites
            _context.Attach(TrackList).State = (isNew) ? EntityState.Added : EntityState.Modified;
            
            // Update Programme Relationship
            List<ProgrammeTrackList> existingPtl = _context.ProgrammeTrackLists.Where(ptl => ptl.TrackListId == viewModel.TrackListId).ToList();

            foreach(var programmeList in existingPtl)
            {
                if (!programmeLists.Contains(programmeList))
                    // Delete Changes
                    _context.Attach(programmeList).State = EntityState.Deleted;
                else
                    // Remove existing entites from ViewModel
                    programmeLists.Remove(programmeList);
            }
            // Add new itmes
            _context.AttachRange(programmeLists);

            // Save Database
            _context.SaveChanges();
        }
    }
}