using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World
{
    public static class EntitiesFactory
    {
        public static Entity CreateTileMap(GameWorld world, int width, int height)
        {
            var tiles = new TileData[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = new TileData
                    {
                        floor = Floor.defaultFloor,
                        speedMultiplier = 1.0f,
                    };

                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        tiles[x, y] = new TileData
                        {
                            floor = Floor.defaultFloor,
                            speedMultiplier = 0.0f,
                        };

                        var wallEntity = CreateWall(
                            world,
                            100.0f,
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

        public static Entity CreateWall(
            GameWorld world,
            float maxHealth,
            Vector2 position,
            float mass = 1.0f
        )
        {
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
                    type = WeaponType.kamikaze,
                    damage = 10.0f,
                    range = 1.0f,
                    cooldown = 0.0f,
                }
            );

            return enemyEntity;
        }
    }
}
