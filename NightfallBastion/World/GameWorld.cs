using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.World
{
    public class GameWorld(NightfallBastionGame game)
    {
        public NightfallBastionGame Game { get; private set; } = game;
        public ECSManager ECSManager { get; private set; } = new();
        public Entity? CameraEntity { get; private set; }
        public TileMap? TileMap { get; private set; }

        public void LoadContent()
        {
            CreateCamera();

            TileMap = new TileMap(
                Game.CoreSettings.DefaultMapWidth,
                Game.CoreSettings.DefaultMapHeight,
                Game.CoreSettings.DefaultTileSize
            );

            for (int y = 0; y < Game.CoreSettings.DefaultMapHeight; y++)
            {
                for (int x = 0; x < Game.CoreSettings.DefaultMapWidth; x++)
                {
                    Tile tile;
                    if (
                        x == 0
                        || y == 0
                        || x == Game.CoreSettings.DefaultMapWidth - 1
                        || y == Game.CoreSettings.DefaultMapHeight - 1
                    )
                    {
                        tile = new Tile(
                            new Rectangle(
                                Game.CoreSettings.DefaultTileTextureX + Game.CoreSettings.DefaultTileSize,
                                Game.CoreSettings.DefaultTileTextureY,
                                Game.CoreSettings.DefaultTileSize,
                                Game.CoreSettings.DefaultTileSize
                            ),
                            true,
                            TileType.Wall
                        );
                    }
                    else if (x == 2 && y == 2)
                    {
                        tile = new Tile(
                            new Rectangle(
                                Game.CoreSettings.DefaultTileTextureX + 2 * Game.CoreSettings.DefaultTileSize,
                                Game.CoreSettings.DefaultTileTextureY,
                                Game.CoreSettings.DefaultTileSize,
                                Game.CoreSettings.DefaultTileSize
                            ),
                            false,
                            TileType.EnemySpawn
                        );
                    }
                    else if (
                        x == Game.CoreSettings.DefaultMapWidth / 2
                        && y == Game.CoreSettings.DefaultMapHeight / 2
                    )
                    {
                        tile = new Tile(
                            new Rectangle(
                                Game.CoreSettings.DefaultTileTextureX + 3 * Game.CoreSettings.DefaultTileSize,
                                Game.CoreSettings.DefaultTileTextureY,
                                Game.CoreSettings.DefaultTileSize,
                                Game.CoreSettings.DefaultTileSize
                            ),
                            false,
                            TileType.PlayerCore
                        );
                    }
                    else
                    {
                        tile = new Tile(
                            new Rectangle(
                                Game.CoreSettings.DefaultTileTextureX,
                                Game.CoreSettings.DefaultTileTextureY,
                                Game.CoreSettings.DefaultTileSize,
                                Game.CoreSettings.DefaultTileSize
                            ),
                            false,
                            TileType.Empty
                        );
                    }
                    TileMap.SetTile(x, y, tile);
                }
            }
        }

        private void CreateCamera()
        {
            CameraEntity = ECSManager.CreateEntity();
            ECSManager.AddComponent(CameraEntity, new CameraComponent());
            ECSManager.AddSystem(new CameraSystem(this));
        }

        public void Update(GameTime gameTime)
        {
            ECSManager.Update(gameTime);
        }
    }
}
