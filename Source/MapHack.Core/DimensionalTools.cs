using System;

namespace MapHack.Core
{
    public static class DimensionalTools
    {
        public static int GetDimensionForZoom(int zoom)
        {
            return (int)Math.Pow(2, zoom);
        }

        public static int GetResolutionForZoom(int zoom, int tileSize)
        {
            return GetDimensionForZoom(zoom) * tileSize;
        }

        public static int GetMaximumZoom(int width, int height, int tileSize)
        {
            var dimension = Math.Max(width, height);
            var tiles = Math.Ceiling((double)dimension / tileSize);
            var maximum = 0;
            // TODO: There's definitely a better way to do this.
            while (Math.Pow(2, maximum) < tiles)
            {
                maximum++;
            }
            return maximum;
        }

        public static int GetMaximumDimension(int width, int height, int tileSize)
        {
            return GetDimensionForZoom(GetMaximumZoom(width, height, tileSize));
        }

        public static int GetMaximumResolution(int width, int height, int tileSize)
        {
            return GetResolutionForZoom(GetMaximumZoom(width, height, tileSize), tileSize);
        }
    }
}
