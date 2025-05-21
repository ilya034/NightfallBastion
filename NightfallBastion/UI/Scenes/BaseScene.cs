using System;
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

        public static TScene Create<TScene, TPresenter, TView>(NightfallBastionGame game)
            where TScene : BaseScene
            where TPresenter : BasePresenter
            where TView : BaseView
        {
            var view = Activator.CreateInstance(typeof(TView), game) as TView;
            var presenter = Activator.CreateInstance(typeof(TPresenter), game, view) as TPresenter;
            var scene = Activator.CreateInstance(typeof(TScene), game, presenter, view) as TScene;

            return scene;
        }
    }
}
