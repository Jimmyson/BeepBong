using System;
using System.IO;
using Xunit;
using BeepBong.Application;

namespace BeepBong.Application.Test
{
    public class ImageTest
    {
        [Fact]
        public void Downscale720Test()
        {
            using (var imageFile = File.OpenRead("..\\..\\..\\news-radio-background-2000-1125.jpg"))
            {
                MemoryStream mem = new MemoryStream();
                imageFile.CopyTo(mem);

                using (ImageProcessing image = new ImageProcessing(mem.ToArray()))
                {
                    image.DownscaleImage();
                    Assert.Equal(720, image.Height);
                }
                imageFile.Close();
            }
        }

        [Fact]
        public void DoubleDownscaleTest()
        {
            using (var imageFile = File.OpenRead("..\\..\\..\\news-radio-background-2000-1125.jpg"))
            {
                MemoryStream mem = new MemoryStream();
                imageFile.CopyTo(mem);
            //Given
                ImageProcessing image1 = new ImageProcessing(mem.ToArray());
                ImageProcessing image2 = new ImageProcessing(mem.ToArray());
            
            //When
                image1.DownscaleImage(9);
                image1.DownscaleImage();

                image2.DownscaleImage();

            //Then
                Assert.Equal(image1.ToDataURL(), image2.ToDataURL());
                imageFile.Close();
            }
        }
    }
}
