using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NightfallBastion.Core;
using NightfallBastion.Utilities;

namespace NightfallBastion.UI
{
    public class GameWorldView(NightfallBastionGame game) : View(game)
    {
        private Texture2D? _tileTexture;
        private Texture2D? _enemyTexture;
        private Texture2D? _wallTexture;
        private SpriteFont? _font;
        public RenderData _renderData;

        public override void LoadContent()
        {
            _tileTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.TilesetAssetName);
            _wallTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.WallAssetName);
            _enemyTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.EnemyAssetName);
            _font = _game.Content.Load<SpriteFont>("Font");
        }

        public override void UnloadContent()
        {
            _tileTexture = null;
            _enemyTexture = null;
            _font = null;
        }

        public override void Draw()
        {
            _game.SpriteBatch.Begin(transformMatrix: _renderData.cameraTransform);

            RenderTileMap();
            RenderBuildings();
            RenderEnemies();

            _game.SpriteBatch.End();
        }

        public void UpdateRenderData(RenderData renderData) => _renderData = renderData;

        public void RenderTileMap()
        {
            var tileMapData = _renderData.tileMapData;

            if (tileMapData.tiles == null)
                return;

            for (int x = 0; x < tileMapData.width; x++)
            {
                for (int y = 0; y < tileMapData.height; y++)
                {
                    var tile = tileMapData.tiles[x, y];

                    var position = new Vector2(
                        x * _game.CoreSettings.DefaultTileSize,
                        y * _game.CoreSettings.DefaultTileSize
                    );

                    _game.SpriteBatch.Draw(_tileTexture, position, Color.White);
                }
            }
        }

        public void RenderBuildings()
        {
            var entityData = _renderData.buildingData;

            foreach (var entity in entityData)
            {
                Color buildingColor = Color.White;
                if (entity.MaxHealth > 0)
                {
                    float healthRatio = entity.Health / entity.MaxHealth;
                    if (healthRatio > 0.7f)
                        buildingColor = Color.White;
                    else if (healthRatio > 0.3f)
                        buildingColor = Color.Yellow;
                    else
                        buildingColor = Color.Red;
                }

                _game.SpriteBatch.Draw(
                    _wallTexture,
                    UtilMethods.GetDestinationRect(
                        entity.position,
                        _game.CoreSettings.DefaultTileSize
                    ),
                    _game.CoreSettings.DefaultTextureRectangle,
                    buildingColor
                );

                // if (_showHealthDisplay && building.MaxHealth > 0)
                //     DrawEntityHealthBar(building, destinationRect);
            }
        }

        public void RenderEnemies()
        {
            var entityData = _renderData.enemyData;

            foreach (var entity in entityData)
            {
                Color entityColor = Color.White;
                if (entity.maxHealth > 0)
                {
                    float healthRatio = entity.health / entity.maxHealth;
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
                        entity.position,
                        _game.CoreSettings.DefaultTileSize
                    ),
                    _game.CoreSettings.DefaultTextureRectangle,
                    entityColor
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
