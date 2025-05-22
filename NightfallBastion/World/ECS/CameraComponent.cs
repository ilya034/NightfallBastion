using Microsoft.Xna.Framework;

namespace NightfallBastion.World
{
    public class CameraComponent : Component
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public float Zoom { get; set; } = 1f;
        public float Rotation { get; set; } = 0f;
    }
}
