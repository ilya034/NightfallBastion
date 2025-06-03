using System;
using Microsoft.Xna.Framework;
using NightfallBastion.Core;
using NightfallBastion.UI;
using NightfallBastion.World.Buildings;
using NightfallBastion.World.Floors;

namespace NightfallBastion.World
{
    public class GameWorld(NightfallBastionGame game)
    {
        public NightfallBastionGame Game { get; private set; } = game;
        public ECSManager ECSManager { get; private set; } = new();
        public TileMap? TileMap { get; private set; }
        public Camera Camera { get; private set; }

        public void LoadContent()
        {
            CreateCamera();

            ECSManager.AddSystem(new DamageSystem(this));
            ECSManager.AddSystem(new EnemySpawnSystem(this));
            ECSManager.AddSystem(new MovementSystem(this));
            ECSManager.AddSystem(new EnemyCollisionSystem(this));

            TileMap = new TileMap(
                Game.CoreSettings.DefaultMapWidth,
                Game.CoreSettings.DefaultMapHeight,
                Game.CoreSettings.DefaultTileSize
            );

            for (int y = 0; y < Game.CoreSettings.DefaultMapHeight; y++)
            {
                for (int x = 0; x < Game.CoreSettings.DefaultMapWidth; x++)
                {
                    Tile tile;
                    if (
                        x == 0
                        || y == 0
                        || x == Game.CoreSettings.DefaultMapWidth - 1
                        || y == Game.CoreSettings.DefaultMapHeight - 1
                    )
                        tile = new Tile(
                            FloorFactory.CreateStandartFloor(Game),
                            BuildingFactory.CreateStrongWall(Game)
                        );
                    else if ((x == 5 && y >= 5 && y <= 10) || (x == 15 && y >= 5 && y <= 10))
                        tile = new Tile(
                            FloorFactory.CreateStandartFloor(Game),
                            BuildingFactory.CreateMediumWall(Game)
                        );
                    else if (x >= 8 && x <= 12 && y == 8)
                        tile = new Tile(
                            FloorFactory.CreateStandartFloor(Game),
                            BuildingFactory.CreateWeakWall(Game)
                        );
                    else if (x == 2 && y == 2)
                        tile = new Tile(
                            FloorFactory.CreateStandartFloor(Game),
                            BuildingFactory.CreateEnemySpawn(Game)
                        );
                    else if (
                        x == Game.CoreSettings.DefaultMapWidth / 2
                        && y == Game.CoreSettings.DefaultMapHeight / 2
                    )
                        tile = new Tile(
                            FloorFactory.CreateStandartFloor(Game),
                            BuildingFactory.CreatePlayerCore(Game)
                        );
                    else
                        tile = new Tile(
                            FloorFactory.CreateStandartFloor(Game),
                            BuildingFactory.CreateEmptySpace(Game)
                        );

                    TileMap.SetTile(x, y, tile);
                }
            }

            var scene = Game.SceneManager.GetScene(Scenes.GameWorld);
            if (scene is GameWorldScene gameWorldScene)
                (gameWorldScene.Presenter as GameWorldPresenter)?.MarkTileMapForUpdate();
        }

        public void Dispose()
        {
            ECSManager?.Dispose();
            TileMap = null;
            Camera = null;
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
    }
}
