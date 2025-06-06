using System;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class SettingsPresenter(NightfallBastionGame game, SettingsView view) : Presenter(game)
    {
        private readonly SettingsView _view = view;

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            RegisterButtonEvent(
                _view,
                nameof(_view.OnBackButtonClicked),
                (Action)OnBackButtonClicked
            );
        }

        private void OnBackButtonClicked() => _game.SceneManager.HideCurrentScene();
    }
}
