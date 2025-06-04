using Microsoft.Xna.Framework;
using NightfallBastion.World;

namespace NightfallBastion.Core
{
    public struct RenderData
    {
        public Matrix cameraTransform;
        public TileMapRenderData tileMapData;
        public BuildingRenderData[] buildingData;
        public EnemyRenderData[] enemyData;
    }

    public struct TileMapRenderData
    {
        public TileRenderData[,] tiles;
        public int width;
        public int height;
    }

    public struct TileRenderData
    {
        public Floor floor;
    }

    public struct BuildingRenderData
    {
        public BuildingType type;
        public Vector2 position;
        public float Health;
        public float MaxHealth;
    }

    public struct EnemyRenderData
    {
        public EnemyType type;
        public Vector2 position;
        public float health;
        public float maxHealth;
    }
}
