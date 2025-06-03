// using System.Collections.Generic;
// using Microsoft.Xna.Framework;
// using NightfallBastion.UI.ViewModels;
// using NightfallBastion.World;
// using NightfallBastion.World.Buildings;

// namespace NightfallBastion.UI.Adapters
// {
//     public class ViewModelAdapter
//     {
//         public TileMapViewModel CreateTileMapViewModel(TileMap tileMap)
//         {
//             if (tileMap == null)
//                 return new TileMapViewModel();

//             var tileViewModels = new TileViewModel[tileMap.Width * tileMap.Height];

//             for (int y = 0; y < tileMap.Height; y++)
//             {
//                 for (int x = 0; x < tileMap.Width; x++)
//                 {
//                     var tile = tileMap.GetTile(x, y);
//                     var tileViewModel = CreateTileViewModel(tile, x, y, tileMap.TileSize);
//                     tileViewModels[y * tileMap.Width + x] = tileViewModel;
//                 }
//             }

//             return new TileMapViewModel(tileMap.Width, tileMap.Height, tileViewModels);
//         }

//         private TileViewModel CreateTileViewModel(Tile tile, int x, int y, int tileSize)
//         {
//             if (tile?.Building == null)
//             {
//                 return new TileViewModel
//                 {
//                     X = x,
//                     Y = y,
//                     TileType = 0,
//                     TextureRegion = new Rectangle(0, 0, tileSize, tileSize),
//                     Health = 0,
//                     MaxHealth = 0,
//                 };
//             }

//             var building = tile.Building;
//             return new TileViewModel
//             {
//                 X = x,
//                 Y = y,
//                 TileType = GetTileTypeFromBuilding(building),
//                 TextureRegion = building.SourceRect,
//                 Health = building.CurrentHealth,
//                 MaxHealth = building.MaxHealth,
//             };
//         }

//         private int GetTileTypeFromBuilding(Building building)
//         {
//             return building.GetType().Name switch
//             {
//                 "Wall" => 1,
//                 "EnemySpawn" => 2,
//                 "PlayerCore" => 3,
//                 _ => 0,
//             };
//         }

//         public List<EnemyViewModel> CreateEnemyViewModels(ECSManager ecsManager)
//         {
//             var enemyViewModels = new List<EnemyViewModel>();
//             FillEnemyViewModels(enemyViewModels, ecsManager);
//             return enemyViewModels;
//         }

//         private void FillEnemyViewModels(List<EnemyViewModel> viewModels, ECSManager ecsManager)
//         {
//             if (viewModels == null || ecsManager == null)
//                 return;

//             var enemyEntities = ecsManager.GetEntitiesWithComponents<
//                 EnemyComponent,
//                 PositionComponent
//             >();

//             foreach (var entity in enemyEntities)
//             {
//                 var enemyComponent = ecsManager.GetComponent<EnemyComponent>(entity);
//                 var positionComponent = ecsManager.GetComponent<PositionComponent>(entity);

//                 if (enemyComponent != null && positionComponent != null)
//                 {
//                     var enemyViewModel = CreateEnemyViewModel(enemyComponent, positionComponent);
//                     viewModels.Add(enemyViewModel);
//                 }
//             }
//         }

//         private EnemyViewModel CreateEnemyViewModel(
//             EnemyComponent enemyComponent,
//             PositionComponent positionComponent
//         )
//         {
//             var textureRegion = new Rectangle(0, 0, 32, 32);

//             return new EnemyViewModel(
//                 positionComponent.Position.X,
//                 positionComponent.Position.Y,
//                 enemyComponent.CurrentHealth,
//                 enemyComponent.MaxHealth,
//                 textureRegion,
//                 1.0f
//             );
//         }

//         public void UpdateEnemyViewModels(
//             List<EnemyViewModel> existingViewModels,
//             ECSManager ecsManager
//         )
//         {
//             if (existingViewModels == null)
//                 return;
//             existingViewModels.Clear();
//             FillEnemyViewModels(existingViewModels, ecsManager);
//         }

//         public CameraViewModel CreateCameraViewModel(Camera camera)
//         {
//             if (camera == null)
//                 return new CameraViewModel();

//             return new CameraViewModel(camera.Position.X, camera.Position.Y, camera.Zoom);
//         }

//         public void UpdateCameraViewModel(CameraViewModel cameraViewModel, Camera camera)
//         {
//             if (cameraViewModel == null || camera == null)
//                 return;

//             cameraViewModel.X = camera.Position.X;
//             cameraViewModel.Y = camera.Position.Y;
//             cameraViewModel.Zoom = camera.Zoom;
//         }
//     }
// }
