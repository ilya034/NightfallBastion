namespace NightfallBastion.World
{
    public enum Floor
    {
        Space,
        DefaultFloor,
        SlowdownFloor,
        DamageFloor,
    }

    public struct TileData
    {
        public Floor floor;
        public int BuildingID;
        public float speedMultiplier;
    }
}
