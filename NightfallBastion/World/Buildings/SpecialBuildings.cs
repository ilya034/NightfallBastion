using System;
using Microsoft.Xna.Framework;

namespace NightfallBastion.World.Buildings
{
    public class EmptySpace(Rectangle sourceRect) : Building(sourceRect, false, 0) { }

    public class EnemySpawn(Rectangle sourceRect) : Building(sourceRect, false, 0) { }

    public class PlayerCore(Rectangle sourceRect, int MaxHealth = 1000)
        : Building(sourceRect, false, MaxHealth)
    {
        protected override void OnDestroyed() => Console.WriteLine("Game over.");
    }

    public class Turret(
        Rectangle sourceRect,
        int MaxHealth = 100,
        int damage = 10,
        int range = 5,
        float attackSpeed = 1.0f
    ) : Building(sourceRect, true, MaxHealth)
    {
        public int Damage { get; private set; } = damage;
        public int Range { get; private set; } = range;
        public float AttackSpeed { get; private set; } = attackSpeed;

        // public void Attack(Enemy target) { ... }
    }

    public class Mine(Rectangle sourceRect, int MaxHealth = 10, int explosionRadius = 2)
        : Building(sourceRect, false, MaxHealth)
    {
        public int ExplosionRadius { get; private set; } = explosionRadius;
    }
}
