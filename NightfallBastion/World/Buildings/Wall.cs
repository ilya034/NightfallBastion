using Microsoft.Xna.Framework;

namespace NightfallBastion.World.Buildings
{
    public class Wall(Rectangle sourceRect, int health) : Building(sourceRect, true, health)
    {
        protected override void OnDestroyed() { }
    }

    public class WeakWall(Rectangle sourceRect) : Wall(sourceRect, DefaultHealth)
    {
        public const int DefaultHealth = 50;
    }

    public class StandardWall(Rectangle sourceRect) : Wall(sourceRect, DefaultHealth)
    {
        public const int DefaultHealth = 100;
    }

    public class MediumWall(Rectangle sourceRect) : Wall(sourceRect, DefaultHealth)
    {
        public const int DefaultHealth = 200;
    }

    public class StrongWall(Rectangle sourceRect) : Wall(sourceRect, DefaultHealth)
    {
        public const int DefaultHealth = 400;
    }
}
