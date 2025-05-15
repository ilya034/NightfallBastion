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

        public static MainMenuScene Create(NightfallBastionGame game)
        {
            var view = new MainMenuView();
            var presenter = new MainMenuPresenter(game, view);
            return new MainMenuScene(game, presenter, view);
        }
    }
}
