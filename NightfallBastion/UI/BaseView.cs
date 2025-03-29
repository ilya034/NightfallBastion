using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;

namespace NightfallBastion.UI
{
    public abstract class BaseView : IView
    {
        public Desktop Desktop { get; private set; }
        protected Game Game { get; }

        protected BaseView(Game game)
        {
            Game = game;
            Desktop = new Desktop();
        }

        public virtual void Initialize() { }
        public virtual void LoadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Desktop.Render();
        }
    }
} 