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

                    for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        var newX = tileX + dx;
                        var newY = tileY + dy;

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
                    }

                    if (bestX != tileX || bestY != tileY)
                    {
                        movementComp.NextPosition =
                            new Vector2(bestX, bestY) * _world.Game.CoreSettings.DefaultTileSize
                            + new Vector2(1, 1) * _world.Game.CoreSettings.DefaultTileSize / 2;
                        movementComp.IsMoving = true;
                        _world.ECSManager.SetComponent(enemy, movementComp);
                    }
                }

                var targetVector = movementComp.NextPosition - position;
                if (targetVector.Length() < 0.1f)
                {
                    movementComp.IsMoving = false;
                    _world.ECSManager.SetComponent(enemy, movementComp);
                    continue;
                }

                var direction = targetVector;
                direction.Normalize();
                var delta = direction * movementComp.Speed * deltaTime;

                if (delta.Length() > targetVector.Length())
                    positionComp.Position = movementComp.NextPosition;
                else
                    positionComp.Position += delta;

                _world.ECSManager.SetComponent(enemy, positionComp);
            }
        }
    }
}
