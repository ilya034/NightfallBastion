using Microsoft.Xna.Framework;

namespace NightfallBastion.World
{
    public class MovementComponent(Vector2 velocity, float speed) : Component
    {
        public Vector2 Velocity { get; set; } = velocity;
        public float Speed { get; set; } = speed;
        public Vector2 Target { get; set; }
    }
}
