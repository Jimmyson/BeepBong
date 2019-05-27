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
    public class SampleCreateCommand : ICommand<SampleCreateViewModel>
    {
        private readonly BeepBongContext _context;

        public SampleCreateCommand(BeepBongContext context) => _context = context;

        public void SendCommand(SampleCreateViewModel viewModel)
        {
            Action(viewModel);

            // Save Database
            _context.SaveChanges();
        }

        public async Task SendCommandAsync(SampleCreateViewModel viewModel)
        {
            Action(viewModel);

            // Save Database
            await _context.SaveChangesAsync();
        }

        private void Action(SampleCreateViewModel viewModel)
        {
            Sample s = new Sample()
            {
                SampleRate = int.Parse(viewModel.SampleRate),
                SampleCount = int.Parse(viewModel.SampleCount),
                AudioChannelCount = int.Parse(viewModel.AudioChannelCount),
                BitRate = int.Parse(viewModel.BitRate),
                BitRateMode = viewModel.BitRateMode,
                BitDepth = int.Parse(viewModel.BitDepth),
                Codec = viewModel.Codec,
                Compression = viewModel.Compression,
                Fingerprint = viewModel.Fingerprint,
                OtherAttributes = viewModel.OtherAttributes,
                Notes = viewModel.Notes,
                Waveform = viewModel.Waveform,
                Spectrograph = viewModel.Spectrograph,
                TrackId = viewModel.TrackId
            };

            _context.Attach(s);
        }
    }
}