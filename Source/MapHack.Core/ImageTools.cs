using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MapHack.Core
{
    public static class ImageTools
    {

        public static int GetMaximumZoom(Image image, int tileSize)
        {
            return DimensionalTools.GetMaximumZoom(image.Width, image.Height, tileSize);
        }

        public static int GetMaximumDimension(Image image, int tileSize)
        {
            return DimensionalTools.GetMaximumDimension(image.Width, image.Height, tileSize);
        }

        public static int GetMaximumResolution(Image image, int tileSize)
        {
            return DimensionalTools.GetMaximumResolution(image.Width, image.Height, tileSize);
        }

        public static Image CropImage(Image image, int targetWidth, int targetHeight, Color? padColor = null)
        {
            if (image.Width == targetWidth && image.Height == targetHeight)
            {
                return image;
            }
            var bitmap = new Bitmap(targetWidth, targetHeight, PixelFormat.Format24bppRgb);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                if (padColor.HasValue)
                {
                    graphics.FillRectangle(new SolidBrush(padColor.Value), new Rectangle(0, 0, targetWidth, targetHeight));
                }
                graphics.DrawImage(image,
                    new Rectangle((targetWidth / 2) - (image.Width / 2), (targetHeight / 2) - (image.Height / 2), image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height),
                    GraphicsUnit.Pixel);
            }
            return bitmap;
        }

        public static Image ResizeImage(Image image, int targetWidth, int targetHeight)
        {
            if (image.Width == targetWidth && image.Height == targetHeight)
            {
                return image;
            }
            var bitmap = new Bitmap(targetWidth, targetHeight, PixelFormat.Format24bppRgb);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image,
                    new Rectangle(0, 0, targetWidth, targetHeight),
                    new Rectangle(0, 0, image.Width, image.Height),
                    GraphicsUnit.Pixel);
            }
            return bitmap;
        }

        public static Image LoadImage(string source)
        {
            return Image.FromFile(source);
        }
    }
}
