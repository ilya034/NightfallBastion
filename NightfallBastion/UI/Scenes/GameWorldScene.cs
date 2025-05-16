using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class GameWorldScene : BaseScene
    {
        public new GameWorldPresenter Presenter => (GameWorldPresenter)base.Presenter;
        public new GameWorldView View => (GameWorldView)base.View;

        public GameWorldScene(
            NightfallBastionGame game,
            GameWorldPresenter presenter,
            GameWorldView view
        )
            : base(game, presenter, view) { }
    }
}
