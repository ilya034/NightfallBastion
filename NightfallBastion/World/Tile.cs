using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NightfallBastion.World
{
    public class Tile(Texture2D texture, Rectangle sourceRect, bool isSolid = false)
    {
        public Texture2D Texture { get; set; } = texture;
        public Rectangle SourceRect { get; set; } = sourceRect;
        public bool IsSolid { get; set; } = isSolid;
    }
}
