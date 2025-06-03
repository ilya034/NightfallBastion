using System;
using Microsoft.Xna.Framework;

namespace NightfallBastion.World.ECS.Events
{
    public class EntityCreatedEvent : IEvent
    {
        public DateTime Timestamp { get; } = DateTime.Now;
        public Entity Entity { get; }
        public Vector2 Position { get; }
        public string EntityType { get; }

        public EntityCreatedEvent(Entity entity, Vector2 position, string entityType)
        {
            Entity = entity;
            Position = position;
            EntityType = entityType;
        }
    }

    public class EntityDestroyedEvent : IEvent
    {
        public DateTime Timestamp { get; } = DateTime.Now;
        public Entity Entity { get; }

        public EntityDestroyedEvent(Entity entity)
        {
            Entity = entity;
        }
    }

    public class ComponentChangedEvent : IEvent
    {
        public DateTime Timestamp { get; } = DateTime.Now;
        public Entity Entity { get; }
        public Type ComponentType { get; }

        public ComponentChangedEvent(Entity entity, Type componentType)
        {
            Entity = entity;
            ComponentType = componentType;
        }
    }

    public class HealthChangedEvent : IEvent
    {
        public DateTime Timestamp { get; } = DateTime.Now;
        public Entity Entity { get; }
        public float NewHealth { get; }
        public float MaxHealth { get; }

        public HealthChangedEvent(Entity entity, float newHealth, float maxHealth)
        {
            Entity = entity;
            NewHealth = newHealth;
            MaxHealth = maxHealth;
        }
    }

    public class CameraChangedEvent : IEvent
    {
        public DateTime Timestamp { get; } = DateTime.Now;
        public Vector2 Position { get; }
        public float Zoom { get; }

        public CameraChangedEvent(Vector2 position, float zoom)
        {
            Position = position;
            Zoom = zoom;
        }
    }

    public class TileMapChangedEvent : IEvent
    {
        public DateTime Timestamp { get; } = DateTime.Now;
        public Rectangle ChangedRegion { get; }

        public TileMapChangedEvent(Rectangle changedRegion)
        {
            ChangedRegion = changedRegion;
        }
    }
}