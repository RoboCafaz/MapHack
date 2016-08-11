using System.Drawing;
using System.Drawing.Imaging;
using MapHack.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MapHack.Test
{
    [TestClass]
    public class ImageToolsTest
    {
        [TestMethod]
        public void GetMaximumZoom_ShouldReturnTheZoomLevelThatCanContainThisSize()
        {
            Assert.AreEqual(0, ImageTools.GetMaximumZoom(CreateImage(256, 64), 512));
            Assert.AreEqual(0, ImageTools.GetMaximumZoom(CreateImage(512, 64), 512));
            Assert.AreEqual(1, ImageTools.GetMaximumZoom(CreateImage(1024, 64), 512));
            Assert.AreEqual(2, ImageTools.GetMaximumZoom(CreateImage(2048, 64), 512));
            Assert.AreEqual(3, ImageTools.GetMaximumZoom(CreateImage(4096, 64), 512));

            Assert.AreEqual(0, ImageTools.GetMaximumZoom(CreateImage(64, 250), 512));
            Assert.AreEqual(0, ImageTools.GetMaximumZoom(CreateImage(64, 500), 512));
            Assert.AreEqual(1, ImageTools.GetMaximumZoom(CreateImage(64, 1000), 512));
            Assert.AreEqual(2, ImageTools.GetMaximumZoom(CreateImage(64, 2000), 512));
            Assert.AreEqual(3, ImageTools.GetMaximumZoom(CreateImage(64, 4000), 512));
        }

        [TestMethod]
        public void GetMaximumDimension_ShouldReturnTheMaximumBoundsForThisSize()
        {
            Assert.AreEqual(1, ImageTools.GetMaximumDimension(CreateImage(256, 64), 512));
            Assert.AreEqual(1, ImageTools.GetMaximumDimension(CreateImage(512, 64), 512));
            Assert.AreEqual(2, ImageTools.GetMaximumDimension(CreateImage(1024, 64), 512));
            Assert.AreEqual(4, ImageTools.GetMaximumDimension(CreateImage(2048, 64), 512));
            Assert.AreEqual(8, ImageTools.GetMaximumDimension(CreateImage(4096, 64), 512));

            Assert.AreEqual(1, ImageTools.GetMaximumDimension(CreateImage(64, 250), 512));
            Assert.AreEqual(1, ImageTools.GetMaximumDimension(CreateImage(64, 500), 512));
            Assert.AreEqual(2, ImageTools.GetMaximumDimension(CreateImage(64, 1000), 512));
            Assert.AreEqual(4, ImageTools.GetMaximumDimension(CreateImage(64, 2000), 512));
            Assert.AreEqual(8, ImageTools.GetMaximumDimension(CreateImage(64, 4000), 512));
        }

        [TestMethod]
        public void GetMaximumResolution_ShouldReturnTheMaximumResolutionForThisSize()
        {
            Assert.AreEqual(512, ImageTools.GetMaximumResolution(CreateImage(256, 64), 512));
            Assert.AreEqual(512, ImageTools.GetMaximumResolution(CreateImage(512, 64), 512));
            Assert.AreEqual(1024, ImageTools.GetMaximumResolution(CreateImage(1024, 64), 512));
            Assert.AreEqual(2048, ImageTools.GetMaximumResolution(CreateImage(2048, 64), 512));
            Assert.AreEqual(4096, ImageTools.GetMaximumResolution(CreateImage(4096, 64), 512));

            Assert.AreEqual(512, ImageTools.GetMaximumResolution(CreateImage(64, 250), 512));
            Assert.AreEqual(512, ImageTools.GetMaximumResolution(CreateImage(64, 500), 512));
            Assert.AreEqual(1024, ImageTools.GetMaximumResolution(CreateImage(64, 1000), 512));
            Assert.AreEqual(2048, ImageTools.GetMaximumResolution(CreateImage(64, 2000), 512));
            Assert.AreEqual(4096, ImageTools.GetMaximumResolution(CreateImage(64, 4000), 512));
        }

        [TestMethod]
        public void CropImage_WithLargerSize_ShouldPadTheImageWithInputColor()
        {
            var bitmap = CreateImage(256, 256) as Bitmap;
            Assert.IsNotNull(bitmap);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(new SolidBrush(Color.Blue), new Rectangle(0, 0, 256, 256));
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(16, 16, 224, 224));
            }

            Assert.AreEqual(Color.Blue.ToArgb(), bitmap.GetPixel(0, 0).ToArgb());
            Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(127, 127).ToArgb());
            Assert.AreEqual(Color.Blue.ToArgb(), bitmap.GetPixel(255, 255).ToArgb());

            bitmap = ImageTools.CropImage(bitmap, 512, 512, Color.Yellow) as Bitmap;
            Assert.IsNotNull(bitmap);
            Assert.AreEqual(512, bitmap.Width);
            Assert.AreEqual(512, bitmap.Height);

            Assert.AreEqual(Color.Yellow.ToArgb(), bitmap.GetPixel(0, 0).ToArgb());
            Assert.AreEqual(Color.Blue.ToArgb(), bitmap.GetPixel(128, 128).ToArgb());
            Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(256, 256).ToArgb());
            Assert.AreEqual(Color.Blue.ToArgb(), bitmap.GetPixel(383, 383).ToArgb());
            Assert.AreEqual(Color.Yellow.ToArgb(), bitmap.GetPixel(511, 511).ToArgb());
        }

        [TestMethod]
        public void CropImage_WithSmallerSize_ShrinkImageCanvas()
        {
            var bitmap = CreateImage(256, 256) as Bitmap;
            Assert.IsNotNull(bitmap);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(0, 0, 256, 256));
                graphics.FillRectangle(new SolidBrush(Color.Yellow), new Rectangle(16, 16, 224, 224));
            }

            Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(0, 0).ToArgb());
            Assert.AreEqual(Color.Yellow.ToArgb(), bitmap.GetPixel(127, 127).ToArgb());
            Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(255, 255).ToArgb());

            bitmap = ImageTools.CropImage(bitmap, 208, 208) as Bitmap;
            Assert.IsNotNull(bitmap);
            Assert.AreEqual(208, bitmap.Width);
            Assert.AreEqual(208, bitmap.Height);

            Assert.AreEqual(Color.Yellow.ToArgb(), bitmap.GetPixel(0, 0).ToArgb());
            Assert.AreEqual(Color.Yellow.ToArgb(), bitmap.GetPixel(63, 63).ToArgb());
            Assert.AreEqual(Color.Yellow.ToArgb(), bitmap.GetPixel(127, 127).ToArgb());
            Assert.AreEqual(Color.Yellow.ToArgb(), bitmap.GetPixel(207, 207).ToArgb());
        }

        [TestMethod]
        public void ResizeImage_WithLargerSize_ShouldInflateTheImage()
        {
            var bitmap = CreateImage(256, 256) as Bitmap;
            Assert.IsNotNull(bitmap);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(0, 0, 256, 256));
                graphics.FillRectangle(new SolidBrush(Color.Blue), new Rectangle(16, 16, 224, 224));
            }

            Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(0, 0).ToArgb());
            Assert.AreEqual(Color.Blue.ToArgb(), bitmap.GetPixel(127, 127).ToArgb());
            Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(255, 255).ToArgb());

            bitmap = ImageTools.ResizeImage(bitmap, 512, 512) as Bitmap;
            Assert.IsNotNull(bitmap);
            Assert.AreEqual(512, bitmap.Width);
            Assert.AreEqual(512, bitmap.Height);

            Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(4, 4).ToArgb());
            Assert.AreEqual(Color.Blue.ToArgb(), bitmap.GetPixel(128, 128).ToArgb());
            Assert.AreEqual(Color.Blue.ToArgb(), bitmap.GetPixel(256, 256).ToArgb());
            Assert.AreEqual(Color.Blue.ToArgb(), bitmap.GetPixel(383, 383).ToArgb());
            Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(508, 508).ToArgb());
        }

        [TestMethod]
        public void ResizeImage_WithSmallerSize_ShouldDeflateTheImage()
        {
            var bitmap = CreateImage(256, 256) as Bitmap;
            Assert.IsNotNull(bitmap);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(0, 0, 256, 256));
                graphics.FillRectangle(new SolidBrush(Color.Blue), new Rectangle(16, 16, 224, 224));
            }

            Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(0, 0).ToArgb());
            Assert.AreEqual(Color.Blue.ToArgb(), bitmap.GetPixel(127, 127).ToArgb());
            Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(255, 255).ToArgb());

            bitmap = ImageTools.ResizeImage(bitmap, 128, 128) as Bitmap;
            Assert.IsNotNull(bitmap);
            Assert.AreEqual(128, bitmap.Width);
            Assert.AreEqual(128, bitmap.Height);

            Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(3, 3).ToArgb());
            Assert.AreEqual(Color.Blue.ToArgb(), bitmap.GetPixel(34, 34).ToArgb());
            Assert.AreEqual(Color.Blue.ToArgb(), bitmap.GetPixel(63, 63).ToArgb());
            Assert.AreEqual(Color.Blue.ToArgb(), bitmap.GetPixel(92, 92).ToArgb());
            Assert.AreEqual(Color.Red.ToArgb(), bitmap.GetPixel(124, 124).ToArgb());
        }

        private Image CreateImage(int width, int height)
        {
            return new Bitmap(width, height, PixelFormat.Format24bppRgb);
        }
    }
}
