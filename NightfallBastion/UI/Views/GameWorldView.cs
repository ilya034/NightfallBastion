using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NightfallBastion.Core;
using NightfallBastion.World;

namespace NightfallBastion.UI
{
    public class GameWorldView(NightfallBastionGame game) : View(game)
    {
        private Texture2D? _tilesetTexture;
        private SpriteFont? _font;

        public void Draw()
        {
            var transform = _game.GameWorld.GetCameraTransformMatrix();

            _game.SpriteBatch.Begin(transformMatrix: transform);
            DrawTileMap();
            _game.SpriteBatch.End();
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

                    Color tileColor = tile.GetRenderColor();

                    _game.SpriteBatch.Draw(
                        _tilesetTexture,
                        new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize),
                        tile.SourceRect,
                        tileColor
                    );

                    if (tile.MaxHealth > 0 && _font != null)
                    {
                        string healthText = $"{tile.CurrentHealth}";
                        Vector2 textSize = _font.MeasureString(healthText);
                        Vector2 textPosition = new Vector2(
                            x * tileSize + (tileSize - textSize.X) / 2,
                            y * tileSize + (tileSize - textSize.Y) / 2
                        );

                        _game.SpriteBatch.DrawString(_font, healthText, textPosition, Color.Black);
                    }
                }
            }
        }

        public override void LoadContent()
        {
            _tilesetTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.TilesetAssetName);
            _font = _game.Content.Load<SpriteFont>("Font");
        }
    }
}
