namespace NightfallBastion.World
{
    public enum Floor
    {
        space,
        defaultFloor,
        slowdownFloor,
        damageFloor,
    }

    public struct TileData
    {
        public Floor floor;
        public int BuildingID;
        public float speedMultiplier;
    }
}
