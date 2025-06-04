using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World
{
    public class ECSManager
    {
        private int _nextEntityId = 1;
        private readonly List<Entity> _entities = [];
        private readonly List<System> _systems = [];
        private readonly Dictionary<Type, Dictionary<Entity, IComponent>> _components = [];

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
            where T : IComponent
        {
            var type = typeof(T);
            if (!_components.ContainsKey(type))
                _components[type] = new Dictionary<Entity, IComponent>();

            _components[type][entity] = component;
        }

        public void RemoveComponent<T>(Entity entity)
            where T : IComponent
        {
            if (_components.TryGetValue(typeof(T), out var entityComponents))
                entityComponents.Remove(entity);
        }

        public T? GetComponent<T>(Entity entity)
            where T : IComponent
        {
            if (
                _components.TryGetValue(typeof(T), out var entityComponents)
                && entityComponents.TryGetValue(entity, out var component)
            )
                return (T)component;

            return default;
        }

        public IEnumerable<Entity> GetEntitiesWithComponents(params Type[] componentTypes)
        {
            if (componentTypes == null || componentTypes.Length == 0)
                yield break;

            List<HashSet<Entity>> entitySets = new();
            foreach (var type in componentTypes)
            {
                if (!_components.TryGetValue(type, out var components))
                    yield break;
                entitySets.Add(new HashSet<Entity>(components.Keys));
            }

            var intersection = entitySets[0];
            for (int i = 1; i < entitySets.Count; i++)
                intersection.IntersectWith(entitySets[i]);

            foreach (var entity in intersection)
                yield return entity;
        }

        public IEnumerable<Entity> GetEntitiesWithComponents<T1>()
            where T1 : IComponent
        {
            return GetEntitiesWithComponents(typeof(T1));
        }

        public IEnumerable<Entity> GetEntitiesWithComponents<T1, T2>()
            where T1 : IComponent
            where T2 : IComponent
        {
            return GetEntitiesWithComponents(typeof(T1), typeof(T2));
        }

        public IEnumerable<Entity> GetEntitiesWithComponents<T1, T2, T3>()
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
        {
            return GetEntitiesWithComponents(typeof(T1), typeof(T2), typeof(T3));
        }

        public IEnumerable<Entity> GetEntitiesWithComponents<T1, T2, T3, T4>()
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
        {
            return GetEntitiesWithComponents(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        public IEnumerable<Entity> GetEntitiesWithComponents<T1, T2, T3, T4, T5>()
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
            where T5 : IComponent
        {
            return GetEntitiesWithComponents(
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5)
            );
        }

        public void RemoveEntity(Entity entity) => DestroyEntity(entity);

        public void AddSystem(System system) => _systems.Add(system);

        public void RemoveSystem(System system) => _systems.Remove(system);

        public void Update(GameTime gameTime)
        {
            foreach (var system in _systems)
                system.Update(gameTime);
        }

        public void Dispose()
        {
            _entities.Clear();
            _systems.Clear();
            _components.Clear();
        }
    }
}
