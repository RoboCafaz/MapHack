using System;
using System.Diagnostics;
using System.IO;

namespace MapHack.Core
{
    public static class ImageProcessor
    {
        public static void Process(ImageProcessorArgs args)
        {
            var image = ImageTools.LoadImage(args.InputImage);
            Debug.WriteLine("Loaded image '" + args.InputImage + "'");

            var maxSupportedZoom = ImageTools.GetMaximumZoom(image, args.TileSize);
            Debug.WriteLine("Image maximum zoom is " + maxSupportedZoom);

            var dimension = DimensionalTools.GetResolutionForZoom(maxSupportedZoom, args.TileSize);
            Debug.WriteLine("Required dimension is " + dimension);

            image = ImageTools.CropImage(image, dimension, dimension, args.BackgroundColor);
            GC.Collect();

            if (args.MaximumZoom > maxSupportedZoom && !args.Upscale)
            {
                args.MaximumZoom = maxSupportedZoom;
            }
            var neededDimension = DimensionalTools.GetResolutionForZoom(args.MaximumZoom, args.TileSize);
            image = ImageTools.ResizeImage(image, neededDimension, neededDimension);
            GC.Collect();

            image.Save(Path.Combine(
                Directory.GetCurrentDirectory(),
                String.Concat(Path.GetFileNameWithoutExtension(args.InputImage), "_resized", Path.GetExtension(args.InputImage))
                ));
        }
    }
}
