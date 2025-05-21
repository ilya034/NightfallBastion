using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NightfallBastion.Core;

namespace NightfallBastion.World
{
    public class GameWorld(NightfallBastionGame game)
    {
        private readonly NightfallBastionGame _game = game;
        private readonly ECSManager _ecsManager = new();
        private TileMap? _tileMap;
        private Texture2D? _tilesetTexture;

        public void LoadContent()
        {
            _tilesetTexture = _game.Content.Load<Texture2D>(_game.Settings.TilesetAssetName);
            _tileMap = new TileMap(
                _game.Settings.DefaultMapWidth,
                _game.Settings.DefaultMapHeight,
                _game.Settings.DefaultTileSize
            );
            for (int y = 0; y < _game.Settings.DefaultMapHeight; y++)
            {
                for (int x = 0; x < _game.Settings.DefaultMapWidth; x++)
                {
                    var tile = new Tile(
                        _tilesetTexture,
                        new Rectangle(
                            _game.Settings.DefaultTileTextureX,
                            _game.Settings.DefaultTileTextureY,
                            _game.Settings.DefaultTileSize,
                            _game.Settings.DefaultTileSize
                        )
                    );
                    _tileMap.SetTile(x, y, tile);
                }
            }
        }

        public void Update(GameTime gameTime) => _ecsManager.Update(gameTime);

        public void Draw(SpriteBatch spriteBatch) => _tileMap?.Draw(spriteBatch);
    }
}
