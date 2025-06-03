using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World.ECS.Systems
{
    public class PhysicsSystem(GameWorld world) : System(world)
    {
        public override void Update(GameTime gameTime)
        {
            var entities = _world.ECSManager.GetEntitiesWithComponents<
                PositionComp,
                TilePositionComp,
                MovementComp
            >();
        }
    }
}