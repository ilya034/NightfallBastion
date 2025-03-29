using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;

namespace NightfallBastion.UI.States
{
    public abstract class UIState
    {
        protected Game Game { get; }
        protected Desktop Desktop { get; set; }

        protected UIState(Game game)
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