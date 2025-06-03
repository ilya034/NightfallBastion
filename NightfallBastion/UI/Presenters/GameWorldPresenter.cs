using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NightfallBastion.Core;
using NightfallBastion.UI.Adapters;
using NightfallBastion.UI.Input;
using NightfallBastion.UI.ViewModels;
using NightfallBastion.World;

namespace NightfallBastion.UI
{
    public class GameWorldPresenter : Presenter
    {
        private readonly GameWorldView _view;
        private readonly ViewModelAdapter _viewModelAdapter;
        private readonly GameWorld _gameWorld;

        private TileMapViewModel? _cachedTileMapViewModel;
        private CameraViewModel? _cachedCameraViewModel;
        private List<EnemyViewModel> _cachedEnemyViewModels;

        private bool _tileMapNeedsUpdate = true;
        private bool _cameraViewNeedsUpdate = true;
        private bool _enemiesNeedUpdate = true;

        public GameWorldPresenter(NightfallBastionGame game, GameWorldView view)
            : base(game)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _gameWorld = game.GameWorld ?? throw new ArgumentNullException(nameof(game.GameWorld));

            _viewModelAdapter = new ViewModelAdapter();
            _cachedEnemyViewModels = new List<EnemyViewModel>();

            InitializeView();
        }

        private void InitializeView()
        {
            _view.LoadContent();
            ForceUpdateAllViewModels();
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            HandleInput(keyboardState, mouseState, gameTime);
            UpdateModel(gameTime);
            UpdateView();

            _game.SceneManager.InputHandler.UpdatePreviousStates(keyboardState, mouseState);
        }

        private void HandleInput(
            KeyboardState keyboardState,
            MouseState mouseState,
            GameTime gameTime
        )
        {
            var cameraInput = _game.SceneManager.InputHandler.HandleCameraInput(
                keyboardState,
                mouseState,
                gameTime,
                _game.GameplaySettings.CameraSpeed
            );

            ApplyCameraInput(cameraInput);

            var gameplayInput = _game.SceneManager.InputHandler.HandleGameplayInput(
                keyboardState,
                mouseState,
                gameTime
            );

            ApplyGameplayInput(gameplayInput, gameTime);
        }

        private void ApplyCameraInput(CameraInputData cameraInput)
        {
            var camera = _gameWorld.Camera;

            if (cameraInput.MovementDirection != Vector2.Zero)
            {
                var newPosition =
                    camera.Position + cameraInput.MovementDirection * cameraInput.MovementSpeed;
                camera.Position = newPosition;
                _cameraViewNeedsUpdate = true;
            }

            if (Math.Abs(cameraInput.ZoomDelta) > 0.001f)
            {
                var newZoom = Math.Max(0.1f, camera.Zoom + cameraInput.ZoomDelta);
                camera.Zoom = newZoom;
                _cameraViewNeedsUpdate = true;
            }
        }

        private void ApplyGameplayInput(GameplayInputData gameplayInput, GameTime gameTime)
        {
            if (gameplayInput.LeftMouseClicked && gameplayInput.MouseClickPosition.HasValue)
                HandleLeftMouseClick(gameplayInput.MouseClickPosition.Value);

            if (gameplayInput.RightMouseClicked && gameplayInput.MouseClickPosition.HasValue)
                HandleRightMouseClick(gameplayInput.MouseClickPosition.Value);

            if (gameplayInput.BuildingSelectionPressed)
                HandleBuildingSelection();
        }

        private void HandleLeftMouseClick(Vector2 screenPosition)
        {
            var worldPosition = _gameWorld.ScreenToWorld(screenPosition);
            Console.WriteLine($"Left click at world position: {worldPosition}");
        }

        private void HandleRightMouseClick(Vector2 screenPosition)
        {
            var worldPosition = _gameWorld.ScreenToWorld(screenPosition);
            Console.WriteLine($"Right click at world position: {worldPosition}");
        }

        private void HandleBuildingSelection()
        {
            Console.WriteLine("Building selection pressed");
        }

        private void UpdateModel(GameTime gameTime)
        {
            _gameWorld.ECSManager.Update(gameTime);
            _enemiesNeedUpdate = true;
        }

        private void UpdateView()
        {
            if (_tileMapNeedsUpdate)
            {
                UpdateTileMapView();
                _tileMapNeedsUpdate = false;
            }

            if (_cameraViewNeedsUpdate)
            {
                UpdateCameraView();
                _cameraViewNeedsUpdate = false;
            }

            if (_enemiesNeedUpdate)
            {
                UpdateEnemiesView();
                _enemiesNeedUpdate = false;
            }

            _view.Draw();
        }

        private void UpdateTileMapView()
        {
            if (_gameWorld.TileMap != null)
            {
                _cachedTileMapViewModel = _viewModelAdapter.CreateTileMapViewModel(
                    _gameWorld.TileMap
                );

                _view.CurrentTileMapViewModel = _cachedTileMapViewModel;
            }
            else
                Console.WriteLine($"[DEBUG] TileMap is null, cannot update view");
        }

        private void UpdateCameraView()
        {
            if (_cachedCameraViewModel == null)
                _cachedCameraViewModel = _viewModelAdapter.CreateCameraViewModel(_gameWorld.Camera);
            else
                _viewModelAdapter.UpdateCameraViewModel(_cachedCameraViewModel, _gameWorld.Camera);

            _view.CurrentCameraViewModel = _cachedCameraViewModel;
        }

        private void UpdateEnemiesView()
        {
            _viewModelAdapter.UpdateEnemyViewModels(_cachedEnemyViewModels, _gameWorld.ECSManager);
            _view.UpdateEnemies(_cachedEnemyViewModels);
        }

        private void ForceUpdateAllViewModels()
        {
            _tileMapNeedsUpdate = true;
            _cameraViewNeedsUpdate = true;
            _enemiesNeedUpdate = true;

            UpdateView();
        }

        public void UpdateCameraViewport()
        {
            _gameWorld.UpdateCameraViewport();
            _cameraViewNeedsUpdate = true;
        }

        public void MarkTileMapForUpdate()
        {
            _tileMapNeedsUpdate = true;
        }

        public void MarkCameraForUpdate()
        {
            _cameraViewNeedsUpdate = true;
        }
    }
}
