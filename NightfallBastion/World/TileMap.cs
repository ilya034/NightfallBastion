using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NightfallBastion.World
{
    public class TileMap(int width, int height, int tileSize)
    {
        public int Width { get; } = width;
        public int Height { get; } = height;
        public int TileSize { get; } = tileSize;
        private readonly Tile[,] _tiles = new Tile[width, height];

        public void SetTile(int x, int y, Tile tile)
        {
            _tiles[x, y] = tile;
        }

        public Tile GetTile(int x, int y)
        {
            return _tiles[x, y];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var tile = _tiles[x, y];
                    if (tile != null)
                        spriteBatch.Draw(
                            tile.Texture,
                            new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize),
                            tile.SourceRect,
                            Color.White
                        );
                }
            }
        }
    }
}
