using System;
using Microsoft.Xna.Framework;
using NightfallBastion.Utilities;

namespace NightfallBastion.World.ECS.Systems
{
    public class EnemySpawnSystem(GameWorld gameWorld) : System(gameWorld)
    {
        private readonly Random _random = new();
        private float _spawnTimer = 0.0f;

        public override void Update(GameTime gameTime)
        {
            _spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_spawnTimer >= 5.0f)
            {
                EntitiesFactory.CreateEnemy(
                    _world,
                    EnemyType.Boy,
                    new Vector2(
                        _world.Game.CoreSettings.DefaultTileSize * 2 + 16,
                        _world.Game.CoreSettings.DefaultTileSize * 2 + 16
                    ),
                    100f
                );
                _spawnTimer = 0.0f;
            }
        }
    }
}
