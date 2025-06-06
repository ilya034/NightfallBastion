using System;
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
        public RenderAdapter RenderAdapter { get; private set; }
        public Camera Camera { get; private set; }

        public void LoadContent()
        {
            RenderAdapter = new RenderAdapter(this);
            CreateCamera();

            EntitiesFactory.CreateTileMap(
                this,
                Game.CoreSettings.DefaultMapWidth,
                Game.CoreSettings.DefaultMapHeight
            );

            EntitiesFactory.CreateEnemy(this, EnemyType.Boy, new Vector2(80, 80), 100.0f);

            AddSystems();
        }

        private void AddSystems()
        {
            ECSManager.AddSystem(new DistanceMapSystem(this));
            ECSManager.AddSystem(new MovementSystem(this));
            ECSManager.AddSystem(new HealthSystem(this));
            ECSManager.AddSystem(new PhysicsSystem(this));
            ECSManager.AddSystem(new WeaponSystem(this));
            ECSManager.AddSystem(new DamageSystem(this));
            ECSManager.AddSystem(new EnemySpawnSystem(this));
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

        public void Update(GameTime gameTime) => ECSManager.Update(gameTime);

        public void PlaceWall(Vector2 position)
        {
            if (!IsInWorld(position))
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
            if (!IsInWorld(position))
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

            if (
                buildingEntity != 0
                && ECSManager.GetComponent<BuildingComp>(buildingEntity).IsDestroyable
            )
            {
                Console.WriteLine($"Destroying wall at {tilePosition}");
                ECSManager.DestroyEntity(buildingEntity);
                tileMapComp.TileMap[(int)tilePosition.X, (int)tilePosition.Y].BuildingID = 0;
            }
        }

        public bool IsInWorld(Vector2 position)
        {
            var tileSize = Game.CoreSettings.DefaultTileSize;
            return position.X >= 0
                && position.Y >= 0
                && position.X < tileSize * Game.CoreSettings.DefaultMapWidth
                && position.Y < tileSize * Game.CoreSettings.DefaultMapHeight;
        }

        public Vector2 TileToWorldCentered(Vector2 tilePosition) =>
            TileToWorld(tilePosition) + new Vector2(0.5f, 0.5f) * Game.CoreSettings.DefaultTileSize;

        public Vector2 TileToWorld(Vector2 tilePosition) =>
            tilePosition * Game.CoreSettings.DefaultTileSize;

        public Vector2 WorldToTile(Vector2 worldPosition)
        {
            var tileSize = Game.CoreSettings.DefaultTileSize;
            return new Vector2(
                (int)(worldPosition.X / tileSize),
                (int)(worldPosition.Y / tileSize)
            );
        }
    }
}
