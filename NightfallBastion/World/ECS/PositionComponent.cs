using Microsoft.Xna.Framework;

namespace NightfallBastion.World
{
    public class PositionComponent(Vector2 position) : Component
    {
        public Vector2 Position { get; set; } = position;
        public Vector2 PreviousPosition { get; set; } = position;
    }
}
