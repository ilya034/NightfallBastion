using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.World
{
    public class GameWorld(NightfallBastionGame game)
    {
        private readonly NightfallBastionGame _game = game;
        private readonly ECSManager _ecsManager = new();

        public void Update(GameTime gameTime) => _ecsManager.Update(gameTime);
    }
}
