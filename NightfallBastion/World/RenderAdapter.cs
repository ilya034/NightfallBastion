using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World
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

    public class RenderAdapter(GameWorld world)
    {
        private GameWorld _world = world;

        public RenderData GetRenderData()
        {
            var renderData = new RenderData
            {
                CameraTransform = _world.Camera.GetTransformMatrix(),
                TileMapData = GetTileMapRenderData(),
                BuildingData = GetBuildingRenderData(),
                EnemyData = GetEnemyRenderData(),
            };

            return renderData;
        }

        public TileMapRenderData GetTileMapRenderData()
        {
            var tileMapEntity = _world
                .ECSManager.GetEntitiesWithComponents<TileMapComp>()
                .FirstOrDefault();

            var tileMapComp = _world.ECSManager.GetComponent<TileMapComp>(tileMapEntity);
            var distanceMapComp = _world.ECSManager.GetComponent<DistanceMapComp>(tileMapEntity);

            var tiles = new TileRenderData[tileMapComp.Width, tileMapComp.Height];

            for (int x = 0; x < tileMapComp.Width; x++)
            {
                for (int y = 0; y < tileMapComp.Height; y++)
                {
                    var tileData = tileMapComp.TileMap[x, y];

                    var floorTile = new TileRenderData
                    {
                        FloorType = tileData.FloorType,
                        Distance = distanceMapComp.Distances[x, y],
                    };
                    tiles[x, y] = floorTile;
                }
            }

            return new TileMapRenderData
            {
                Tiles = tiles,
                Width = tileMapComp.Width,
                Height = tileMapComp.Height,
            };
        }

        public BuildingRenderData[] GetBuildingRenderData()
        {
            var buildingsRenderData = new List<BuildingRenderData>();

            var buildingEntities = _world.ECSManager.GetEntitiesWithComponents<
                BuildingComp,
                HealthComp
            >();

            foreach (var buildingEntity in buildingEntities)
            {
                var healthComp = _world.ECSManager.GetComponent<HealthComp>(buildingEntity);
                var buildingComp = _world.ECSManager.GetComponent<BuildingComp>(buildingEntity);

                Vector2 position;
                if (
                    _world.ECSManager.TryGetComponent<PositionComp>(
                        buildingEntity,
                        out var positionComp
                    )
                )
                    position = positionComp.Position;
                else
                    position = _world.TileToWorldCentered(
                        _world.ECSManager.GetComponent<TilePositionComp>(buildingEntity).Position
                    );

                var renderEntity = new BuildingRenderData
                {
                    Type = buildingComp.Type,
                    Position = position,
                    Health = healthComp.CurrentHealth,
                    MaxHealth = healthComp.MaxHealth,
                };

                buildingsRenderData.Add(renderEntity);
            }

            return buildingsRenderData.ToArray();
        }

        public EnemyRenderData[] GetEnemyRenderData()
        {
            var entitiesRenderData = new List<EnemyRenderData>();

            var enemyEntities = _world.ECSManager.GetEntitiesWithComponents<
                EnemyComp,
                PositionComp,
                HealthComp
            >();

            foreach (var enemyEntity in enemyEntities)
            {
                var positionComp = _world.ECSManager.GetComponent<PositionComp>(enemyEntity);
                var healthComp = _world.ECSManager.GetComponent<HealthComp>(enemyEntity);
                var enemyComp = _world.ECSManager.GetComponent<EnemyComp>(enemyEntity);

                var renderEntity = new EnemyRenderData
                {
                    Type = enemyComp.Type,
                    Position = positionComp.Position,
                    Health = healthComp.CurrentHealth,
                    MaxHealth = healthComp.MaxHealth,
                };
                entitiesRenderData.Add(renderEntity);
            }

            return entitiesRenderData.ToArray();
        }
    }
}
