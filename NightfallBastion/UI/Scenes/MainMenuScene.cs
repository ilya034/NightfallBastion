using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class MainMenuScene : BaseScene
    {
        public MainMenuScene(NightfallBastionGame game, BasePresenter presenter, BaseView view)
            : base(game, presenter, view) { }

        public static MainMenuScene Create(NightfallBastionGame game)
        {
            var view = new MainMenuView();
            var presenter = new MainMenuPresenter(game, view);
            return new MainMenuScene(game, presenter, view);
        }
    }
}
