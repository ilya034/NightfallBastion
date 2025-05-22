using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class MainMenuPresenter(NightfallBastionGame game, MainMenuView view)
        : BasePresenter(game)
    {
        private readonly MainMenuView _view = view;

        public override void RegisterEvents()
        {
            base.RegisterEvents();

            _view.OnStartButtonClicked += OnPlayButtonClicked;
            _view.OnOptionsButtonClicked += OnOptionsButtonClicked;
            _view.OnExitButtonClicked += OnExitButtonClicked;
        }

        public void OnPlayButtonClicked() =>
            _game.SceneManager.ChangeScene(_game.SceneManager.GameWorldScene);

        public void OnOptionsButtonClicked() =>
            _game.SceneManager.ChangeScene(_game.SceneManager.OptionsScene);

        public void OnExitButtonClicked() => _game.Exit();
    }
}
