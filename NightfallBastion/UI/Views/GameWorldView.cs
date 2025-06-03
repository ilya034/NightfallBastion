using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NightfallBastion.Core;
using NightfallBastion.UI.ViewModels;

namespace NightfallBastion.UI
{
    public class GameWorldView(NightfallBastionGame game) : View(game)
    {
        private Texture2D? _tilesetTexture;
        private Texture2D? _enemyTexture;
        private SpriteFont? _font;
        public TileMapViewModel? CurrentTileMapViewModel { get; set; }
        public CameraViewModel? CurrentCameraViewModel { get; set; }
        private List<EnemyViewModel> _currentEnemyViewModels = new();
        public List<EnemyViewModel> CurrentEnemyViewModels => _currentEnemyViewModels;

        private bool _showHealthDisplay = true;

        public void Draw()
        {
            var transform = GetCameraTransformMatrix();

            _game.SpriteBatch.Begin(transformMatrix: transform);

            if (CurrentTileMapViewModel != null)
                RenderTileMap(CurrentTileMapViewModel);

            if (_currentEnemyViewModels.Any())
                RenderEnemies(_currentEnemyViewModels);

            _game.SpriteBatch.End();
        }

        public void UpdateEnemies(List<EnemyViewModel> enemies)
        {
            _currentEnemyViewModels.Clear();
            if (enemies != null)
                _currentEnemyViewModels.AddRange(enemies);
        }

        public void RenderTileMap(TileMapViewModel tileMapData)
        {
            if (tileMapData?.Tiles == null || _tilesetTexture == null)
                return;

            int tileSize = _game.CoreSettings.DefaultTileSize;
            int renderedTiles = 0;

            for (int i = 0; i < tileMapData.Tiles.Length; i++)
            {
                var tile = tileMapData.Tiles[i];
                if (tile == null)
                    continue;

                var position = new Rectangle(
                    tile.X * tileSize,
                    tile.Y * tileSize,
                    tileSize,
                    tileSize
                );

                // Если у тайла есть ХП и MaxHealth — считаем, что это стена
                bool isWall = tile.Health > 0 && tile.MaxHealth > 0;

                Color tileColor = isWall
                    ? GetWallColor(tile.Health, tile.MaxHealth)
                    : GetTileColor(tile.TileType);

                _game.SpriteBatch.Draw(_tilesetTexture, position, tile.TextureRegion, tileColor);
                renderedTiles++;

                if (_showHealthDisplay && isWall && _font != null)
                    DrawWallHealth(tile, position);
            }
        }

        public void RenderEnemies(IEnumerable<EnemyViewModel> enemies)
        {
            if (_enemyTexture == null)
                return;

            foreach (var enemy in enemies)
            {
                var position = new Vector2(enemy.X, enemy.Y);

                var scale = enemy.Scale;

                Color enemyColor = GetEnemyColor(enemy);

                _game.SpriteBatch.Draw(
                    _enemyTexture,
                    position,
                    enemy.TextureRegion,
                    enemyColor,
                    0f,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    0f
                );

                if (_showHealthDisplay && enemy.MaxHealth > 0)
                    DrawEnemyHealthBar(enemy, position);
            }
        }

        public void SetHealthDisplayMode(bool showHealth)
        {
            _showHealthDisplay = showHealth;
        }

        public override void LoadContent()
        {
            _tilesetTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.TilesetAssetName);
            _enemyTexture = _game.Content.Load<Texture2D>(_game.CoreSettings.EnemyAssetName);
            _font = _game.Content.Load<SpriteFont>("Font");
        }

        public override void UnloadContent()
        {
            _tilesetTexture = null;
            _enemyTexture = null;
            _font = null;
        }

        private Matrix GetCameraTransformMatrix()
        {
            if (CurrentCameraViewModel == null)
                return Matrix.Identity;

            return Matrix.CreateTranslation(-CurrentCameraViewModel.X, -CurrentCameraViewModel.Y, 0)
                * Matrix.CreateScale(CurrentCameraViewModel.Zoom, CurrentCameraViewModel.Zoom, 1)
                * Matrix.CreateTranslation(
                    _game.GraphicsDevice.Viewport.Width * 0.5f,
                    _game.GraphicsDevice.Viewport.Height * 0.5f,
                    0
                );
        }

        private Color GetTileColor(int tileType)
        {
            return tileType switch
            {
                0 => Color.White,
                1 => Color.Gray,
                2 => Color.Red,
                3 => Color.Blue,
                _ => Color.White,
            };
        }

        private Color GetWallColor(int health, int maxHealth)
        {
            if (maxHealth <= 0) return Color.White;
            float ratio = (float)health / maxHealth;
            if (ratio > 0.7f)
                return Color.LimeGreen;
            else if (ratio > 0.3f)
                return Color.Gold;
            else
                return Color.Red;
        }

        private Color GetEnemyColor(EnemyViewModel enemy)
        {
            if (enemy.MaxHealth > 0)
            {
                float healthRatio = (float)enemy.Health / enemy.MaxHealth;
                if (healthRatio > 0.7f)
                    return Color.White;
                else if (healthRatio > 0.3f)
                    return Color.Yellow;
                else
                    return Color.Red;
            }

            return Color.White;
        }

        private void DrawWallHealth(TileViewModel tile, Rectangle position)
        {
            if (_font == null || tile.Health < 0 || tile.MaxHealth <= 0)
                return;

            string healthText = $"{tile.Health}/{tile.MaxHealth}";
            Vector2 textSize = _font.MeasureString(healthText);
            Vector2 textPosition = new(
                position.X + (position.Width - textSize.X) / 2,
                position.Y + (position.Height - textSize.Y) / 2
            );

            Color textColor = (float)tile.Health / tile.MaxHealth > 0.3f ? Color.Black : Color.White;

            _game.SpriteBatch.DrawString(_font, healthText, textPosition, textColor);
        }

        private void DrawEnemyHealthBar(EnemyViewModel enemy, Vector2 position)
        {
            if (enemy.MaxHealth <= 0)
                return;

            int barWidth = 30;
            int barHeight = 4;
            Vector2 barPosition = new(position.X - barWidth / 2, position.Y - 10);

            var backgroundRect = new Rectangle(
                (int)barPosition.X,
                (int)barPosition.Y,
                barWidth,
                barHeight
            );
            _game.SpriteBatch.Draw(_tilesetTexture, backgroundRect, Color.DarkRed);

            float healthRatio = (float)enemy.Health / enemy.MaxHealth;
            int healthWidth = (int)(barWidth * healthRatio);
            var healthRect = new Rectangle(
                (int)barPosition.X,
                (int)barPosition.Y,
                healthWidth,
                barHeight
            );
            _game.SpriteBatch.Draw(_tilesetTexture, healthRect, Color.Green);
        }
    }
}
