using Myra.Graphics2D.UI;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public abstract class BaseView(NightfallBastionGame game)
    {
        public NightfallBastionGame Game { get; } = game;
        public Panel RootElement { get; } = new Panel();

        public virtual void BuildUI() { }
    }
}
