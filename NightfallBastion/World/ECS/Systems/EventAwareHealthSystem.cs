using System;
using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;
using NightfallBastion.World.ECS.Events;

namespace NightfallBastion.World.ECS.Systems
{
    public class EventAwareHealthSystem(GameWorld world, EventBus eventBus) : System(world)
    {
        private readonly EventBus _eventBus = eventBus;

        public override void Update(GameTime gameTime)
        {
            var entities = _world.ECSManager.GetEntitiesWithComponents<HealthComp>();

            foreach (var entity in entities)
            {
                HealthComp? health = _world.ECSManager.GetComponent<HealthComp>(entity);
                if (health.HasValue)
                {
                    var oldHealth = health.Value.currentHealth;
                    var newHealth = ProcessHealthChanges(health.Value, gameTime);

                    if (Math.Abs(oldHealth - newHealth.currentHealth) > 0.001f)
                    {
                        _world.ECSManager.AddComponent(entity, newHealth);

                        _eventBus.Publish(
                            new HealthChangedEvent(
                                entity,
                                newHealth.currentHealth,
                                newHealth.maxHealth
                            )
                        );

                        if (newHealth.currentHealth <= 0)
                        {
                            _eventBus.Publish(new EntityDestroyedEvent(entity));
                            _world.ECSManager.DestroyEntity(entity);
                        }
                    }
                }
            }
        }

        private HealthComp ProcessHealthChanges(HealthComp health, GameTime gameTime)
        {
            return health;
        }
    }
}
