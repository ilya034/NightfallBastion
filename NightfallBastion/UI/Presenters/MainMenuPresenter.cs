using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class MainMenuPresenter : BasePresenter
    {
        private readonly MainMenuView _view;

        public MainMenuPresenter(NightfallBastionGame game, MainMenuView view)
            : base(game)
        {
            _view = view;
        }

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
