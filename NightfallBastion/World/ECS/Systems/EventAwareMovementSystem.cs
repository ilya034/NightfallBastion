using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;
using NightfallBastion.World.ECS.Events;

namespace NightfallBastion.World.ECS.Systems
{
    public class EventAwareMovementSystem(GameWorld world, EventBus eventBus) : System(world)
    {
        private readonly EventBus _eventBus = eventBus;

        public override void Update(GameTime gameTime)
        {
            var entities = _world.ECSManager.GetEntitiesWithComponents<
                PositionComp,
                MovementComp
            >();

            foreach (var entity in entities)
            {
                PositionComp? position = _world.ECSManager.GetComponent<PositionComp>(entity);
                MovementComp? movement = _world.ECSManager.GetComponent<MovementComp>(entity);

                if (position.HasValue && movement.HasValue && movement.Value.isMoving)
                {
                    var oldPosition = position.Value.position;
                    var newPosition = CalculateNewPosition(
                        position.Value,
                        movement.Value,
                        gameTime
                    );

                    if (Vector2.Distance(oldPosition, newPosition) > 0.1f)
                    {
                        var updatedPosition = position.Value;
                        updatedPosition.position = newPosition;
                        _world.ECSManager.AddComponent(entity, updatedPosition);
                        _eventBus.Publish(new ComponentChangedEvent(entity, typeof(PositionComp)));
                    }
                }
            }
        }

        private Vector2 CalculateNewPosition(
            PositionComp position,
            MovementComp movement,
            GameTime gameTime
        )
        {
            var direction = Vector2.Normalize(movement.nextPosition - position.position);
            var distance = movement.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            var newPosition = position.position + direction * distance;

            if (Vector2.Distance(newPosition, movement.nextPosition) < distance)
                return movement.nextPosition;

            return newPosition;
        }
    }
}
