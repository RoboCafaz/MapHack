using System;
using System.Diagnostics;
using System.IO;
using ImageMagick;

namespace MapHack.Core
{
    public static class ImageProcessor
    {
        public static void Process(ImageProcessorArgs args)
        {
            var image = PrepareImage(args);
            if (image == null)
            {
                return;
            }

            var temporaryImage = Path.GetTempFileName();
            image.Write(temporaryImage);
            args.InputImage = temporaryImage;

            for (var i = args.MinimumZoom; i <= args.MaximumZoom; i++)
            {
                ChopLayer(args, i);
            }

            File.Delete(temporaryImage);
        }

        private static MagickImage PrepareImage(ImageProcessorArgs args)
        {
            var image = ImageTools.LoadImage(args.InputImage);

            var maxSupportedZoom = ImageTools.GetMaximumZoom(image, args.TileSize);
            if (args.MaximumZoom == -1)
            {
                args.MaximumZoom = maxSupportedZoom;
            }
            if (args.MinimumZoom > args.MaximumZoom)
            {
                Console.WriteLine($"The minimum zoom ({args.MinimumZoom}) is larger than the maximum zoom for this image ({args.MaximumZoom}). Please try different values.");
                return null;
            }

            var dimension = DimensionalTools.GetResolutionForZoom(args.MaximumZoom, args.TileSize);

            if (args.Upscale || args.MaximumZoom < maxSupportedZoom)
            {
                int width = dimension;
                int height = dimension;
                if (image.Width > image.Height)
                {
                    var aspect = (double)image.Height / image.Width;
                    height = (int)(dimension * aspect);
                }
                else
                {
                    var aspect = (double)image.Width / image.Height;
                    width = (int)(dimension * aspect);
                }
                image = ImageTools.ResizeImage(image, width, height);
            }
            image = ImageTools.CropImage(image, dimension, dimension, args.BackgroundColor);
            GC.Collect();

            var neededDimension = DimensionalTools.GetResolutionForZoom(args.MaximumZoom, args.TileSize);
            image = ImageTools.ResizeImage(image, neededDimension, neededDimension);
            GC.Collect();

            return image;
        }

        private static void ChopLayer(ImageProcessorArgs args, int zoom)
        {
            var image = ImageTools.LoadImage(args.InputImage);
            var neededDimension = DimensionalTools.GetDimensionForZoom(zoom);
            var neededResolution = DimensionalTools.GetResolutionForZoom(zoom, args.TileSize);
            image = ImageTools.ResizeImage(image, neededResolution, neededResolution);
            var tiles = ImageTools.CutTiles(image, args.TileSize);
            ImageTools.SaveTiles(tiles, zoom, neededDimension, args.OutputDirectory, args.UseFolders);
        }
    }
}
