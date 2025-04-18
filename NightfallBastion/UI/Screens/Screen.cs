using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;

namespace NightfallBastion.UI.Screens
{
    public abstract class Screen
    {
        public Panel RootElement { get; private set; }
        public UI UI { get; private set; }

        public Screen(UI ui)
        {
            UI = ui;
            RootElement = new Panel();

            BuildUI();
        }

        protected abstract void BuildUI();

        public virtual void Update(GameTime gameTime) {}

        public void Show()
        {
            UI.Show(this);
        }
    }
} 