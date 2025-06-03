using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World.ECS.Systems
{
    public class WeaponSystem(GameWorld gameWorld) : System(gameWorld)
    {
        public override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var entities = _world.ECSManager.GetEntitiesWithComponents<
                PositionComp,
                WeaponComp,
                TeamComp
            >();

            foreach (var entity in entities)
            {
                var position = _world.ECSManager.GetComponent<PositionComp>(entity);
                var weapon = _world.ECSManager.GetComponent<WeaponComp>(entity);
                var team = _world.ECSManager.GetComponent<TeamComp>(entity);

                if (_world.ECSManager.GetComponent<HealthComp>(entity).currentHealth <= 0)
                    continue;
            }
        }
    }
}
