using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class SettingsPresenter(NightfallBastionGame game, SettingsView view)
        : BasePresenter(game)
    {
        private readonly SettingsView _view = view;

        public override void RegisterEvents()
        {
            base.RegisterEvents();
        }

        private void OnBackButtonClicked() => _game.SceneManager.HideCurrentScene();
    }
}
