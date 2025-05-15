using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public abstract class BasePresenter
    {
        protected readonly NightfallBastionGame _game;

        public BasePresenter(NightfallBastionGame game)
        {
            _game = game;
        }

        public virtual void RegisterEvents() { }
    }
}
