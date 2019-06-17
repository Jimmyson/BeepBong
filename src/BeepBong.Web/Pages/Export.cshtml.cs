using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
using System.IO;
using BeepBong.Application;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Web.Pages
{
    public class ExportModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        public ActionResult OnPostDownload()
        {
            var includeImages = (Request.Form.Keys.Contains("ImageInclude"));

			var options = new DbContextOptionsBuilder<BeepBongContext>()
                .UseSqlite("Data Source=../BeepBong.Web/BeepBong.db")
                .Options;

			using (var memoryStream = new MemoryStream())
            {
                var xDoc = XMLTranslate.ExportData(options, includeImages);
                xDoc.Save(memoryStream);

                return File(memoryStream.ToArray(), "application/xml", "BeepBong.xml");
            }
        }
    }
}