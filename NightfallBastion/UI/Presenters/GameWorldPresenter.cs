using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class GameWorldPresenter(NightfallBastionGame game, GameWorldView view) : Presenter(game)
    {
        private readonly GameWorldView _view = view;
        private readonly InputHandler _inputHandler = new();

        public override void Update(GameTime gameTime)
        {
            HandleInput();
            _game.GameWorld.Update(gameTime);
            _view.UpdateRenderData(_game.GameWorld.GetRenderData());
            base.Update(gameTime);
        }

        private void HandleInput()
        {
            var cameraInput = _inputHandler.HandleCameraInput();

            if (cameraInput.MovementDirection != Vector2.Zero)
            {
                var movement =
                    cameraInput.MovementDirection * _game.GameplaySettings.DefaultCameraSpeed;
                _game.GameWorld.Camera.Move(movement);
            }

            if (cameraInput.ZoomDelta != 0f)
            {
                var zoomFactor = 1f + cameraInput.ZoomDelta;
                _game.GameWorld.Camera.ScaleZoom(zoomFactor);
            }

            var gameplayInput = _inputHandler.HandleGameplayInput();

            if (gameplayInput.LeftMouseClicked && gameplayInput.MouseClickPosition.HasValue)
            {
                var worldPosition = _game.GameWorld.ScreenToWorld(
                    gameplayInput.MouseClickPosition.Value
                );
                HandleLeftClick(worldPosition);
            }

            if (gameplayInput.RightMouseClicked && gameplayInput.MouseClickPosition.HasValue)
            {
                var worldPosition = _game.GameWorld.ScreenToWorld(
                    gameplayInput.MouseClickPosition.Value
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
