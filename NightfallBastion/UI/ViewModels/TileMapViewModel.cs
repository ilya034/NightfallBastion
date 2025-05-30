namespace NightfallBastion.UI.ViewModels
{
    public class TileMapViewModel
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public TileViewModel[] Tiles { get; set; }

        public TileMapViewModel(int width, int height, TileViewModel[] tiles)
        {
            Width = width;
            Height = height;
            Tiles = tiles;
        }

        public TileMapViewModel()
        {
            Tiles = new TileViewModel[0];
        }
    }
}
