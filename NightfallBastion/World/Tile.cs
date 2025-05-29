using Microsoft.Xna.Framework;
using NightfallBastion.World.Buildings;

namespace NightfallBastion.World
{
    public class Tile(Building? building = null)
    {
        public Building? Building { get; private set; } = building;

        public bool IsSolid => Building?.IsSolid ?? false;

        public bool IsDestroyed => Building?.IsDestroyed ?? false;

        public Rectangle SourceRect => Building?.SourceRect ?? Rectangle.Empty;

        public int MaxHealth => Building?.MaxHealth ?? 0;

        public int CurrentHealth => Building?.CurrentHealth ?? 0;

        public void SetBuilding(Building? building)
        {
            Building = building;
        }

        public void RemoveBuilding()
        {
            Building = null;
        }

        public bool TakeDamage(int damage)
        {
            return Building?.TakeDamage(damage) ?? false;
        }

        public bool Repair(int amount)
        {
            return Building?.Repair(amount) ?? false;
        }

        public Color GetRenderColor()
        {
            return Building?.GetRenderColor() ?? Color.White;
        }
    }
}
