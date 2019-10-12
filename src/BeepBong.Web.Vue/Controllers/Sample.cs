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
    public class SampleController : Controller
    {
        private readonly BeepBongContext _context;

        public SampleController(BeepBongContext context)
        {
            _context = context;
        }

        // GET: api/Sample/
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<SampleIndexViewModel>>> GetSamples(int? pageNumber, int? pageSize)
        // {
        //     var query = new SampleIndexQuery(_context).GetQuery(null);

        //     return await PaginatedList<SampleIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 20);
        // }

        // GET: api/Sample/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SampleDetailViewModel>> GetSample(Guid id)
        {
            var query = new SampleDetailQuery(_context).GetQuery(id);

            return await query.SingleOrDefaultAsync();
        }

        // POST: api/Sample
        // @TODO: Return the created object
        [HttpPost]
        public async Task<ActionResult<SampleDetailViewModel>> PostSample(SampleCreateViewModel svm)
        {
            if (new SampleEditQuery(_context).Exists(svm)) return BadRequest();

            new SampleCreateCommand(_context).SendCommand(svm);

            await _context.SaveChangesAsync();

            return new SampleDetailViewModel();
        }

        // PUT: api/Sample/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSample(Guid id, SampleEditViewModel svm)
        {
            if (id != svm.SampleId) return BadRequest();

            new SampleEditCommand(_context).SendCommand(svm);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Sample/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSample(Guid id)
        {
            new SampleDeleteCommand(_context).SendCommand(id);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}