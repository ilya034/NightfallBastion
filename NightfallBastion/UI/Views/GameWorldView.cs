using Microsoft.Xna.Framework.Graphics;
using NightfallBastion.Core;
using NightfallBastion.World;

namespace NightfallBastion.UI
{
    public class GameWorldView(NightfallBastionGame game) : BaseView(game)
    {
        private GameWorld? _gameWorld;

        public void SetGameWorld(GameWorld gameWorld)
        {
            _gameWorld = gameWorld;
        }

        public void DrawGameWorld(SpriteBatch spriteBatch)
        {
            _gameWorld?.Draw(spriteBatch);
        }

        public override void BuildUI() { }
    }
}
