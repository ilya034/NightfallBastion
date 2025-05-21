using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public abstract class BasePresenter(NightfallBastionGame game)
    {
        protected readonly NightfallBastionGame _game = game;

        public virtual void RegisterEvents() { }
    }
}
