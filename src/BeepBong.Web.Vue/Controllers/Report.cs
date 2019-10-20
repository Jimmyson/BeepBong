using System;
using System.Threading.Tasks;
using BeepBong.Application.Commands;
using BeepBong.Application.Queries.Report;
using BeepBong.Application.ViewModels;
using BeepBong.Application.ViewModels.Report;
using BeepBong.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Web.Vue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly BeepBongContext _context;

        public ReportController(BeepBongContext context)
        {
            _context = context;
        }

        // GET: api/Report/LibrarySample
        [HttpGet("LibrarySample")]
        public async Task<ActionResult<Pagination<LibrarySampleViewModel>>> GetLibrarySample(int? pageNumber, int? pageSize)
        {
            var query = new LibrarySampleQuery(_context).GetQuery(null);

            return await Pagination<LibrarySampleViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 20);
        }

        // GET: api/Report/OrphanedTrackList
        [HttpGet("OrphanedTrackList")]
        public async Task<ActionResult<Pagination<TrackListIndexViewModel>>> GetBroadcaster(int? pageNumber, int? pageSize)
        {
            var query = new OrphanedTrackListQuery(_context).GetQuery(null);

            return await Pagination<TrackListIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 20);
        }

        // GET: api/Report/ProgrammeWOTrackList
        [HttpGet("ProgrammeWOTrackList")]
        public async Task<ActionResult<Pagination<ProgrammeIndexViewModel>>> GetBroadcasterChannels(int? pageNumber, int? pageSize)
        {
            var query = new ProgrammeWOTrackListQuery(_context).GetQuery(null);

            return await Pagination<ProgrammeIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 20);
        }
    }
}