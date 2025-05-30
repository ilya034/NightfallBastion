using System;
using Microsoft.Xna.Framework;

namespace NightfallBastion.World
{
    public class EnemyMovementSystem(GameWorld gameWorld) : System(gameWorld)
    {
        public override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var enemies = _world.ECSManager.GetEntitiesWithComponents<
                PositionComponent,
                MovementComponent,
                EnemyComponent
            >();

            foreach (var enemy in enemies)
            {
                var position = _world.ECSManager.GetComponent<PositionComponent>(enemy);
                var movement = _world.ECSManager.GetComponent<MovementComponent>(enemy);
                var enemyComp = _world.ECSManager.GetComponent<EnemyComponent>(enemy);

                if (!enemyComp.IsAlive)
                    continue;

                position.PreviousPosition = position.Position;

                var targetVector = movement.Target - position.Position;
                var distance = targetVector.Length();

                if (distance < 5.0f)
                {
                    Console.WriteLine($"Enemy reached target at {position.Position}");
                    continue;
                }

                var direction = Vector2.Normalize(targetVector);
                var velocity = direction * movement.Speed * deltaTime;

                position.Position += velocity;
                movement.Velocity = velocity;

                if (gameTime.TotalGameTime.TotalSeconds % 2.0 < 0.016)
                {
                    var logMessage =
                        $"Enemy at {position.Position}, moving to {movement.Target}, distance: {distance:F1}";
                    Console.WriteLine(logMessage);
                }
            }
        }
    }
}
