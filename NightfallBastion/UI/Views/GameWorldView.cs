using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NightfallBastion.Core;
using NightfallBastion.World;

namespace NightfallBastion.UI
{
    public class GameWorldView(NightfallBastionGame game) : BaseView(game)
    {
        private Texture2D _tilesetTexture;

        public void Draw()
        {
            var camera = _game.GameWorld.ECSManager.GetComponent<CameraComponent>(
                _game.GameWorld.CameraEntity
            );

            var transform =
                Matrix.CreateTranslation(new Vector3(camera.Position, 0f))
                * Matrix.CreateRotationZ(camera.Rotation)
                * Matrix.CreateScale(camera.Zoom);

            _game.SpriteBatch.End();
            _game.SpriteBatch.Begin(transformMatrix: transform);
            DrawTileMap();
        }

        public void DrawTileMap()
        {
            for (int y = 0; y < _game.Settings.DefaultMapHeight; y++)
            {
                for (int x = 0; x < _game.Settings.DefaultMapWidth; x++)
                {
                    var tileMap = _game.GameWorld.TileMap;
                    if (tileMap == null)
                        continue;
                    var tile = tileMap.GetTile(x, y);
                    if (tile != null)
                        _game.SpriteBatch.Draw(
                            _tilesetTexture,
                            new Rectangle(
                                x * _game.Settings.DefaultTileSize,
                                y * _game.Settings.DefaultTileSize,
                                _game.Settings.DefaultTileSize,
                                _game.Settings.DefaultTileSize
                            ),
                            tile.SourceRect,
                            Color.White
                        );
                }
            }
        }

        public override void LoadContent()
        {
            _tilesetTexture = _game.Content.Load<Texture2D>(_game.Settings.TilesetAssetName);
        }
    }
}
