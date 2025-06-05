using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World.Tiles;

public struct TileData
{
    public FloorType FloorType;
    public int BuildingID;

    public float GetTileWalkCost(GameWorld gameWorld)
    {
        if (BuildingID == 0)
            return Floors.Stats[FloorType].WalkCost;

        if (gameWorld.ECSManager.GetComponent<BuildingComp>(BuildingID).IsDestroyable)
            return -1.0f;

        return Floors.Stats[FloorType].WalkCost
            + gameWorld.ECSManager.GetComponent<HealthComp>(BuildingID).CurrentHealth;
    }

    public float GetTileSpeedMultiplayer(GameWorld gameWorld) => 1 / GetTileWalkCost(gameWorld);
}
