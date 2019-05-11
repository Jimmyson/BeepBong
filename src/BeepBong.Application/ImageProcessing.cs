using System;
using ImageMagick;

namespace BeepBong.Application
{
    public class ImageProcessing : IDisposable
    {
        private MagickImage Image { get; set; }
        private IMagickImage TempImage { get; set; }

        public ImageProcessing(byte[] imageData)
        {
            Image = new MagickImage(imageData);
            TempImage = new MagickImage(imageData);
        }

        public int Width { get => (TempImage != null ? TempImage.Width : 0); }
        public int Height { get => (TempImage != null ? TempImage.Height : 0); }

        /// <summary>
        /// Convert an encoded image into a Base64 Data URL
        /// </summary>
        /// <returns></returns>
        public string ToDataURL()
        {
            return "data:" + TempImage.FormatInfo.MimeType + ";base64," + TempImage.ToBase64();
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

        /// <summary>
        /// Clean up Object for removal
        /// </summary>
        public void Dispose()
        {
            Image.Dispose();
            TempImage.Dispose();
            Image = null;
            TempImage = null;
        }
    }
}
