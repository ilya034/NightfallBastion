using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.World
{
    public class GameWorld(NightfallBastionGame game)
    {
        public NightfallBastionGame Game { get; } = game;
        public ECSManager ECSManager { get; } = new();
        public Entity CameraEntity { get; private set; }
        public TileMap TileMap { get; private set; }

        public void LoadContent()
        {
            CreteCamera();

            TileMap = new TileMap(
                Game.Settings.DefaultMapWidth,
                Game.Settings.DefaultMapHeight,
                Game.Settings.DefaultTileSize
            );

            for (int y = 0; y < Game.Settings.DefaultMapHeight; y++)
            {
                for (int x = 0; x < Game.Settings.DefaultMapWidth; x++)
                {
                    var tile = new Tile(
                        new Rectangle(
                            Game.Settings.DefaultTileTextureX,
                            Game.Settings.DefaultTileTextureY,
                            Game.Settings.DefaultTileSize,
                            Game.Settings.DefaultTileSize
                        )
                    );
                    TileMap.SetTile(x, y, tile);
                }
            }
        }

        private void CreteCamera()
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
