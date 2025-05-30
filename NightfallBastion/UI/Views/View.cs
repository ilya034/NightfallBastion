using Myra.Graphics2D.UI;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public abstract class View(NightfallBastionGame game)
    {
        protected readonly NightfallBastionGame _game = game;
        public Panel RootElement { get; } = new Panel();

        public virtual void LoadContent() { }

        public virtual void BuildUI() { }

        public virtual void UnloadContent() { }
    }
}
