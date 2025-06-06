using System;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class MainMenuPresenter(NightfallBastionGame game, MainMenuView view) : Presenter(game)
    {
        private readonly MainMenuView _view = view;

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            RegisterButtonEvent(
                _view,
                nameof(_view.OnStartButtonClicked),
                (Action)OnPlayButtonClicked
            );
            RegisterButtonEvent(
                _view,
                nameof(_view.OnSettingsButtonClicked),
                (Action)OnOptionsButtonClicked
            );
            RegisterButtonEvent(
                _view,
                nameof(_view.OnExitButtonClicked),
                (Action)OnExitButtonClicked
            );
        }

        public void OnPlayButtonClicked() => _game.SceneManager.ShowScene(Scenes.GameWorld);

        public void OnOptionsButtonClicked() => _game.SceneManager.ShowScene(Scenes.Settings);

        public void OnExitButtonClicked() => _game.Exit();
    }
}
