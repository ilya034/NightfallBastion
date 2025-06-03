using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using NightfallBastion.UI.ViewModels;
using NightfallBastion.World;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.UI.Cache
{
    public class ViewModelCache
    {
        private readonly Dictionary<int, EnemyViewModel> _entityViewModels = new();
        private readonly HashSet<int> _invalidatedEntities = new();
        private CameraViewModel _cachedCameraViewModel;
        private bool _cameraInvalidated = true;

        public List<EnemyViewModel> GetEntityViewModels(ECSManager ecsManager)
        {
            var result = new List<EnemyViewModel>();
            var entities = ecsManager.GetEntitiesWithComponents<EnemyComp, PositionComp>();

            foreach (var entity in entities)
            {
                if (_invalidatedEntities.Contains(entity.Id) ||
                    !_entityViewModels.TryGetValue(entity.Id, out var viewModel))
                {
                    viewModel = CreateEntityViewModel(entity, ecsManager);
                    if (viewModel != null)
                    {
                        _entityViewModels[entity.Id] = viewModel;
                        _invalidatedEntities.Remove(entity.Id);
                    }
                }
                else
                {
                    UpdateEntityViewModel(viewModel, entity, ecsManager);
                }

                if (viewModel != null)
                    result.Add(viewModel);
            }

            // Удаляем ViewModels для несуществующих сущностей
            var existingEntityIds = entities.Select(e => e.Id).ToHashSet();
            var toRemove = _entityViewModels.Keys.Where(id => !existingEntityIds.Contains(id)).ToList();
            foreach (var id in toRemove)
            {
                _entityViewModels.Remove(id);
            }

            return result;
        }

        public CameraViewModel GetCameraViewModel(Camera camera)
        {
            if (_cameraInvalidated || _cachedCameraViewModel == null)
            {
                _cachedCameraViewModel = CreateCameraViewModel(camera);
                _cameraInvalidated = false;
            }
            else
            {
                UpdateCameraViewModel(_cachedCameraViewModel, camera);
            }

            return _cachedCameraViewModel;
        }

        public void InvalidateEntity(int entityId)
        {
            _invalidatedEntities.Add(entityId);
        }

        public void InvalidateCamera()
        {
            _cameraInvalidated = true;
        }

        public void Clear()
        {
            _entityViewModels.Clear();
            _invalidatedEntities.Clear();
            _cachedCameraViewModel = null;
            _cameraInvalidated = true;
        }

        private EnemyViewModel CreateEntityViewModel(Entity entity, ECSManager ecsManager)
        {
            PositionComp? position = ecsManager.GetComponent<PositionComp>(entity);
            HealthComp? health = ecsManager.GetComponent<HealthComp>(entity);

            if (!position.HasValue)
                return null;

            return new EnemyViewModel(
                position.Value.position.X,
                position.Value.position.Y,
                (int)(health?.currentHealth ?? 1),
                (int)(health?.maxHealth ?? 1),
                new Rectangle(0, 0, 32, 32), // Default texture region
                1.0f
            );
        }

        private void UpdateEntityViewModel(EnemyViewModel viewModel, Entity entity, ECSManager ecsManager)
        {
            PositionComp? position = ecsManager.GetComponent<PositionComp>(entity);
            HealthComp? health = ecsManager.GetComponent<HealthComp>(entity);

            if (position.HasValue)
            {
                viewModel.X = position.Value.position.X;
                viewModel.Y = position.Value.position.Y;
            }

            if (health.HasValue)
            {
                viewModel.Health = (int)health.Value.currentHealth;
                viewModel.MaxHealth = (int)health.Value.maxHealth;
            }
        }

        private CameraViewModel CreateCameraViewModel(Camera camera)
        {
            if (camera == null)
                return new CameraViewModel();

            return new CameraViewModel(camera.Position.X, camera.Position.Y, camera.Zoom);
        }

        private void UpdateCameraViewModel(CameraViewModel cameraViewModel, Camera camera)
        {
            if (cameraViewModel == null || camera == null)
                return;

            cameraViewModel.X = camera.Position.X;
            cameraViewModel.Y = camera.Position.Y;
            cameraViewModel.Zoom = camera.Zoom;
        }
    }
}