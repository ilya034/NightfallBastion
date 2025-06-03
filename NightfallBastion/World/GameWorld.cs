using System;
using Microsoft.Xna.Framework;
using NightfallBastion.Core;
using NightfallBastion.UI;
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

            AddSystems();

            var scene = Game.SceneManager.GetScene(Scenes.GameWorld);
            if (scene is GameWorldScene gameWorldScene)
                (gameWorldScene.Presenter as GameWorldPresenter)?.MarkTileMapForUpdate();
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
    }
}
