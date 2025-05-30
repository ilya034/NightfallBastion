using Microsoft.Xna.Framework;

namespace NightfallBastion.World
{
    public class DamageComponent(Point tilePosition, int damageAmount) : Component
    {
        public Point TilePosition { get; set; } = tilePosition;

        public int DamageAmount { get; set; } = damageAmount;

        public bool IsApplied { get; set; } = false;
    }
}
