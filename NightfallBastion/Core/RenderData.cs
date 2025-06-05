using Microsoft.Xna.Framework;
using NightfallBastion.World;

namespace NightfallBastion.Core
{
    public struct RenderData
    {
        public Matrix CameraTransform;
        public TileMapRenderData TileMapData;
        public BuildingRenderData[] BuildingData;
        public EnemyRenderData[] EnemyData;
    }

    public struct TileMapRenderData
    {
        public TileRenderData[,] Tiles;
        public int Width;
        public int Height;
    }

    public struct TileRenderData
    {
        public FloorType FloorType;
        public float Distance;
    }

    public struct BuildingRenderData
    {
        public BuildingType Type;
        public Vector2 Position;
        public float Health;
        public float MaxHealth;
    }

    public struct EnemyRenderData
    {
        public EnemyType Type;
        public Vector2 Position;
        public float Health;
        public float MaxHealth;
    }
}
