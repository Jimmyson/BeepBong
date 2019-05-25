using System;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;
using System.IO;
using BeepBong.Application;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Web.Pages
{
    public class ExportModel : PageModel
    {
        private readonly BeepBongContext _context;

        public ExportModel(BeepBongContext context) => _context = context;

        public IActionResult OnGet()
        {
            return Page();
        }

        public ActionResult OnPostDownload()
        {
			var options = new DbContextOptionsBuilder<BeepBongContext>()
                .UseSqlite("Data Source=../BeepBong.Web/BeepBong.db")
                .Options;

			var memoryStream = new MemoryStream();
			var xDoc = XMLTranslate.ExportData(options);
			xDoc.Save(memoryStream);

            return File(memoryStream.ToArray(), "application/xml");
        }
    }
}