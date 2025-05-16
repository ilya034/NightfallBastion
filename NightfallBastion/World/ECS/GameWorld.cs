using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace NightfallBastion.World
{
    public class GameWorld
    {
        private int _nextEntityId = 0;
        private readonly List<Entity> _entities = [];
        private readonly List<System> _systems = [];
        private readonly Dictionary<Type, Dictionary<Entity, Component>> _components = [];

        public void CreateEntity()
        {
            var entity = new Entity(_nextEntityId++);
            _entities.Add(entity);
        }

        public void DestroyEntity(Entity entity)
        {
            if (_entities.Remove(entity))
                foreach (var component in _components)
                    component.Value.Remove(entity);
        }

        public void AddComponent<T>(Entity entity, T component)
            where T : Component
        {
            var type = typeof(T);
            if (!_components.ContainsKey(type))
                _components[type] = new Dictionary<Entity, Component>();

            _components[type][entity] = component;
        }

        public void RemoveComponent<T>(Entity entity)
            where T : Component
        {
            if (_components.TryGetValue(typeof(T), out var entityComponents))
                entityComponents.Remove(entity);
        }

        public T? GetComponent<T>(Entity entity)
            where T : Component
        {
            if (
                _components.TryGetValue(typeof(T), out var entityComponents)
                && entityComponents.TryGetValue(entity, out var component)
            )
                return (T)component;

            return null;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var system in _systems)
                system.Update(gameTime);
        }
    }
}
