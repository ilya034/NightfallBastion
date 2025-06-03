using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace NightfallBastion.World.ECS.Components
{
    public interface IComponent { }

    public struct PositionComp : IComponent
    {
        public Vector2 position;
    }

    public struct TilePositionComp : IComponent
    {
        public Vector2 tilePosition;
    }

    public struct MovementComp : IComponent
    {
        public Vector2 nextPosition;
        public bool isMoving;
        public float speed;
    }

    public struct TargetComp : IComponent
    {
        public Vector2 target;
    }

    public struct HealthComp : IComponent
    {
        public int maxHealth;
        public int currentHealth;
    }

    public struct TeamComp : IComponent
    {
        public Team team;
    }

    public struct PhysicsComp : IComponent
    {
        public int mass;
        public bool isSolid;
        public Rectangle hitbox;
    }

    public struct WeaponComp : IComponent
    {
        public WeaponType type;
    }

    public struct EnemyComp : IComponent
    {
        public EnemyType type;
    }

    public struct DamageComp : IComponent
    {
        public int damage;
    }

    public struct TileMapComp : IComponent
    {
        public int width;
        public int height;
        public TileData[,] tileMap;
    }

    public struct PathfindingComp : IComponent
    {
        public List<Vector2> path;
    }
}
