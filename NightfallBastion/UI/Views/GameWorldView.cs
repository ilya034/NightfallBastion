using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NightfallBastion.Core;
using NightfallBastion.World;

namespace NightfallBastion.UI
{
    public class GameWorldView(NightfallBastionGame game) : BaseView(game)
    {
        private Texture2D? _tilesetTexture;

        public void Draw()
        {
            var camera = _game.GameWorld.ECSManager.GetComponent<CameraComponent>(_game.GameWorld.CameraEntity);
            if (camera == null)
                return;

            var transform = GetCameraTransform(camera);

            _game.SpriteBatch.Begin(transformMatrix: transform);
            DrawTileMap();
            _game.SpriteBatch.End();
        }

        private Matrix GetCameraTransform(CameraComponent camera)
        {
            var position = camera.Position;
            var scale = camera.Zoom;
            var rotation = camera.Rotation;

            return Matrix.CreateTranslation(new Vector3(-position, 0))
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateScale(new Vector3(scale, scale, 1))
                * Matrix.CreateTranslation(
                    new Vector3(
                        _game.GraphicsDevice.Viewport.Width / 2f,
                        _game.GraphicsDevice.Viewport.Height / 2f,
                        0
                    )
                );
        }

        private void DrawTileMap()
        {
            var tileMap = _game.GameWorld.TileMap;
            if (tileMap == null)
                return;

            int tileSize = _game.CoreSettings.DefaultTileSize;
            int mapWidth = _game.CoreSettings.DefaultMapWidth;
            int mapHeight = _game.CoreSettings.DefaultMapHeight;

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    var tile = tileMap.GetTile(x, y);
                    if (tile == null)
                        continue;

                    _game.SpriteBatch.Draw(
                        _tilesetTexture,
                        new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize),
                        tile.SourceRect,
                        Color.White
                    );
                }
            }
        }

        public override void LoadContent()
        {
            _tilesetTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.TilesetAssetName);
        }
    }
}
