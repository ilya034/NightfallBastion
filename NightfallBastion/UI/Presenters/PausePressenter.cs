using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class PausePresenter(NightfallBastionGame game, PauseView view) : Presenter(game)
    {
        private readonly PauseView _view = view;

        public override void RegisterEvents()
        {
            base.RegisterEvents();
        }

        private void OnBackButtonClicked() => _game.SceneManager.HideCurrentScene();
    }
}
