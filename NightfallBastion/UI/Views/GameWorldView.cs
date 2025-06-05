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

        // private Texture2D _coreTexture;
        private Texture2D? _mineTexture;
        private SpriteFont? _font;
        public RenderData _renderData;

        public override void LoadContent()
        {
            _tileTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.TilesetAssetName);
            _wallTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.WallAssetName);
            _strongWallTexture = _game.Content.Load<Texture2D>(
                _game.CoreSettings.StrongWallAssetName
            );
            // _coreTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.CoreAssetName);
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

        public void RenderTileMap()
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

                    _game.SpriteBatch.DrawString(_font, distance, position, Color.White);
                }
            }
        }

        public void RenderBuildings()
        {
            var entityData = _renderData.BuildingData;

            foreach (var entity in entityData)
            {
                var texture = entity.Type switch
                {
                    BuildingType.Wall => _wallTexture,
                    BuildingType.StrongWall => _strongWallTexture,
                    BuildingType.Core => _strongWallTexture,
                    BuildingType.Mine => _mineTexture,
                    _ => _tileTexture,
                };

                _game.SpriteBatch.Draw(
                    texture,
                    UtilMethods.GetDestinationRect(
                        entity.Position,
                        _game.CoreSettings.DefaultTileSize
                    ),
                    _game.CoreSettings.DefaultTextureRectangle,
                    Color.WhiteSmoke
                );

                // if (_showHealthDisplay && building.MaxHealth > 0)
                //     DrawEntityHealthBar(building, destinationRect);
            }
        }

        public void RenderEnemies()
        {
            var entityData = _renderData.EnemyData;

            foreach (var entity in entityData)
            {
                Color entityColor = Color.White;
                if (entity.MaxHealth > 0)
                {
                    float healthRatio = entity.Health / entity.MaxHealth;
                    if (healthRatio > 0.7f)
                        entityColor = Color.White;
                    else if (healthRatio > 0.3f)
                        entityColor = Color.Yellow;
                    else
                        entityColor = Color.Red;
                }

                _game.SpriteBatch.Draw(
                    _enemyTexture,
                    UtilMethods.GetDestinationRect(
                        entity.Position,
                        _game.CoreSettings.DefaultTileSize * 2
                    ),
                    _game.CoreSettings.DefaultTextureRectangle,
                    Color.White
                );

                // if (_showHealthDisplay && entity.maxHealth > 0)
                //     DrawEntityHealthBar(entity, entity.position);
            }
        }

        // private void DrawWallHealth(TileViewModel tile, Rectangle position)
        // {
        //     if (_font == null || tile.Health < 0 || tile.MaxHealth <= 0)
        //         return;

        //     string healthText = $"{tile.Health}/{tile.MaxHealth}";
        //     Vector2 textSize = _font.MeasureString(healthText);
        //     Vector2 textPosition = new(
        //         position.X + (position.Width - textSize.X) / 2,
        //         position.Y + (position.Height - textSize.Y) / 2
        //     );

        //     Color textColor = (float)tile.Health / tile.MaxHealth > 0.3f ? Color.Black : Color.White;

        //     _game.SpriteBatch.DrawString(_font, healthText, textPosition, textColor);
        // }

        // private void DrawEnemyHealthBar(EnemyViewModel enemy, Vector2 position)
        // {
        //     if (enemy.MaxHealth <= 0)
        //         return;

        //     int barWidth = 30;
        //     int barHeight = 4;
        //     Vector2 barPosition = new(position.X, position.Y - 10);

        //     var backgroundRect = new Rectangle(
        //         (int)barPosition.X,
        //         (int)barPosition.Y,
        //         barWidth,
        //         barHeight
        //     );
        //     _game.SpriteBatch.Draw(_tilesetTexture, backgroundRect, Color.DarkRed);

        //     float healthRatio = (float)enemy.Health / enemy.MaxHealth;
        //     int healthWidth = (int)(barWidth * healthRatio);
        //     var healthRect = new Rectangle(
        //         (int)barPosition.X,
        //         (int)barPosition.Y,
        //         healthWidth,
        //         barHeight
        //     );
        //     _game.SpriteBatch.Draw(_tilesetTexture, healthRect, Color.Green);
        // }
    }
}
