using System;
using System.Linq;
using System.Threading.Tasks;
using BeepBong.Application.Commands;
using BeepBong.Application.Queries;
using BeepBong.Application.Validation;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using BeepBong.Web.Vue.Logic;
using BeepBong.Web.Vue.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Web.Vue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammeController : Controller
    {
        private readonly BeepBongContext _context;

        public ProgrammeController(BeepBongContext context)
        {
            _context = context;
        }

        // GET: api/Programme/
        [HttpGet]
        public async Task<ActionResult<Pagination<ProgrammeIndexViewModel>>> GetProgrammes(int? pageNumber, int? pageSize)
        {
            var query = new ProgrammeIndexQuery(_context).GetQuery(null);

            return await Pagination<ProgrammeIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 20);
        }

		// GET: api/Programme/IdList
		// @TODO: Alias query off
		[HttpGet("IdList")]
		public async Task<ActionResult<object>> GetBroadcasterIds()
		{
			var ids = await _context.Programmes.Select(x => new {
				Id = x.ProgrammeId,
				Name = x.Name
			}).OrderBy(x => x.Name).ToListAsync();

			return ids;
		}

        // GET: api/Programme/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgrammeDetailViewModel>> GetProgramme(Guid id)
        {
            var query = new ProgrammeDetailQuery(_context).GetQuery(id);

            return await query.SingleOrDefaultAsync();
        }

        // POST: api/Programme
        // @TODO: Return the created object
        [HttpPost]
        public async Task<ActionResult<ProgrammeDetailViewModel>> PostProgramme([FromForm] ProgrammeUploadViewModel puvm)
        {
			// Cast ViewModel to Model
			ProgrammeEditViewModel pvm = new ProgrammeEditViewModel()
			{
                ProgrammeId = puvm.ProgrammeId,
                Name = puvm.Name,
                AirDate = puvm.AirDate,
                ChannelId = puvm.ChannelId,
                TrackListIds = puvm.TrackListIds
			};

			// Validate Model
			ProgrammeEditValidator validator = new ProgrammeEditValidator();
			if (!validator.Validate(pvm).IsValid) return BadRequest();

            if (new ProgrammeEditQuery(_context).Exists(pvm)) return BadRequest();

			FileToImageEntity.CopyImageToModel(puvm, pvm);

            new ProgrammeEditCommand(_context).SendCommand(pvm);

            await _context.SaveChangesAsync();

            return new ProgrammeDetailViewModel();
        }

        // PUT: api/Programme/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgramme(Guid id, [FromForm] ProgrammeUploadViewModel puvm)
        {
            if (id != puvm.ProgrammeId) return BadRequest();

			// Cast ViewModel to Model
			ProgrammeEditViewModel pvm = new ProgrammeEditViewModel()
			{
                ProgrammeId = puvm.ProgrammeId,
                Name = puvm.Name,
                AirDate = puvm.AirDate,
                ChannelId = puvm.ChannelId,
                TrackListIds = puvm.TrackListIds,
				ImageChange = puvm.ImageChange ?? false,
                ImageId = puvm.ImageId
			};

			// Validate Model
			ProgrammeEditValidator validator = new ProgrammeEditValidator();
			if (!validator.Validate(pvm).IsValid) return BadRequest();

			FileToImageEntity.CopyImageToModel(puvm, pvm);

            new ProgrammeEditCommand(_context).SendCommand(pvm);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Programme/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramme(Guid id)
        {
            new ProgrammeDeleteCommand(_context).SendCommand(id);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}