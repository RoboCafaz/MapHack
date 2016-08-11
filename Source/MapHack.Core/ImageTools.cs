using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using ImageMagick;

namespace MapHack.Core
{
    public static class ImageTools
    {

        public static int GetMaximumZoom(MagickImage image, int tileSize)
        {
            Debug.WriteLine("Calculating maximum zoom of image.");
            return DimensionalTools.GetMaximumZoom(image.Width, image.Height, tileSize);
        }

        public static int GetMaximumDimension(MagickImage image, int tileSize)
        {
            Debug.WriteLine("Calculating maximum dimension of image.");
            return DimensionalTools.GetMaximumDimension(image.Width, image.Height, tileSize);
        }

        public static int GetMaximumResolution(MagickImage image, int tileSize)
        {
            Debug.WriteLine("Calculating maximum resolution of image.");
            return DimensionalTools.GetMaximumResolution(image.Width, image.Height, tileSize);
        }

        public static MagickImage CropImage(MagickImage image, int targetWidth, int targetHeight, Color? padColor = null)
        {
            if (image.Width != targetWidth || image.Height != targetHeight)
            {
                if (padColor.HasValue)
                {
                    Debug.WriteLine($"Setting background color of image to {padColor.Value}");
                    image.BackgroundColor = new MagickColor(padColor.Value);
                }
                Debug.WriteLine($"Cropping image to {targetWidth}x{targetHeight}...");
                image.Extent(targetWidth, targetHeight, Gravity.Center);
            }
            else
            {
                Debug.WriteLine($"Skipping image crop to {targetWidth}x{targetHeight}. Image is already the ideal size.");
            }
            return image;
        }

        public static MagickImage ResizeImage(MagickImage image, int targetWidth, int targetHeight)
        {
            if (image.Width != targetWidth || image.Height != targetHeight)
            {
                Debug.WriteLine($"Resizing image to {targetWidth}x{targetHeight}...");
                image.Resize(targetWidth, targetHeight);
            }
            else
            {
                Debug.WriteLine($"Skipping image resize to {targetWidth}x{targetHeight}. Image is already the ideal size.");
            }
            return image;
        }

        public static MagickImage LoadImage(string source)
        {
            Debug.WriteLine($"Loading image '{source}'.");
            return new MagickImage(new FileInfo(source));
        }

        public static IEnumerable<MagickImage> CutTiles(MagickImage image, int tileSize)
        {
            Debug.WriteLine($"Cutting image to tiles of {tileSize}x{tileSize}");
            return image.CropToTiles(tileSize, tileSize);
        }

        public static void SaveTiles(IEnumerable<MagickImage> tiles, int zoom, int dimension, string outputDirectory, bool folders)
        {
            var x = 0;
            var y = 0;
            if (outputDirectory == null)
            {
                outputDirectory = Directory.GetCurrentDirectory();
            }
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            foreach (var tile in tiles)
            {
                string directory;
                string filename;
                if (folders)
                {
                    directory = Path.Combine(outputDirectory, zoom.ToString(), x.ToString());
                    filename = y.ToString();
                }
                else
                {
                    directory = outputDirectory;
                    filename = String.Join("_", zoom, x, y);
                }
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                var path = Path.Combine(directory, String.Concat(filename, ".", tile.Format.ToString()));
                tile.Write(path);
                x++;
                if (x == dimension)
                {
                    x = 0;
                    y++;
                }
            }
        }
    }
}
