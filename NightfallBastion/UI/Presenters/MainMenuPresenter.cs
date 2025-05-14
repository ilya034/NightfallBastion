using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class MainMenuPresenter : BasePresenter
    {
        public MainMenuPresenter(NightfallBastionGame game, MainMenuView view)
            : base(game, view) { }

        public override void RegisterEvents()
        {
            base.RegisterEvents();

            var mainMenuView = (MainMenuView)_view;
            mainMenuView.OnStartButtonClicked += OnPlayButtonClicked;
            mainMenuView.OnOptionsButtonClicked += OnOptionsButtonClicked;
            mainMenuView.OnExitButtonClicked += OnExitButtonClicked;
        }

        public void OnPlayButtonClicked() =>
            _game.SceneManager.ChangeScene(_game.SceneManager.GameWorldScene);

        public void OnOptionsButtonClicked() { }

        public void OnExitButtonClicked() => _game.Exit();
    }
}
