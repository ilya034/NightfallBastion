using System;
using NightfallBastion.World.Buildings;
using NightfallBastion.World.Floors;

namespace NightfallBastion.World
{
    public class Tile(Floor floor, Building? building = null)
    {
        public Floor Floor { get; } = floor ?? throw new ArgumentNullException(nameof(floor));

        public Building? Building { get; set; } = building;
    }
}
