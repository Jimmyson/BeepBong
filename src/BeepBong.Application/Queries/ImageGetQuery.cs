using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class ImageGetQuery : IQuery<ImageDetailViewModel>
    {
        private readonly BeepBongContext _context;

        public ImageGetQuery(BeepBongContext context) => _context = context;
        
        public IQueryable<ImageDetailViewModel> GetQuery(Guid? id = null)
        {
            return _context.Images
                .Where(i => i.ImageId == id.Value)
                .Select(i => new ImageDetailViewModel() {
                    Data = Convert.FromBase64String(i.Base64),
                    MimeType = i.MimeType
                });
        }
    }
}