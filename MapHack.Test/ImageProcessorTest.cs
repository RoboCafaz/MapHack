using System;
using MapHack.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MapHack.Test
{
    [TestClass]
    public class ImageProcessorTest
    {
        [TestMethod]
        public void GetDimensionForZoom_ShouldReturnNumberOfTilesTheZoomHosts()
        {
            Assert.AreEqual(1, ImageProcessor.GetDimensionForZoom(0));
            Assert.AreEqual(2, ImageProcessor.GetDimensionForZoom(1));
            Assert.AreEqual(4, ImageProcessor.GetDimensionForZoom(2));
            Assert.AreEqual(8, ImageProcessor.GetDimensionForZoom(3));
            Assert.AreEqual(16, ImageProcessor.GetDimensionForZoom(4));
            Assert.AreEqual(32, ImageProcessor.GetDimensionForZoom(5));
        }

        [TestMethod]
        public void GetMaximumZoom_ShouldReturnTheZoomLevelThatCanContainTheZoomLevel()
        {
            Assert.AreEqual(0, ImageProcessor.GetMaximumZoom(256, 64, 256));
            Assert.AreEqual(1, ImageProcessor.GetMaximumZoom(512, 64, 256));
            Assert.AreEqual(2, ImageProcessor.GetMaximumZoom(1024, 64, 256));
            Assert.AreEqual(3, ImageProcessor.GetMaximumZoom(2048, 64, 256));
            Assert.AreEqual(4, ImageProcessor.GetMaximumZoom(4096, 64, 256));

            Assert.AreEqual(0, ImageProcessor.GetMaximumZoom(64, 250, 256));
            Assert.AreEqual(1, ImageProcessor.GetMaximumZoom(64, 500, 256));
            Assert.AreEqual(2, ImageProcessor.GetMaximumZoom(64, 1000, 256));
            Assert.AreEqual(3, ImageProcessor.GetMaximumZoom(64, 2000, 256));
            Assert.AreEqual(4, ImageProcessor.GetMaximumZoom(64, 4000, 256));
        }

        [TestMethod]
        public void GetMaximumDimension_ShouldReturnTheMaximumBoundsForThisSize()
        {
            Assert.AreEqual(1, ImageProcessor.GetMaximumDimension(256, 64, 256));
            Assert.AreEqual(2, ImageProcessor.GetMaximumDimension(512, 64, 256));
            Assert.AreEqual(4, ImageProcessor.GetMaximumDimension(1024, 64, 256));
            Assert.AreEqual(8, ImageProcessor.GetMaximumDimension(2048, 64, 256));
            Assert.AreEqual(16, ImageProcessor.GetMaximumDimension(4096, 64, 256));

            Assert.AreEqual(1, ImageProcessor.GetMaximumDimension(64, 250, 256));
            Assert.AreEqual(2, ImageProcessor.GetMaximumDimension(64, 500, 256));
            Assert.AreEqual(4, ImageProcessor.GetMaximumDimension(64, 1000, 256));
            Assert.AreEqual(8, ImageProcessor.GetMaximumDimension(64, 2000, 256));
            Assert.AreEqual(16, ImageProcessor.GetMaximumDimension(64, 4000, 256));
        }
        
        [TestMethod]
        public void GetMaximumResolution_ShouldReturnTheMaximumResolutionForThisSize()
        {
            Assert.AreEqual(256, ImageProcessor.GetMaximumResolution(256, 64, 256));
            Assert.AreEqual(512, ImageProcessor.GetMaximumResolution(512, 64, 256));
            Assert.AreEqual(1024, ImageProcessor.GetMaximumResolution(1024, 64, 256));
            Assert.AreEqual(2048, ImageProcessor.GetMaximumResolution(2048, 64, 256));
            Assert.AreEqual(4096, ImageProcessor.GetMaximumResolution(4096, 64, 256));

            Assert.AreEqual(256, ImageProcessor.GetMaximumResolution(64, 250, 256));
            Assert.AreEqual(512, ImageProcessor.GetMaximumResolution(64, 500, 256));
            Assert.AreEqual(1024, ImageProcessor.GetMaximumResolution(64, 1000, 256));
            Assert.AreEqual(2048, ImageProcessor.GetMaximumResolution(64, 2000, 256));
            Assert.AreEqual(4096, ImageProcessor.GetMaximumResolution(64, 4000, 256));
        }
    }
}
