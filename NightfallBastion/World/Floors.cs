using System.Collections.Generic;

namespace NightfallBastion.World
{
    public static class Floors
    {
        public static readonly Dictionary<FloorType, FloorStats> Stats = new()
        {
            { FloorType.Space, new FloorStats("Space", 0.0f) },
            { FloorType.DefaultFloor, new FloorStats("Default Floor", 1.0f) },
            { FloorType.SlowdownFloor, new FloorStats("Slowdown Floor", 2.0f) },
            { FloorType.DamageFloor, new FloorStats("Damage Floor", 3.0f) },
        };
    }

    public struct FloorStats(string name, float walkCost)
    {
        public string Name = name;
        public float WalkCost = walkCost;
    }

    public enum FloorType
    {
        Space,
        DefaultFloor,
        SlowdownFloor,
        DamageFloor,
    }
}
