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

    public struct DamageComp : IComponent
    {
        public int damage;
    }

    public struct PathfindingComp : IComponent
    {
        public List<Vector2> path;
    }
}
