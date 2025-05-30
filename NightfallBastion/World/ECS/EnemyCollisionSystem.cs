using System.Collections.Generic;
using Microsoft.Xna.Framework;
using NightfallBastion.World.Buildings;

namespace NightfallBastion.World
{
    public class EnemyCollisionSystem(GameWorld gameWorld) : System(gameWorld)
    {
        public override void Update(GameTime gameTime)
        {
            var enemies = _world.ECSManager.GetEntitiesWithComponents<
                PositionComponent,
                EnemyComponent
            >();
            var enemiesToRemove = new List<Entity>();

            foreach (var enemy in enemies)
            {
                var position = _world.ECSManager.GetComponent<PositionComponent>(enemy);
                var enemyComp = _world.ECSManager.GetComponent<EnemyComponent>(enemy);

                if (!enemyComp.IsAlive)
                    continue;

                var tileX = (int)(position.Position.X / 32);
                var tileY = (int)(position.Position.Y / 32);

                if (tileX < 0 || tileX >= 100 || tileY < 0 || tileY >= 100)
                {
                    enemiesToRemove.Add(enemy);
                    continue;
                }

                var tile = _world.TileMap.GetTile(tileX, tileY);

                if (tile?.Building is Wall wall)
                {
                    var damageEntity = _world.ECSManager.CreateEntity();
                    _world.ECSManager.AddComponent(
                        damageEntity,
                        new DamageComponent(new Point(tileX, tileY), enemyComp.Damage)
                    );

                    enemiesToRemove.Add(enemy);
                }
                else if (tileX == 50 && tileY == 50)
                    enemiesToRemove.Add(enemy);
            }

            foreach (var enemy in enemiesToRemove)
                _world.ECSManager.RemoveEntity(enemy);
        }
    }
}
