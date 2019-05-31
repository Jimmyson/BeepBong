using System;
using ImageMagick;

namespace BeepBong.Application
{
    public class ImageProcessing : IDisposable
    {
        private IMagickImage Image { get; set; }
        private IMagickImage TempImage { get; set; }

        public ImageProcessing(byte[] imageData)
        {
            Image = new MagickImage(imageData);
        }

		public ImageProcessing(string base64)
		{
			if (base64.StartsWith("data:"))
				base64 = base64.Split(',')[1];
			
			Image = MagickImage.FromBase64(base64);
		}

        public int Width { get => (TempImage != null ? TempImage.Width : Image.Width); }
        public int Height { get => (TempImage != null ? TempImage.Height : Image.Height); }

        /// <summary>
        /// Convert an encoded image into a Base64 Data URI
        /// </summary>
        /// <returns></returns>
        public string ToDataURI()
        {
            return (TempImage != null) ?
					"data:" + TempImage.FormatInfo.MimeType + ";base64," + TempImage.ToBase64()
					: "data:" + Image.FormatInfo.MimeType + ";base64," + Image.ToBase64();
        }
        /// <summary>
        /// Scale down the image to the default height value of 720px. Scale retains image ratio.
        /// </summary>
        /// <param name="imageData">Byte Image Sequence</param>
        /// <returns>Byte Image Sequence of scaled image</returns>
        public void DownscaleImage() {
            DownscaleImage(720);
        }

        /// <summary>
        /// Scale down the image to the default height value. Scale retains image ratio.
        /// </summary>
        /// <param name="imageData"Byte Image Sequence></param>
        /// <param name="height">Desired height in pixels</param>
        /// <returns>Byte Image Sequence of scaled image</returns>
        public void DownscaleImage(int height)
        {
            // Check size
            if (Image.Height > height)
            {
                // Reset Temp Image
                TempImage = Image.Clone();

                // Scale Image and retain aspect
                TempImage.Scale(0, height);
            }
        }

        public string ToBase64() => (TempImage != null) ? TempImage.ToBase64() : Image.ToBase64();
        public string MimeType => (TempImage != null) ? TempImage.FormatInfo.MimeType : Image.FormatInfo.MimeType;

        /// <summary>
        /// Clean up Object for removal
        /// </summary>
        public void Dispose()
        {
            Image.Dispose();
            Image = null;

			if (TempImage != null)
			{
				TempImage.Dispose();
				TempImage = null;
			}
        }
    }
}
