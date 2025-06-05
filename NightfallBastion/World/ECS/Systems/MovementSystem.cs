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
                MovementComp
            >();

            foreach (var entity in entities)
            {
                var position = _world.ECSManager.GetComponent<PositionComp>(entity);
                var movement = _world.ECSManager.GetComponent<MovementComp>(entity);

                if (_world.ECSManager.GetComponent<HealthComp>(entity).CurrentHealth <= 0)
                    continue;

                if (!movement.IsMoving)
                    continue;

                if ((movement.NextPosition - position.Position).Length() < 0.01f)
                {
                    position.Position = movement.NextPosition;
                    movement.IsMoving = false;
                    continue;
                }

                var direction = movement.NextPosition - position.Position;
                direction.Normalize();
                position.Position += direction * movement.Speed * deltaTime;
            }
        }
    }
}
