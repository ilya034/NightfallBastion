using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NightfallBastion.UI;

namespace NightfallBastion.Core
{
    public class GameView
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly UIManager _ui;

        public GameView(Game game)
        {
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
            _ui = new UIManager(game);
        }

        public void LoadContent()
        {
            _ui.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            _ui.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _ui.Draw(gameTime);
        }
    }
}
