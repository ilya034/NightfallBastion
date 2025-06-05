namespace NightfallBastion.World.Tiles;

public struct TileData
{
    public FloorType FloorType;
    public int BuildingID;

    public float GetTileWalkCost()
    {
        if (BuildingID > 0)
            return float.MaxValue;

        return Floors.Stats[FloorType].WalkCost;
    }

    public float GetTileSpeedMultiplayer() => 1 / GetTileWalkCost();
}
