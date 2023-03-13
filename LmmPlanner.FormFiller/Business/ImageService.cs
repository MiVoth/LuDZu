using System.IO;
using ImageMagick;

namespace LmmPlanner.LmmFormFiller.Business
{
    public class ImageService
    {
        private byte[] ResizeImage(byte[] image, int max)
        {
            var mgImg = new MagickImage(image);
            var mgeo = new MagickGeometry
            {
                IgnoreAspectRatio = false,
                FillArea = true
            };
            mgImg.Quality = 50;
            if (mgImg.Width > mgImg.Height)
            {
                mgeo.Width = max;
            }
            else
            {
                mgeo.Height = max;
            }
            mgImg.Resize(mgeo);
            return mgImg.ToByteArray();
        }
        private byte[] ResizeImage2(byte[] image, int width, int height)
        {
            var mgImg = new MagickImage(image);
            mgImg.Resize(new MagickGeometry
            {
                IgnoreAspectRatio = false,
                FillArea = true,
                Width = width,
                Height = height
            });
            return mgImg.ToByteArray();
        }

        public byte[] Compress(byte[] image)
        {
            return ResizeImage(image, 1000);
        }
        public byte[] Vorschau(byte[] image) => ResizeImage(image, 300);


        public byte[]? PdfToImage(byte[] pdf)
        {
            var settings = new MagickReadSettings
            {
                // Settings the density to 300 dpi will create an image with a better quality
                //settings.Density = new Density(300, 300);
                Density = new Density(125, 125)
            };
            // var thumbSettings = new MagickReadSettings
            // {
            //     Density = new Density(50, 50)
            // };
            byte[]? picture = null;
            using (var images = new MagickImageCollection())
            {
                // using (var thumbImages = new MagickImageCollection())
                // {
                // Add all the pages of the pdf file to the collection
                //images.Read("Snakeware.pdf", settings);
                images.Read(pdf, settings);
                // thumbImages.Read(blob.Content, thumbSettings);
                short page = 1;
                var i = 0;
                foreach (var image in images)
                {
                    // Write page to file that contains the page number
                    // using (var msThumb = new MemoryStream())
                    // {
                    using (var ms = new MemoryStream())
                    {
                        // thumbImages[i].Alpha(AlphaOption.Remove);
                        // thumbImages[i].Write(msThumb, MagickFormat.Png);

                        image.Alpha(AlphaOption.Remove);
                        image.Write(ms, MagickFormat.Png);
                        //_logger.Log("Vor Bildererstellung");
                        picture = ms.ToArray();
                    }
                    //image.Write(@"C:\Users\Michi\Dev\tmp\Snakeware.Page" + page + ".png");
                    // Writing to a specific format works the same as for a single image
                    //image.Format = MagickFormat.Ptif;
                    //image.Write("Snakeware.Page" + page + ".tif");
                    page++;
                    // }
                    i++;
                }
                // }
                return picture;
            }
        }
    }
}