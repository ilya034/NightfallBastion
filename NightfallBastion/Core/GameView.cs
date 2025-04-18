using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NightfallBastion.Core
{
    public class GameView
    {
        private readonly SpriteBatch _spriteBatch;
        public UI.UI UI { get; }

        public GameView(Game game)
        {
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
            UI = new UI.UI(game);
        }

        public void LoadContent()
        {
            UI.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            UI.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.End();

            UI.Draw(gameTime);
        }
    }
}
