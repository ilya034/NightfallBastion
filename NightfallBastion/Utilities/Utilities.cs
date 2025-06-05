using Microsoft.Xna.Framework;
using NightfallBastion.World;
using NightfallBastion.World.ECS.Components;
using NightfallBastion.World.Tiles;

namespace NightfallBastion.Utilities
{
    public static class UtilMethods
    {
        public static Rectangle GetDestinationRect(Vector2 position, int tileSize) =>
            new Rectangle((int)position.X, (int)position.Y, tileSize, tileSize);
    }

    public static class EntitiesFactory
    {
        public static int CreateTileMap(GameWorld world, int width, int height)
        {
            var tiles = new TileData[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = new TileData { FloorType = FloorType.DefaultFloor };

                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        var wallEntity = CreateWall(
                            world,
                            BuildingType.StrongWall,
                            new Vector2(x, y),
                            false
                        );

                        tiles[x, y].BuildingID = wallEntity;
                    }
                    else if (
                        x == world.Game.CoreSettings.DefaultMapWidth / 2
                        && y == world.Game.CoreSettings.DefaultMapHeight / 2
                    )
                    {
                        var coreEntity = CreateCore(world, new Vector2(x, y));
                        tiles[x, y].BuildingID = coreEntity;
                    }
                }
            }

            var tileMapEntity = world.ECSManager.CreateEntity();
            world.ECSManager.AddComponent(
                tileMapEntity,
                new TileMapComp
                {
                    Width = width,
                    Height = height,
                    TileMap = tiles,
                }
            );
            world.ECSManager.AddComponent(
                tileMapEntity,
                new DistanceMapComp { Distances = new float[width, height] }
            );

            return tileMapEntity;
        }

        public static int CreateCore(GameWorld world, Vector2 position)
        {
            var coreEntity = world.ECSManager.CreateEntity();

            world.ECSManager.AddComponent(coreEntity, new TilePositionComp { Position = position });
            world.ECSManager.AddComponent(
                coreEntity,
                new HealthComp { MaxHealth = 100.0f, CurrentHealth = 100.0f }
            );
            world.ECSManager.AddComponent(
                coreEntity,
                new PhysicsComp
                {
                    Mass = 1.0f,
                    IsSolid = true,
                    Hitbox = new Rectangle(
                        0,
                        0,
                        world.Game.CoreSettings.DefaultTileSize,
                        world.Game.CoreSettings.DefaultTileSize
                    ),
                }
            );
            world.ECSManager.AddComponent(
                coreEntity,
                new BuildingComp { Type = BuildingType.Core, IsDestroyable = true }
            );
            world.ECSManager.AddComponent(coreEntity, new CoreMarkComp());

            return coreEntity;
        }

        public static int CreateWall(
            GameWorld world,
            BuildingType type,
            Vector2 position,
            bool destroyable = true,
            float mass = 1.0f
        )
        {
            if (type != BuildingType.Wall && type != BuildingType.StrongWall)
                return 0;

            var maxHealth = type switch
            {
                BuildingType.Wall => 100.0f,
                BuildingType.StrongWall => 200.0f,
            };

            var wallEntity = world.ECSManager.CreateEntity();

            world.ECSManager.AddComponent(
                wallEntity,
                new HealthComp { MaxHealth = maxHealth, CurrentHealth = maxHealth }
            );
            world.ECSManager.AddComponent(wallEntity, new TilePositionComp { Position = position });
            world.ECSManager.AddComponent(
                wallEntity,
                new PhysicsComp
                {
                    Mass = mass,
                    IsSolid = true,
                    Hitbox = new Rectangle(
                        0,
                        0,
                        world.Game.CoreSettings.DefaultTileSize,
                        world.Game.CoreSettings.DefaultTileSize
                    ),
                }
            );
            world.ECSManager.AddComponent(
                wallEntity,
                new BuildingComp { Type = type, IsDestroyable = destroyable }
            );

            return wallEntity;
        }

        public static int CreateEnemy(
            GameWorld world,
            EnemyType type,
            Vector2 position,
            float maxHealth,
            float mass = 1.0f
        )
        {
            var enemyEntity = world.ECSManager.CreateEntity();
            world.ECSManager.AddComponent(enemyEntity, new EnemyComp { Type = type });
            world.ECSManager.AddComponent(enemyEntity, new PositionComp { Position = position });
            world.ECSManager.AddComponent(
                enemyEntity,
                new MovementComp { Speed = 50f, IsMoving = false }
            );
            world.ECSManager.AddComponent(
                enemyEntity,
                new HealthComp { MaxHealth = maxHealth, CurrentHealth = maxHealth }
            );
            world.ECSManager.AddComponent(
                enemyEntity,
                new PhysicsComp
                {
                    Mass = mass,
                    IsSolid = true,
                    Hitbox = new Rectangle(
                        0,
                        0,
                        world.Game.CoreSettings.DefaultTileSize / 2,
                        world.Game.CoreSettings.DefaultTileSize / 2
                    ),
                }
            );
            world.ECSManager.AddComponent(
                enemyEntity,
                new WeaponComp
                {
                    Type = WeaponType.Kamikaze,
                    Damage = 10.0f,
                    Range = 1.0f,
                    Cooldown = 0.0f,
                }
            );

            return enemyEntity;
        }
    }
}
