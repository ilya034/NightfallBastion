using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class PauseScene(NightfallBastionGame game, PausePresenter presenter, PauseView view)
        : Scene(game, presenter, view)
    {
        public new PausePresenter Presenter => (PausePresenter)base.Presenter;
        public new PauseView View => (PauseView)base.View;
    }
}
