using System.Collections.Generic;
using Microsoft.Xna.Framework;
using NightfallBastion.World.Tiles;

namespace NightfallBastion.World.ECS.Components
{
    public interface IComponent { }

    public struct PositionComp : IComponent
    {
        public Vector2 Position;
    }

    public struct TilePositionComp : IComponent
    {
        public Vector2 Position;
    }

    public struct MovementComp : IComponent
    {
        public Vector2 NextPosition;
        public bool IsMoving;
        public float Speed;
    }

    public struct DistanceMapComp : IComponent
    {
        public float[,] Distances;
    }

    public struct HealthComp : IComponent
    {
        public float MaxHealth;
        public float CurrentHealth;
    }

    public struct TeamComp : IComponent
    {
        public Team Team;
    }

    public struct PhysicsComp : IComponent
    {
        public float Mass;
        public bool IsSolid;
        public Rectangle Hitbox;
    }

    public interface IWeaponComp : IComponent { }

    public struct KamikazeWeaponComp : IWeaponComp
    {
        public float Damage;
        public float DamageRadius;
        public float SenseRadius;
    }

    public struct EnemyComp : IComponent
    {
        public EnemyType Type;
    }

    public struct BuildingComp : IComponent
    {
        public BuildingType Type;
        public bool IsDestroyable;
    }

    public struct CoreMarkComp : IComponent { }

    public struct DamageComp : IComponent
    {
        public float Damage;
    }

    public struct TileMapComp : IComponent
    {
        public int Width;
        public int Height;
        public TileData[,] TileMap;
    }

    public struct SpawnPointComp : IComponent
    {
        public Dictionary<EnemyType, float> EnemyTypes;
        public float SpawnInterval;
        public float Timer;
    }
}
