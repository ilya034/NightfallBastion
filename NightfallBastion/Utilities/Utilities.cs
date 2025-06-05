using Microsoft.Xna.Framework;
using NightfallBastion.World;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.Utilities
{
    public static class UtilMethods
    {
        public static Rectangle GetDestinationRect(Vector2 position, int tileSize) =>
            new Rectangle((int)position.X, (int)position.Y, tileSize, tileSize);
    }

    public static class EntitiesFactory
    {
        public static Entity CreateTileMap(GameWorld world, int width, int height)
        {
            var tiles = new TileData[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = new TileData { floor = FloorType.DefaultFloor };

                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        var wallEntity = CreateWall(
                            world,
                            BuildingType.StrongWall,
                            new Vector2(
                                x * world.Game.CoreSettings.DefaultTileSize,
                                y * world.Game.CoreSettings.DefaultTileSize
                            )
                        );

                        tiles[x, y].BuildingID = wallEntity.Id;
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


            return tileMapEntity;
        }

        public static Entity CreatePlayerCore(GameWorld world, Vector2 position)
        {
            var playerCoreEntity = world.ECSManager.CreateEntity();

            world.ECSManager.AddComponent(
                playerCoreEntity,
                new PositionComp { Position = position }
            );
            world.ECSManager.AddComponent(
                playerCoreEntity,
                new HealthComp { MaxHealth = 100.0f, CurrentHealth = 100.0f }
            );
            world.ECSManager.AddComponent(
                playerCoreEntity,
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

            return playerCoreEntity;
        }

        public static Entity CreateWall(
            GameWorld world,
            BuildingType type,
            Vector2 position,
            float mass = 1.0f
        )
        {
            if (type != BuildingType.Wall && type != BuildingType.StrongWall)
                return null;

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
            world.ECSManager.AddComponent(wallEntity, new PositionComp { Position = position });
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
            world.ECSManager.AddComponent(wallEntity, new BuildingComp { Type = type });

            return wallEntity;
        }

        public static Entity CreateEnemy(
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
