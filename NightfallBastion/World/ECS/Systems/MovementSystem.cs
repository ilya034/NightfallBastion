using System;
using System.Linq;
using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World
{
    public class MovementSystem(GameWorld gameWorld) : System(gameWorld)
    {
        public override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var distanceMapEntity = _world
                .ECSManager.GetEntitiesWithComponents<DistanceMapComp>()
                .FirstOrDefault();

            var distanceMapComp = _world.ECSManager.GetComponent<DistanceMapComp>(
                distanceMapEntity
            );
            var distanceMap = distanceMapComp.Distances;

            var enemies = _world.ECSManager.GetEntitiesWithComponents<
                EnemyComp,
                PositionComp,
                MovementComp
            >();

            int[] dx = [0, 0, -1, 1];
            int[] dy = [-1, 1, 0, 0];

            foreach (var enemy in enemies)
            {
                var positionComp = _world.ECSManager.GetComponent<PositionComp>(enemy);
                var movementComp = _world.ECSManager.GetComponent<MovementComp>(enemy);

                var position = positionComp.Position;
                var tilePosition = _world.WorldToTile(position);
                var tileX = (int)tilePosition.X;
                var tileY = (int)tilePosition.Y;

                if (distanceMap[tileX, tileY] == 0)
                    continue;

                if (!movementComp.IsMoving)
                {
                    var bestDistance = distanceMap[tileX, tileY];
                    var bestX = tileX;
                    var bestY = tileY;
                    for (int i = 0; i < 4; i++)
                    {
                        var newX = tileX + dx[i];
                        var newY = tileY + dy[i];

                        if (
                            newX < 0
                            || newX >= distanceMap.GetLength(0)
                            || newY < 0
                            || newY >= distanceMap.GetLength(1)
                        )
                            continue;

                        var newDistance = distanceMap[newX, newY];
                        if (newDistance < bestDistance)
                        {
                            bestDistance = newDistance;
                            bestX = newX;
                            bestY = newY;
                        }

                        if (bestX != tileX || bestY != tileY)
                        {
                            movementComp.NextPosition =
                                new Vector2(bestX, bestY)
                                * _world.Game.CoreSettings.DefaultTileSize;
                            movementComp.IsMoving = true;
                        }
                    }
                }

                var target = movementComp.NextPosition;
                var direction = target - position;
                if (direction.Length() < 0.01f)
                {
                    movementComp.IsMoving = false;
                    continue;
                }

                direction.Normalize();
                var delta = -direction * movementComp.Speed * deltaTime;
                positionComp.Position += delta;

                _world.ECSManager.SetComponent(enemy, positionComp);
                _world.ECSManager.SetComponent(enemy, movementComp);
            }
        }
    }
}
