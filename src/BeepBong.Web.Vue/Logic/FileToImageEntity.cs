using System.IO;
using BeepBong.Application;
using BeepBong.Application.Interfaces;
using BeepBong.Domain.Models;
using BeepBong.Web.Vue.ViewModel;

namespace BeepBong.Web.Vue.Logic
{
    public static class FileToImageEntity
    {
		public static void CopyImageToModel(IImageUpload upload, IImageEntity entity)
		{
			if (upload.Image?.Length > 0)
            {
                using (var ms = new MemoryStream()) {
                    upload.Image.CopyTo(ms);

                    Image i = new Image();

                    using (ImageProcessing imageProc = new ImageProcessing(ms.ToArray()))
                    {
                        imageProc.DownscaleImage();
                        i.Base64 = imageProc.ToBase64();
                        i.Height = imageProc.Height;
                        i.MimeType = imageProc.MimeType;
                        i.Width = imageProc.Width;

                        entity.Image = i;
                    }
                }
            }
		}
    }
}