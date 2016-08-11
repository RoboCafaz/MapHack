using System;
using MapHack.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MapHack.Test
{
    [TestClass]
    public class DimensionalToolsTest
    {
        [TestMethod]
        public void GetDimensionForZoom_ShouldReturnNumberOfTilesTheZoomHosts()
        {
            Assert.AreEqual(1, DimensionalTools.GetDimensionForZoom(0));
            Assert.AreEqual(2, DimensionalTools.GetDimensionForZoom(1));
            Assert.AreEqual(4, DimensionalTools.GetDimensionForZoom(2));
            Assert.AreEqual(8, DimensionalTools.GetDimensionForZoom(3));
            Assert.AreEqual(16, DimensionalTools.GetDimensionForZoom(4));
            Assert.AreEqual(32, DimensionalTools.GetDimensionForZoom(5));
        }

        [TestMethod]
        public void GetMaximumZoom_ShouldReturnTheZoomLevelThatCanContainThisSize()
        {
            Assert.AreEqual(0, DimensionalTools.GetMaximumZoom(256, 64, 256));
            Assert.AreEqual(1, DimensionalTools.GetMaximumZoom(512, 64, 256));
            Assert.AreEqual(2, DimensionalTools.GetMaximumZoom(1024, 64, 256));
            Assert.AreEqual(3, DimensionalTools.GetMaximumZoom(2048, 64, 256));
            Assert.AreEqual(4, DimensionalTools.GetMaximumZoom(4096, 64, 256));

            Assert.AreEqual(0, DimensionalTools.GetMaximumZoom(64, 250, 256));
            Assert.AreEqual(1, DimensionalTools.GetMaximumZoom(64, 500, 256));
            Assert.AreEqual(2, DimensionalTools.GetMaximumZoom(64, 1000, 256));
            Assert.AreEqual(3, DimensionalTools.GetMaximumZoom(64, 2000, 256));
            Assert.AreEqual(4, DimensionalTools.GetMaximumZoom(64, 4000, 256));
        }

        [TestMethod]
        public void GetMaximumDimension_ShouldReturnTheMaximumBoundsForThisSize()
        {
            Assert.AreEqual(1, DimensionalTools.GetMaximumDimension(256, 64, 256));
            Assert.AreEqual(2, DimensionalTools.GetMaximumDimension(512, 64, 256));
            Assert.AreEqual(4, DimensionalTools.GetMaximumDimension(1024, 64, 256));
            Assert.AreEqual(8, DimensionalTools.GetMaximumDimension(2048, 64, 256));
            Assert.AreEqual(16, DimensionalTools.GetMaximumDimension(4096, 64, 256));

            Assert.AreEqual(1, DimensionalTools.GetMaximumDimension(64, 250, 256));
            Assert.AreEqual(2, DimensionalTools.GetMaximumDimension(64, 500, 256));
            Assert.AreEqual(4, DimensionalTools.GetMaximumDimension(64, 1000, 256));
            Assert.AreEqual(8, DimensionalTools.GetMaximumDimension(64, 2000, 256));
            Assert.AreEqual(16, DimensionalTools.GetMaximumDimension(64, 4000, 256));
        }
        
        [TestMethod]
        public void GetMaximumResolution_ShouldReturnTheMaximumResolutionForThisSize()
        {
            Assert.AreEqual(256, DimensionalTools.GetMaximumResolution(256, 64, 256));
            Assert.AreEqual(512, DimensionalTools.GetMaximumResolution(512, 64, 256));
            Assert.AreEqual(1024, DimensionalTools.GetMaximumResolution(1024, 64, 256));
            Assert.AreEqual(2048, DimensionalTools.GetMaximumResolution(2048, 64, 256));
            Assert.AreEqual(4096, DimensionalTools.GetMaximumResolution(4096, 64, 256));

            Assert.AreEqual(256, DimensionalTools.GetMaximumResolution(64, 250, 256));
            Assert.AreEqual(512, DimensionalTools.GetMaximumResolution(64, 500, 256));
            Assert.AreEqual(1024, DimensionalTools.GetMaximumResolution(64, 1000, 256));
            Assert.AreEqual(2048, DimensionalTools.GetMaximumResolution(64, 2000, 256));
            Assert.AreEqual(4096, DimensionalTools.GetMaximumResolution(64, 4000, 256));
        }
    }
}
