using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace NightfallBastion.World
{
    public class ECSManager
    {
        private int _nextEntityId = 0;
        private readonly List<Entity> _entities = [];
        private readonly List<System> _systems = [];
        private readonly Dictionary<Type, Dictionary<Entity, Component>> _components = [];

        public void Dispose()
        {
            _entities.Clear();
            _systems.Clear();
            _components.Clear();
        }

        public Entity CreateEntity()
        {
            var entity = new Entity(_nextEntityId++);
            _entities.Add(entity);
            return entity;
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

        public IEnumerable<Entity> GetEntitiesWithComponents<T1>()
            where T1 : Component
        {
            if (!_components.TryGetValue(typeof(T1), out var components1))
                yield break;

            foreach (var entity in components1.Keys)
                yield return entity;
        }

        public IEnumerable<Entity> GetEntitiesWithComponents<T1, T2>()
            where T1 : Component
            where T2 : Component
        {
            if (
                !_components.TryGetValue(typeof(T1), out var components1)
                || !_components.TryGetValue(typeof(T2), out var components2)
            )
                yield break;

            foreach (var entity in components1.Keys)
                if (components2.ContainsKey(entity))
                    yield return entity;
        }

        public IEnumerable<Entity> GetEntitiesWithComponents<T1, T2, T3>()
            where T1 : Component
            where T2 : Component
            where T3 : Component
        {
            if (
                !_components.TryGetValue(typeof(T1), out var components1)
                || !_components.TryGetValue(typeof(T2), out var components2)
                || !_components.TryGetValue(typeof(T3), out var components3)
            )
                yield break;

            foreach (var entity in components1.Keys)
                if (components2.ContainsKey(entity) && components3.ContainsKey(entity))
                    yield return entity;
        }

        public void RemoveEntity(Entity entity) => DestroyEntity(entity);

        public void AddSystem(System system) => _systems.Add(system);

        public void RemoveSystem(System system) => _systems.Remove(system);

        public void Update(GameTime gameTime)
        {
            foreach (var system in _systems)
                system.Update(gameTime);
        }
    }
}
