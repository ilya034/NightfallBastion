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
                Game.CoreSettings.DefaultButtonHeight
            );

            EntitiesFactory.CreateEnemy(this, EnemyType.Boy, new Vector2(64, 64), 100.0f);

            AddSystems();
        }

        public void Dispose()
        {
            ECSManager?.Dispose();
            Camera = null;
        }

        private void AddSystems()
        {
            ECSManager.AddSystem(new PathfindSystem(this));
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
                throw new InvalidOperationException("TileMap entity not found in ECSManager.");

            var tileMapComp = ECSManager.GetComponent<TileMapComp>(tileMapEntity);

            var tiles = new TileRenderData[tileMapComp.width, tileMapComp.height];

            for (int x = 0; x < tileMapComp.width; x++)
            {
                for (int y = 0; y < tileMapComp.height; y++)
                {
                    var tileData = tileMapComp.tileMap[x, y];

                    var floorTile = new TileRenderData { floor = tileData.floor };
                    tiles[x, y] = floorTile;
                }
            }

            return new TileMapRenderData
            {
                tiles = tiles,
                width = tileMapComp.width,
                height = tileMapComp.height,
            };
        }

        public BuildingRenderData[] GetBuildingRenderData()
        {
            var buildingsRenderData = new List<BuildingRenderData>();

            var buildingEntities = ECSManager.GetEntitiesWithComponents<
                BuildingComp,
                PositionComp,
                HealthComp
            >();

            foreach (var buildingEntity in buildingEntities)
            {
                var positionComp = ECSManager.GetComponent<PositionComp>(buildingEntity);
                var healthComp = ECSManager.GetComponent<HealthComp>(buildingEntity);
                var buildingComp = ECSManager.GetComponent<BuildingComp>(buildingEntity);

                var renderEntity = new BuildingRenderData
                {
                    type = buildingComp.type,
                    position = positionComp.position,
                    Health = healthComp.currentHealth,
                    MaxHealth = healthComp.maxHealth,
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
                    type = enemyComp.type,
                    position = positionComp.position,
                    health = healthComp.currentHealth,
                    maxHealth = healthComp.maxHealth,
                };
                entitiesRenderData.Add(renderEntity);
            }

            return entitiesRenderData.ToArray();
        }
    }
}
