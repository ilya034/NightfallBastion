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
    }
}
