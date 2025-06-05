using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using NightfallBastion.Core;
using NightfallBastion.Utilities;
using NightfallBastion.World.ECS.Components;
using NightfallBastion.World.ECS.Systems;

namespace NightfallBastion.World
{
    public class GameWorld(NightfallBastionGame game)
    {
        public NightfallBastionGame Game { get; private set; } = game;
        public ECSManager ECSManager { get; private set; } = new();
        public Camera Camera { get; private set; }

        public void LoadContent()
        {
            CreateCamera();

            EntitiesFactory.CreateTileMap(
                this,
                Game.CoreSettings.DefaultMapWidth,
                Game.CoreSettings.DefaultMapHeight
            );

            EntitiesFactory.CreateEnemy(this, EnemyType.Boy, new Vector2(64, 64), 100.0f);

            AddSystems();
        }

        private void AddSystems()
        {
            ECSManager.AddSystem(new DistanceMapSystem(this));
            ECSManager.AddSystem(new MovementSystem(this));
            ECSManager.AddSystem(new PhysicsSystem(this));
            ECSManager.AddSystem(new WeaponSystem(this));
        }

        private void CreateCamera()
        {
            var viewport = Game.GraphicsDevice.Viewport;
            Console.WriteLine($"Camera Viewport size: {viewport.Width}x{viewport.Height}");

            Camera = new Camera(viewport.Width, viewport.Height)
            {
                Position = Vector2.Zero,
                Zoom = 1.0f,
                Rotation = 0.0f,
            };
        }

        public void UpdateCameraViewport()
        {
            var viewport = Game.GraphicsDevice.Viewport;
            Camera.ViewportWidth = viewport.Width;
            Camera.ViewportHeight = viewport.Height;
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition) =>
            Camera.ScreenToWorld(screenPosition);

        public Vector2 WorldToScreen(Vector2 worldPosition) => Camera.WorldToScreen(worldPosition);

        public Matrix GetCameraTransformMatrix() => Camera.GetTransformMatrix();

        public bool IsInCameraView(Vector2 worldPosition) => Camera.IsInView(worldPosition);

        public void Update(GameTime gameTime) => ECSManager.Update(gameTime);

        public void PlaceWall(Vector2 position)
        {
            if (position.X < 0 || position.Y < 0 || !IsInCameraView(position))
                return;

            var tileMapEntity = ECSManager
                .GetEntitiesWithComponents<TileMapComp>()
                .FirstOrDefault();

            var tileMapComp = ECSManager.GetComponent<TileMapComp>(tileMapEntity);
            var tilePosition = WorldToTile(position);
            var tileX = (int)tilePosition.X;
            var tileY = (int)tilePosition.Y;

            if (
                tileMapComp.TileMap[tileX, tileY].BuildingID != 0
                || tileMapComp.TileMap[tileX, tileY].FloorType == FloorType.Space
            )
                return;

            Console.WriteLine($"Placing wall at {tilePosition}");

            tileMapComp.TileMap[tileX, tileY].BuildingID = EntitiesFactory.CreateWall(
                this,
                BuildingType.Wall,
                tilePosition
            );
        }

        public void DestroyWall(Vector2 position)
        {
            if (position.X < 0 || position.Y < 0 || !IsInCameraView(position))
                return;

            var tileMapEntity = ECSManager
                .GetEntitiesWithComponents<TileMapComp>()
                .FirstOrDefault();

            var tileMapComp = ECSManager.GetComponent<TileMapComp>(tileMapEntity);
            var tilePosition = WorldToTile(position);

            var buildingEntity = ECSManager
                .GetEntitiesWithComponents<BuildingComp>()
                .Where(e => ECSManager.GetComponent<TilePositionComp>(e).Position == tilePosition)
                .FirstOrDefault();

            if (buildingEntity != 0)
            {
                Console.WriteLine($"Destroying wall at {tilePosition}");
                ECSManager.DestroyEntity(buildingEntity);
                tileMapComp.TileMap[(int)tilePosition.X, (int)tilePosition.Y].BuildingID = 0;
            }
        }

        public Vector2 WorldToTile(Vector2 worldPosition)
        {
            var tileSize = Game.CoreSettings.DefaultTileSize;
            return new Vector2(
                (int)(worldPosition.X / tileSize),
                (int)(worldPosition.Y / tileSize)
            );
        }

        public RenderData GetRenderData()
        {
            var renderData = new RenderData
            {
                cameraTransform = GetCameraTransformMatrix(),
                tileMapData = GetTileMapRenderData(),
                buildingData = GetBuildingRenderData(),
                enemyData = GetEnemyRenderData(),
            };

            return renderData;
        }

        public TileMapRenderData GetTileMapRenderData()
        {
            var tileMapEntity = ECSManager
                .GetEntitiesWithComponents<TileMapComp>()
                .FirstOrDefault();

            if (tileMapEntity == null)
                throw new InvalidOperationException("TileMapComp not found in ECSManager.");

            var tileMapComp = ECSManager.GetComponent<TileMapComp>(tileMapEntity);

            var tiles = new TileRenderData[tileMapComp.Width, tileMapComp.Height];

            for (int x = 0; x < tileMapComp.Width; x++)
            {
                for (int y = 0; y < tileMapComp.Height; y++)
                {
                    var tileData = tileMapComp.TileMap[x, y];

                    var floorTile = new TileRenderData { floor = tileData.FloorType };
                    tiles[x, y] = floorTile;
                }
            }

            return new TileMapRenderData
            {
                tiles = tiles,
                width = tileMapComp.Width,
                height = tileMapComp.Height,
            };
        }

        public BuildingRenderData[] GetBuildingRenderData()
        {
            var buildingsRenderData = new List<BuildingRenderData>();

            var buildingEntities = ECSManager.GetEntitiesWithComponents<BuildingComp, HealthComp>();

            foreach (var buildingEntity in buildingEntities)
            {
                var healthComp = ECSManager.GetComponent<HealthComp>(buildingEntity);
                var buildingComp = ECSManager.GetComponent<BuildingComp>(buildingEntity);

                Vector2 position;
                if (ECSManager.TryGetComponent<PositionComp>(buildingEntity, out var positionComp))
                    position = positionComp.Position;
                else
                    position =
                        ECSManager.GetComponent<TilePositionComp>(buildingEntity).Position
                        * Game.CoreSettings.DefaultTileSize;

                var renderEntity = new BuildingRenderData
                {
                    type = buildingComp.Type,
                    position = position,
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

            var enemyEntities = ECSManager.GetEntitiesWithComponents<
                EnemyComp,
                PositionComp,
                HealthComp
            >();

            foreach (var enemyEntity in enemyEntities)
            {
                var positionComp = ECSManager.GetComponent<PositionComp>(enemyEntity);
                var healthComp = ECSManager.GetComponent<HealthComp>(enemyEntity);
                var enemyComp = ECSManager.GetComponent<EnemyComp>(enemyEntity);

                var renderEntity = new EnemyRenderData
                {
                    type = enemyComp.Type,
                    position = positionComp.Position,
                    health = healthComp.CurrentHealth,
                    maxHealth = healthComp.MaxHealth,
                };
                entitiesRenderData.Add(renderEntity);
            }

            return entitiesRenderData.ToArray();
        }
    }
}
