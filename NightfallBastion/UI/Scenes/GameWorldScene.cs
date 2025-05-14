using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class GameWorldScene : BaseScene
    {
        public GameWorldScene(NightfallBastionGame game, BasePresenter presenter, BaseView view)
            : base(game, presenter, view) { }

        public static GameWorldScene Create(NightfallBastionGame game)
        {
            var view = new GameWorldView();
            var presenter = new GameWorldPresenter(game, view);
            return new GameWorldScene(game, presenter, view);
        }
    }
}
