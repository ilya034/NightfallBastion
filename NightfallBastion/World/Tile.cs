using Microsoft.Xna.Framework;

namespace NightfallBastion.World
{
    public enum TileType
    {
        Empty,
        Wall,
        EnemySpawn,
        PlayerCore,
    }

    public class Tile(Rectangle sourceRect, bool isSolid = false, TileType type = TileType.Empty)
    {
        public Rectangle SourceRect { get; set; } = sourceRect;
        public bool IsSolid { get; set; } = isSolid;
        public TileType Type { get; set; } = type;
    }
}
