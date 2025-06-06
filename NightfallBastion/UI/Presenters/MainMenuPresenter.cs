using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class MainMenuPresenter(NightfallBastionGame game, MainMenuView view) : Presenter(game)
    {
        private readonly MainMenuView _view = view;

        public override void RegisterEvents()
        {
            base.RegisterEvents();

            _view.OnStartButtonClicked += OnPlayButtonClicked;
            _view.OnSettingsButtonClicked += OnOptionsButtonClicked;
            _view.OnExitButtonClicked += OnExitButtonClicked;
        }

        public void OnPlayButtonClicked() => _game.SceneManager.ShowScene(Scenes.GameWorld);

        public void OnOptionsButtonClicked() => _game.SceneManager.ShowScene(Scenes.Settings);

        public void OnExitButtonClicked() => _game.Exit();
    }
}
