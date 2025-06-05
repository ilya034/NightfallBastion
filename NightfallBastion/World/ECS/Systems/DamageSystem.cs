using Microsoft.Xna.Framework;
using NightfallBastion.Utilities;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World.ECS.Systems
{
    public class DamageSystem(GameWorld gameWorld) : System(gameWorld)
    {
        public override void Update(GameTime gameTime)
        {
            var entities = _world.ECSManager.GetEntitiesWithComponents<PositionComp, HealthComp>();

            foreach (var entity in entities)
            {
                var position = _world.ECSManager.GetComponent<PositionComp>(entity);
                var health = _world.ECSManager.GetComponent<HealthComp>(entity);

                if (health.currentHealth <= 0)
                {
                    continue;
                }
            }
        }
    }
}
