using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NightfallBastion.Core;
using NightfallBastion.UI.Cache;
using NightfallBastion.UI.ViewModels;
using NightfallBastion.World;
using NightfallBastion.World.ECS.Commands;
using NightfallBastion.World.ECS.Events;

namespace NightfallBastion.UI
{
    public class GameWorldPresenter : Presenter, IDisposable
    {
        private readonly GameWorldView _view;
        private readonly ViewModelCache _viewModelCache;
        private readonly GameWorld _gameWorld;
        private readonly CommandBus _commandBus;
        private readonly EventBus _eventBus;

        private bool _tileMapNeedsUpdate = true;

        public GameWorldPresenter(NightfallBastionGame game, GameWorldView view)
            : base(game)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _gameWorld = game.GameWorld ?? throw new ArgumentNullException(nameof(game.GameWorld));

            _commandBus = _gameWorld.CommandBus;
            _eventBus = _gameWorld.EventBus;
            _viewModelCache = new ViewModelCache();

            SubscribeToEvents();
            InitializeView();
        }

        private void SubscribeToEvents()
        {
            _eventBus.Subscribe<EntityCreatedEvent>(OnEntityCreated);
            _eventBus.Subscribe<EntityDestroyedEvent>(OnEntityDestroyed);
            _eventBus.Subscribe<ComponentChangedEvent>(OnComponentChanged);
            _eventBus.Subscribe<HealthChangedEvent>(OnHealthChanged);
            _eventBus.Subscribe<CameraChangedEvent>(OnCameraChanged);
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

            if (cameraInput.MovementDirection != Vector2.Zero)
            {
                _commandBus.EnqueueCommand(
                    new MoveCameraCommand(cameraInput.MovementDirection, cameraInput.MovementSpeed)
                );
                ApplyCameraMovement(cameraInput.MovementDirection, cameraInput.MovementSpeed);
            }

            if (Math.Abs(cameraInput.ZoomDelta) > 0.001f)
            {
                _commandBus.EnqueueCommand(new ZoomCameraCommand(cameraInput.ZoomDelta));
                ApplyCameraZoom(cameraInput.ZoomDelta);
            }

            var gameplayInput = _game.SceneManager.InputHandler.HandleGameplayInput(
                keyboardState,
                mouseState,
                gameTime
            );

            if (gameplayInput.LeftMouseClicked && gameplayInput.MouseClickPosition.HasValue)
            {
                var worldPosition = _gameWorld.ScreenToWorld(
                    gameplayInput.MouseClickPosition.Value
                );
                _commandBus.EnqueueCommand(new SelectEntityCommand(worldPosition));
            }

            if (gameplayInput.RightMouseClicked && gameplayInput.MouseClickPosition.HasValue)
            {
                var worldPosition = _gameWorld.ScreenToWorld(
                    gameplayInput.MouseClickPosition.Value
                );
                // Пример команды создания врага для тестирования
                _commandBus.EnqueueCommand(new SpawnEnemyCommand(worldPosition, EnemyType.boy));
            }

            if (gameplayInput.BuildingSelectionPressed)
            {
                Console.WriteLine("Building selection pressed");
            }
        }

        private void ApplyCameraMovement(Vector2 direction, float speed)
        {
            var camera = _gameWorld.Camera;
            var newPosition = camera.Position + direction * speed;
            camera.Position = newPosition;
            _viewModelCache.InvalidateCamera();

            _eventBus.Publish(new CameraChangedEvent(camera.Position, camera.Zoom));
        }

        private void ApplyCameraZoom(float zoomDelta)
        {
            var camera = _gameWorld.Camera;
            var newZoom = Math.Max(0.1f, camera.Zoom + zoomDelta);
            camera.Zoom = newZoom;
            _viewModelCache.InvalidateCamera();

            _eventBus.Publish(new CameraChangedEvent(camera.Position, camera.Zoom));
        }

        private void UpdateView()
        {
            // Обновляем ViewModels через кэш
            var enemyViewModels = _viewModelCache.GetEntityViewModels(_gameWorld.ECSManager);
            _view.UpdateEnemies(enemyViewModels);

            var cameraViewModel = _viewModelCache.GetCameraViewModel(_gameWorld.Camera);
            _view.CurrentCameraViewModel = cameraViewModel;

            if (_tileMapNeedsUpdate)
            {
                UpdateTileMapView();
                _tileMapNeedsUpdate = false;
            }

            _view.Draw();
        }

        private void UpdateTileMapView()
        {
            // TileMap пока не реализован в новой архитектуре
            // Создаем пустую TileMapViewModel чтобы избежать ошибок
            try
            {
                var emptyTileMapViewModel = new TileMapViewModel();
                _view.CurrentTileMapViewModel = emptyTileMapViewModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Error updating TileMap view: {ex.Message}");
            }
        }

        // Event handlers
        private void OnEntityCreated(EntityCreatedEvent evt)
        {
            _viewModelCache.InvalidateEntity(evt.Entity.Id);
        }

        private void OnEntityDestroyed(EntityDestroyedEvent evt)
        {
            _viewModelCache.InvalidateEntity(evt.Entity.Id);
        }

        private void OnComponentChanged(ComponentChangedEvent evt)
        {
            _viewModelCache.InvalidateEntity(evt.Entity.Id);
        }

        private void OnHealthChanged(HealthChangedEvent evt)
        {
            _viewModelCache.InvalidateEntity(evt.Entity.Id);
        }

        private void OnCameraChanged(CameraChangedEvent evt)
        {
            _viewModelCache.InvalidateCamera();
        }

        private void ForceUpdateAllViewModels()
        {
            _tileMapNeedsUpdate = true;
            _viewModelCache.Clear();
            UpdateView();
        }

        public void UpdateCameraViewport()
        {
            _gameWorld.UpdateCameraViewport();
            _viewModelCache.InvalidateCamera();
        }

        public void MarkTileMapForUpdate()
        {
            _tileMapNeedsUpdate = true;
        }

        public void MarkCameraForUpdate()
        {
            _viewModelCache.InvalidateCamera();
        }

        public void Dispose()
        {
            // Отписка от событий не требуется, так как EventBus будет уничтожен вместе с GameWorld
            _viewModelCache?.Clear();
        }
    }
}
