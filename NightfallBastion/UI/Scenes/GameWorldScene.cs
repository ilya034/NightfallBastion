using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class GameWorldScene(
        NightfallBastionGame game,
        GameWorldPresenter presenter,
        GameWorldView view
    ) : BaseScene(game, presenter, view)
    {
        public new GameWorldPresenter Presenter => (GameWorldPresenter)base.Presenter;
        public new GameWorldView View => (GameWorldView)base.View;
    }
}
