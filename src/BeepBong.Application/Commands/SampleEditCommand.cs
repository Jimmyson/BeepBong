using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Application.Commands
{
    public class SampleEditCommand : ICommand<SampleEditViewModel>
    {
        private readonly BeepBongContext _context;

        public SampleEditCommand(BeepBongContext context) => _context = context;

        public void SendCommand(SampleEditViewModel viewModel)
        {
            Sample s = _context.Samples.Find(viewModel.SampleId);

            s.Notes = viewModel.Notes;
            s.TrackId = viewModel.TrackId;

            _context.Attach(s).State = EntityState.Modified;
        }
    }
}