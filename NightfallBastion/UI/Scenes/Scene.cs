using System;
using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public abstract class Scene
    {
        public NightfallBastionGame Game { get; }
        public Presenter Presenter { get; }
        public View View { get; }

        public Scene(NightfallBastionGame game, Presenter presenter, View view)
        {
            Game = game;
            Presenter = presenter;
            View = view;

            View.LoadContent();
            View.BuildUI();
            Presenter.RegisterEvents();
        }

        public virtual void Update(GameTime gameTime)
        {
            Presenter?.Update(gameTime);
        }

        public virtual void Dispose()
        {
            Presenter?.Dispose();
            View?.UnloadContent();
        }

        public static TScene Create<TScene, TPresenter, TView>(NightfallBastionGame game)
            where TScene : Scene
            where TPresenter : Presenter
            where TView : View
        {
            var view = Activator.CreateInstance(typeof(TView), game) as TView;
            var presenter = Activator.CreateInstance(typeof(TPresenter), game, view) as TPresenter;
            var scene = Activator.CreateInstance(typeof(TScene), game, presenter, view) as TScene;

            return scene;
        }
    }
}
