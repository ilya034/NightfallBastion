using Microsoft.Xna.Framework;

namespace NightfallBastion.World
{
    public abstract class System(GameWorld world)
    {
        protected readonly GameWorld _world = world;

        public abstract void Update(GameTime gameTime);
    }
}
