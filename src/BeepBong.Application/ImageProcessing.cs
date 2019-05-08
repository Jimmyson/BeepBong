using System;
using ImageMagick;

namespace BeepBong.Application
{
    public class ImageProcessing
    {
        private MagickImage Image { get; set; }

        public ImageProcessing(byte[] imageData)
        {
            Image = new MagickImage(imageData);
        }

        /// <summary>
        /// Convert an encoded image into a Base64 Data URL
        /// </summary>
        /// <returns></returns>
        public string ToDataURL()
        {
            return "data:" + Image.FormatInfo.MimeType + ";base64," + Image.ToBase64();
        }

        /// <summary>
        /// Scale down the image to the default height value of 720px. Scale retains image ratio.
        /// </summary>
        /// <param name="imageData">Byte Image Sequence</param>
        /// <returns>Byte Image Sequence of scaled image</returns>
        public void DownscaleImage(byte[] imageData) {
            DownscaleImage(imageData, 720);
        }

        /// <summary>
        /// Scale down the image to the default height value. Scale retains image ratio.
        /// </summary>
        /// <param name="imageData"Byte Image Sequence></param>
        /// <param name="height">Desired height in pixels</param>
        /// <returns>Byte Image Sequence of scaled image</returns>
        public void DownscaleImage(byte[] imageData, int height)
        {
            // Check size
            if (Image.Height > height)
            {
                // Scale Image and retain aspect
                Image.Scale(0, height);
            }
        }
    }
}
