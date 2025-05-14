using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public abstract class BasePresenter
    {
        protected readonly NightfallBastionGame _game;
        protected readonly BaseView _view;

        public BasePresenter(NightfallBastionGame game, BaseView view)
        {
            _game = game;
            _view = view;
        }

        public virtual void RegisterEvents() { }
    }
}
