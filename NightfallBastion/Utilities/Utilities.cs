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
                    tiles[x, y] = new TileData { floor = Floor.DefaultFloor };

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
                    width = width,
                    height = height,
                    tileMap = tiles,
                }
            );

            return tileMapEntity;
        }

        public static Entity CreatePlayerCore(GameWorld world, Vector2 position)
        {
            var playerCoreEntity = world.ECSManager.CreateEntity();

            world.ECSManager.AddComponent(
                playerCoreEntity,
                new PositionComp { position = position }
            );
            world.ECSManager.AddComponent(
                playerCoreEntity,
                new HealthComp { maxHealth = 100.0f, currentHealth = 100.0f }
            );
            world.ECSManager.AddComponent(
                playerCoreEntity,
                new PhysicsComp
                {
                    mass = 1.0f,
                    isSolid = true,
                    hitbox = new Rectangle(
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
                new HealthComp { maxHealth = maxHealth, currentHealth = maxHealth }
            );
            world.ECSManager.AddComponent(wallEntity, new PositionComp { position = position });
            world.ECSManager.AddComponent(
                wallEntity,
                new PhysicsComp
                {
                    mass = mass,
                    isSolid = true,
                    hitbox = new Rectangle(
                        0,
                        0,
                        world.Game.CoreSettings.DefaultTileSize,
                        world.Game.CoreSettings.DefaultTileSize
                    ),
                }
            );
            world.ECSManager.AddComponent(wallEntity, new BuildingComp { type = type });

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
            world.ECSManager.AddComponent(enemyEntity, new EnemyComp { type = type });
            world.ECSManager.AddComponent(enemyEntity, new PositionComp { position = position });
            world.ECSManager.AddComponent(
                enemyEntity,
                new HealthComp { maxHealth = maxHealth, currentHealth = maxHealth }
            );
            world.ECSManager.AddComponent(
                enemyEntity,
                new PhysicsComp
                {
                    mass = mass,
                    isSolid = true,
                    hitbox = new Rectangle(
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
                    type = WeaponType.Kamikaze,
                    damage = 10.0f,
                    range = 1.0f,
                    cooldown = 0.0f,
                }
            );

            return enemyEntity;
        }
    }
}
