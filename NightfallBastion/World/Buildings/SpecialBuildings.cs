using System;
using Microsoft.Xna.Framework;

namespace NightfallBastion.World.Buildings
{
    public class EnemySpawn(Rectangle sourceRect) : Building(sourceRect, false, 0) { }

    public class PlayerCore(Rectangle sourceRect) : Building(sourceRect, false, DefaultHealth)
    {
        public const int DefaultHealth = 1000;

        protected override void OnDestroyed() => Console.WriteLine("Game over.");
    }

    public class EmptySpace(Rectangle sourceRect) : Building(sourceRect, false, 0) { }

    public class Turret(
        Rectangle sourceRect,
        int damage = 10,
        int range = 3,
        float attackSpeed = 1.0f
    ) : Building(sourceRect, true, DefaultHealth)
    {
        public const int DefaultHealth = 150;
        public int Damage { get; private set; } = damage;
        public int Range { get; private set; } = range;
        public float AttackSpeed { get; private set; } = attackSpeed;

        // public void Attack(Enemy target) { ... } ??
    }
}
