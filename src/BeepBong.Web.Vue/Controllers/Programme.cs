using System;
using System.Threading.Tasks;
using BeepBong.Application.Commands;
using BeepBong.Application.Queries;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Web.Vue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammeController : ControllerBase
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
        public async Task<ActionResult<ProgrammeDetailViewModel>> PostProgramme(ProgrammeEditViewModel pvm)
        {
            if (new ProgrammeEditQuery(_context).Exists(pvm)) return BadRequest();

            new ProgrammeEditCommand(_context).SendCommand(pvm);

            await _context.SaveChangesAsync();

            return new ProgrammeDetailViewModel();
        }

        // PUT: api/Programme/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgramme(Guid id, ProgrammeEditViewModel pvm)
        {
            if (id != pvm.ProgrammeId) return BadRequest();

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