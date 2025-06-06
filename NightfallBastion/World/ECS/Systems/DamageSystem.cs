using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World.ECS.Systems
{
    public class DamageSystem(GameWorld gameWorld) : System(gameWorld)
    {
        public override void Update(GameTime gameTime)
        {
            var targets = _world.ECSManager.GetEntitiesWithComponents<DamageComp, HealthComp>();

            foreach (var target in targets)
            {
                var healthComp = _world.ECSManager.GetComponent<HealthComp>(target);
                var damageComp = _world.ECSManager.GetComponent<DamageComp>(target);

                healthComp.CurrentHealth -= damageComp.Damage;

                _world.ECSManager.SetComponent(target, healthComp);
                _world.ECSManager.RemoveComponent<DamageComp>(target);
            }
        }
    }
}
