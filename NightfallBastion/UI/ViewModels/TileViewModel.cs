using Microsoft.Xna.Framework;

namespace NightfallBastion.UI.ViewModels
{
    public class TileViewModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int TileType { get; set; }
        public Rectangle TextureRegion { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public TileViewModel(int x, int y, int tileType, Rectangle textureRegion)
        {
            X = x;
            Y = y;
            TileType = tileType;
            TextureRegion = textureRegion;
        }

        public TileViewModel() { }
    }
}
