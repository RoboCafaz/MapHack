using System;
using System.Drawing;
using System.IO;
using MapHack.Core;
using Mono.Options;

namespace MapHack.Command
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var showHelp = false;
            var options = new ImageProcessorArgs();
            var input = new OptionSet
            {
                {"s|source=", "source image to cut", x => options.InputImage = x},
                {"d|dest=", "destination folder to export to", x => options.OutputDirectory = x},
                {"t|tilesize=", "size of tiles", x => options.TileSize = int.Parse(x)},
                {"f|folders=", "use folder format", x => options.UseFolders = x != null},
                {"u|upscale", "upscale images to fit maximum zoom levels", x => options.Upscale = x != null},
                {"m|min=", "minimum zoom level (0-21)", x => options.MinimumZoom = int.Parse(x)},
                {"x|max=", "maximum zoom level (0-21)", x => options.MaximumZoom = int.Parse(x)},
                {"c|color=", "color to pad images", x => options.BackgroundColor = Color.FromName(x)},
                {"h|help", "show this message and exit", x => showHelp = x != null}
            };

            try
            {
                input.Parse(args);
                if (showHelp || options.InputImage == null)
                {
                    var sw = new StringWriter();
                    input.WriteOptionDescriptions(sw);
                    Console.Write(sw.ToString());
                    return;
                }
                ImageProcessor.Process(options);
            }
            catch (OptionException e)
            {
                Console.Write("Error: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `--help` for more information.");
            }
        }

        private static void ShowHelp()
        {
        }
    }
}