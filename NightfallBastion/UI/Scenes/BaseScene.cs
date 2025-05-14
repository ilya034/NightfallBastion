using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public abstract class BaseScene
    {
        public NightfallBastionGame Game { get; }
        public BasePresenter Presenter { get; }
        public BaseView View { get; }

        public BaseScene(NightfallBastionGame game, BasePresenter presenter, BaseView view)
        {
            Game = game;
            Presenter = presenter;
            View = view;

            View.BuildUI();
            Presenter.RegisterEvents();
        }

        public void Show() => Game.SceneManager.ShowScene(this);

        public void Hide() => Game.SceneManager.HideScene(this);
    }
}
