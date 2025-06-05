using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World.ECS.Systems
{
    public class TargetSystem(GameWorld world) : System(world)
    {
        public override void Update(GameTime gameTime)
        {
            var entities = _world.ECSManager.GetEntitiesWithComponents<
                PositionComp,
                MovementComp,
                MovementTargetComp
            >();

            foreach (var entity in entities)
            {
                var position = _world.ECSManager.GetComponent<PositionComp>(entity);
                var tilePosition = _world.ECSManager.GetComponent<TilePositionComp>(entity);
                var movement = _world.ECSManager.GetComponent<MovementComp>(entity);
                var target = _world.ECSManager.GetComponent<MovementTargetComp>(entity);
            }
        }
    }
}
