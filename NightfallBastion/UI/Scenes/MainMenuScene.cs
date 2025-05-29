using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class MainMenuScene(
        NightfallBastionGame game,
        MainMenuPresenter presenter,
        MainMenuView view
    ) : Scene(game, presenter, view)
    {
        public new MainMenuPresenter Presenter => (MainMenuPresenter)base.Presenter;
        public new MainMenuView View => (MainMenuView)base.View;
    }
}
