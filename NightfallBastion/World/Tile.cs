using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NightfallBastion.World
{
    public class Tile(Rectangle sourceRect, bool isSolid = false)
    {
        public Rectangle SourceRect { get; set; } = sourceRect;
        public bool IsSolid { get; set; } = isSolid;
    }
}
