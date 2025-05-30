using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NightfallBastion.Core;
using NightfallBastion.World.Enemies;

namespace NightfallBastion.World
{
    public class EnemySpawnSystem(GameWorld gameWorld) : System(gameWorld)
    {
        private float _timeSinceLastSpawn = 0f;
        private KeyboardState _previousKeyboardState;
        private readonly Random _random = new();

        public override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var currentKeyboardState = Keyboard.GetState();

            // Автоматический спавн по таймеру
            _timeSinceLastSpawn += deltaTime;
            if (_timeSinceLastSpawn >= _world.Game.GameplaySettings.EnemySpawnInterval)
            {
                SpawnEnemy();
                _timeSinceLastSpawn = 0f;
            }

            // Принудительный спавн по клавише Space
            if (
                currentKeyboardState.IsKeyDown(Keys.Space)
                && !_previousKeyboardState.IsKeyDown(Keys.Space)
            )
            {
                SpawnEnemy();
            }

            _previousKeyboardState = currentKeyboardState;
        }

        private void SpawnEnemy()
        {
            // Новый способ: ищем все тайлы с EnemySpawn
            var tileMap = _world.TileMap;
            if (tileMap == null)
                return;

            var spawnTiles = new List<Point>();
            for (int y = 0; y < tileMap.Height; y++)
            {
                for (int x = 0; x < tileMap.Width; x++)
                {
                    var tile = tileMap.GetTile(x, y);
                    if (tile?.Building is NightfallBastion.World.Buildings.EnemySpawn)
                    {
                        spawnTiles.Add(new Point(x, y));
                    }
                }
            }

            if (spawnTiles.Count == 0)
                return;

            // Выбираем случайный тайл для спавна
            var spawnTile = spawnTiles[_random.Next(spawnTiles.Count)];
            int tileSize = tileMap.TileSize;
            var spawnPosition = new Vector2(
                spawnTile.X * tileSize + tileSize / 2f,
                spawnTile.Y * tileSize + tileSize / 2f
            );

            // Центр карты как цель
            var mapSizeX = tileMap.Width * tileSize;
            var mapSizeY = tileMap.Height * tileSize;
            var corePosition = new Vector2(mapSizeX / 2f, mapSizeY / 2f);

            NightfallBastion.World.Enemies.EnemyFactory.CreateEnemy(
                _world.ECSManager,
                spawnPosition,
                corePosition,
                _world.Game.GameplaySettings,
                _world
            );
        }
    }
}
