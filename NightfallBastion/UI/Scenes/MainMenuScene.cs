using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class MainMenuScene : BaseScene
    {
        public new MainMenuPresenter Presenter => (MainMenuPresenter)base.Presenter;
        public new MainMenuView View => (MainMenuView)base.View;

        public MainMenuScene(
            NightfallBastionGame game,
            MainMenuPresenter presenter,
            MainMenuView view
        )
            : base(game, presenter, view) { }
    }
}
