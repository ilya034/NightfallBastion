using System;
using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World
{
    public class MovementSystem(GameWorld gameWorld) : System(gameWorld)
    {
        public override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var entities = _world.ECSManager.GetEntitiesWithComponents<
                PositionComp,
                TilePositionComp,
                MovementComp
            >();

            foreach (var entity in entities)
            {
                var position = _world.ECSManager.GetComponent<PositionComp>(entity);
                var tilePosition = _world.ECSManager.GetComponent<TilePositionComp>(entity);
                var movement = _world.ECSManager.GetComponent<MovementComp>(entity);

                if (_world.ECSManager.GetComponent<HealthComp>(entity).currentHealth <= 0)
                    continue;

                if (!movement.isMoving)
                    continue;

                if ((movement.nextPosition - position.position).Length() < 0.01f)
                {
                    position.position = movement.nextPosition;
                    movement.isMoving = false;
                    continue;
                }

                var direction = movement.nextPosition - position.position;
                direction.Normalize();
                position.position += direction * movement.speed * deltaTime;

                tilePosition.tilePosition = new Vector2(
                    (int)position.position.X / 32,
                    (int)position.position.Y / 32
                );
            }
        }
    }
}
