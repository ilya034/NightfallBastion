using System;
using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World.ECS.Systems
{
    public class DamageSystem(GameWorld gameWorld) : System(gameWorld)
    {
        public override void Update(GameTime gameTime)
        {
            var entities = _world.ECSManager.GetEntitiesWithComponents<PositionComp, DamageComp>();

            foreach (var damage in entities)
            {
                var positionComp = _world.ECSManager.GetComponent<PositionComp>(damage);
                var damageComp = _world.ECSManager.GetComponent<DamageComp>(damage);

                if (damageComp.Piercing <= 0)
                {
                    _world.ECSManager.DestroyEntity(damage);
                    continue;
                }

                var targets = _world.ECSManager.GetEntitiesWithComponents<
                    TilePositionComp,
                    HealthComp
                >();

                foreach (var target in targets)
                {
                    var targetPosition = _world.ECSManager.GetComponent<TilePositionComp>(target);
                    var targetHealth = _world.ECSManager.GetComponent<HealthComp>(target);

                    if (damageComp.Piercing <= 0)
                    {
                        _world.ECSManager.DestroyEntity(damage);
                        continue;
                    }

                    if (
                        Vector2.Distance(
                            positionComp.Position,
                            _world.TileToWorld(targetPosition.Position)
                        ) <= damageComp.Range
                    )
                    {
                        Console.WriteLine(
                            $"Damage applied at {positionComp.Position} with damage {damageComp.Damage}"
                        );
                        targetHealth.CurrentHealth -= damageComp.Damage;
                        damageComp.Piercing -= 1;
                        _world.ECSManager.SetComponent(target, targetHealth);
                    }
                }
            }
        }
    }
}
