using System.Drawing;

namespace MapHack.Core
{
    public class ImageProcessorArgs
    {
        public string InputImage { get; set; }
        public string OutputDirectory { get; set; }
        public int TileSize { get; set; } = 256;
        public bool UseFolders { get; set; }
        public bool Upscale { get; set; }
        public int MinimumZoom { get; set; } = 0;
        public int MaximumZoom { get; set; } = -1;
        public Color BackgroundColor { get; set; } = Color.White;
    }
}
