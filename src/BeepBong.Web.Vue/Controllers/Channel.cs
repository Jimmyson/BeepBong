using System;
using System.Linq;
using System.Threading.Tasks;
using BeepBong.Application.Commands;
using BeepBong.Application.Queries;
using BeepBong.Application.Validation;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Web.Vue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : Controller
    {
        private readonly BeepBongContext _context;

        public ChannelController(BeepBongContext context)
        {
            _context = context;
        }

        // GET: api/Channel/
        [HttpGet]
        public async Task<ActionResult<Pagination<ChannelIndexViewModel>>> GetChannels(int? pageNumber, int? pageSize)
        {
            var query = new ChannelIndexQuery(_context).GetQuery(null);

            return await Pagination<ChannelIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 20);
        }

		// GET: api/Channel/IdList
		// @TODO: Alias query off
		[HttpGet("IdList")]
		public async Task<ActionResult<object>> GetBroadcasterIds()
		{
			var ids = await _context.Channels.Select(x => new {
				Id = x.ChannelId,
				Name = x.Name
			}).OrderBy(x => x.Name).ToListAsync();

			return ids;
		}

        // GET: api/Channel/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ChannelDetailViewModel>> GetChannel(Guid id)
        {
            var query = new ChannelDetailQuery(_context).GetQuery(id);

            return await query.SingleOrDefaultAsync();
        }
        
        // GET: api/Channel/{id}/Programmes
        // Retrieve channels from the Broadcaster
        [HttpGet("{id}/Programmes")]
        public async Task<ActionResult<Pagination<ProgrammeIndexViewModel>>> GetChannelProgrammes(Guid id, int? pageNumber, int? pageSize)
        {
            var query = new ProgrammeIndexQuery(_context).GetQuery(id);

            return await Pagination<ProgrammeIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 20);
        }

        // POST: api/Channel
        // @TODO: Return the created object
        [HttpPost]
        public async Task<ActionResult<ChannelDetailViewModel>> PostChannel(ChannelEditViewModel cvm)
        {
			// Validate Model
			ChannelEditValidator validator = new ChannelEditValidator();
			if (!validator.Validate(cvm).IsValid) return BadRequest();

            if (new ChannelEditQuery(_context).Exists(cvm)) return BadRequest();

            new ChannelEditCommand(_context).SendCommand(cvm);

            await _context.SaveChangesAsync();

            return new ChannelDetailViewModel();
        }

        // PUT: api/Channel/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChannel(Guid id, ChannelEditViewModel cvm)
        {
            if (id != cvm.ChannelId) return BadRequest();
			
			// Validate Model
			ChannelEditValidator validator = new ChannelEditValidator();
			if (!validator.Validate(cvm).IsValid) return BadRequest();

            new ChannelEditCommand(_context).SendCommand(cvm);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Channel/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChannel(Guid id)
        {
            new ChannelDeleteCommand(_context).SendCommand(id);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}