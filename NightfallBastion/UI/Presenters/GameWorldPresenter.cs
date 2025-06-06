using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class GameWorldPresenter(NightfallBastionGame game, GameWorldView view) : Presenter(game)
    {
        private readonly GameWorldView _view = view;

        public override void Update(GameTime gameTime)
        {
            HandleInput();
            _game.GameWorld.Update(gameTime);
            _view.UpdateRenderData(_game.GameWorld.RenderAdapter.GetRenderData());
            base.Update(gameTime);
        }

        private void HandleInput()
        {
            if (_game.InputHandler.IsKeyPressed(Keys.Escape))
            {
                _game.SceneManager.ShowScene(Scenes.Pause);
                return;
            }

            var cameraInput = _game.InputHandler.HandleCameraInput();

            if (cameraInput.MovementDirection != Vector2.Zero)
            {
                var movement =
                    cameraInput.MovementDirection * _game.GameplaySettings.DefaultCameraSpeed;

                if (_game.InputHandler.IsKeyDown(Keys.LeftShift))
                    movement *= 2f;

                _game.GameWorld.Camera.Move(movement);
            }

            if (cameraInput.ZoomDelta != 0f)
            {
                var zoomFactor = 1f + cameraInput.ZoomDelta;
                _game.GameWorld.Camera.ScaleZoom(zoomFactor);
            }

            if (_game.InputHandler.IsLeftMouseButtonDown())
            {
                var worldPosition = _game.GameWorld.Camera.ScreenToWorld(
                    _game.InputHandler.GetMousePosition()
                );
                HandleLeftClick(worldPosition);
            }

            if (_game.InputHandler.IsRightMouseButtonDown())
            {
                var worldPosition = _game.GameWorld.Camera.ScreenToWorld(
                    _game.InputHandler.GetMousePosition()
                );
                HandleRightClick(worldPosition);
            }
        }

        private void HandleLeftClick(Vector2 position)
        {
            _game.GameWorld.PlaceWall(position);
        }

        private void HandleRightClick(Vector2 position)
        {
            _game.GameWorld.DestroyWall(position);
        }
    }
}
