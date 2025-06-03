using System;
using System.Collections.Generic;

namespace NightfallBastion.World.ECS.Events
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<Action<IEvent>>> _subscribers = new();

        public void Subscribe<T>(Action<T> handler) where T : IEvent
        {
            var eventType = typeof(T);
            if (!_subscribers.ContainsKey(eventType))
                _subscribers[eventType] = new List<Action<IEvent>>();

            _subscribers[eventType].Add(evt => handler((T)evt));
        }

        public void Publish<T>(T eventData) where T : IEvent
        {
            var eventType = typeof(T);
            if (_subscribers.TryGetValue(eventType, out var handlers))
            {
                foreach (var handler in handlers)
                    handler(eventData);
            }
        }

        public void Unsubscribe<T>(Action<T> handler) where T : IEvent
        {
            var eventType = typeof(T);
            if (_subscribers.TryGetValue(eventType, out var handlers))
            {
                // Для упрощения пока не реализуем отписку
                // В реальном проекте нужно сохранять ссылки на обработчики
            }
        }

        public void Clear()
        {
            _subscribers.Clear();
        }
    }
}