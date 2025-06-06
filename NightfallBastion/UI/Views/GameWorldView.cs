using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NightfallBastion.Core;
using NightfallBastion.Utilities;
using NightfallBastion.World;

namespace NightfallBastion.UI
{
    public class GameWorldView(NightfallBastionGame game) : View(game)
    {
        private Texture2D? _tileTexture;
        private Texture2D? _enemyTexture;
        private Texture2D? _wallTexture;
        private Texture2D? _strongWallTexture;

        private Texture2D _coreTexture;
        private Texture2D? _mineTexture;
        private SpriteFont? _font;
        public RenderData _renderData;

        public override void LoadContent()
        {
            _tileTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.TileAssetName);
            _wallTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.WallAssetName);
            _strongWallTexture = _game.Content.Load<Texture2D>(
                _game.CoreSettings.StrongWallAssetName
            );
            _coreTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.CoreAssetName);
            _mineTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.MineAssetName);
            _enemyTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.EnemyAssetName);
            _font = _game.Content.Load<SpriteFont>("Font");
        }

        public override void Draw()
        {
            _game.SpriteBatch.Begin(transformMatrix: _renderData.CameraTransform);

            RenderTileMap();
            RenderBuildings();
            RenderEnemies();

            _game.SpriteBatch.End();
        }

        public void UpdateRenderData(RenderData renderData) => _renderData = renderData;

        private void RenderTileMap()
        {
            var tileMapData = _renderData.TileMapData;

            if (tileMapData.Tiles == null)
                return;

            for (int x = 0; x < tileMapData.Width; x++)
            {
                for (int y = 0; y < tileMapData.Height; y++)
                {
                    var tile = tileMapData.Tiles[x, y];

                    var position = new Vector2(
                        x * _game.CoreSettings.DefaultTileSize,
                        y * _game.CoreSettings.DefaultTileSize
                    );

                    _game.SpriteBatch.Draw(_tileTexture, position, Color.DimGray);

                    string distance;
                    if (tile.Distance == float.PositiveInfinity)
                        distance = "Inf";
                    else
                        distance = tile.Distance.ToString();

                    _game.SpriteBatch.DrawString(
                        _font,
                        distance,
                        position
                            + new Vector2(
                                _game.CoreSettings.DefaultTileSize / 2f,
                                _game.CoreSettings.DefaultTileSize / 2f
                            ),
                        Color.DimGray,
                        0f,
                        _font.MeasureString(distance) / 2f,
                        0.75f,
                        SpriteEffects.None,
                        0f
                    );
                }
            }
        }

        private void RenderBuildings()
        {
            var entityData = _renderData.BuildingData;

            foreach (var entity in entityData)
            {
                var texture = entity.Type switch
                {
                    BuildingType.Wall => _wallTexture,
                    BuildingType.StrongWall => _strongWallTexture,
                    BuildingType.Core => _coreTexture,
                    BuildingType.Mine => _mineTexture,
                    _ => _tileTexture,
                };

                _game.SpriteBatch.Draw(
                    texture,
                    GetDestinationRect(entity.Position),
                    _game.CoreSettings.DefaultTextureRectangle,
                    Color.WhiteSmoke
                );

                DrawBuildingHealth(entity.Health, entity.Position);
            }
        }

        private void RenderEnemies()
        {
            var entityData = _renderData.EnemyData;

            foreach (var entity in entityData)
            {
                _game.SpriteBatch.Draw(
                    _enemyTexture,
                    GetDestinationRect(entity.Position),
                    _game.CoreSettings.DefaultTextureRectangle,
                    Color.White
                );
            }
        }

        private void DrawBuildingHealth(float health, Vector2 position)
        {
            _game.SpriteBatch.DrawString(
                _font,
                health.ToString(),
                position,
                Color.DarkGray,
                0f,
                _font.MeasureString(health.ToString()) / 2f,
                0.75f,
                SpriteEffects.None,
                0f
            );
        }

        public Rectangle GetDestinationRect(Vector2 position)
        {
            var tileSize = _game.CoreSettings.DefaultTileSize;
            position -= new Vector2(tileSize / 2f, tileSize / 2f);
            return new Rectangle((int)position.X, (int)position.Y, tileSize, tileSize);
        }
    }
}
