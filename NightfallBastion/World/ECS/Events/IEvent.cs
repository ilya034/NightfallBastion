using System;

namespace NightfallBastion.World.ECS.Events
{
    public interface IEvent
    {
        DateTime Timestamp { get; }
    }
}