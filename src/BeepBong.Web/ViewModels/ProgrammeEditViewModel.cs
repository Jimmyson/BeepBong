using System;
using Microsoft.AspNetCore.Http;

namespace BeepBong.Web.ViewModels
{
    public class ProgrammeEditViewModel
    {
        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Channel { get; set; }
        public string AudioComposer { get; set; }
        
        public IFormFile LogoUpload { get; set; }
        public string Logo { get; set; }
        public bool IsLibraryMusic { get; set; }
    }
}