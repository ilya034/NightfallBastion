using Microsoft.Xna.Framework;

namespace NightfallBastion.World.Buildings
{
    public class Wall(Rectangle sourceRect, int health) : Building(sourceRect, true, health)
    {
        protected override void OnDestroyed() { }
    }
}
