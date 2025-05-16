using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class OptionsScene : BaseScene
    {
        public new OptionsPresenter Presenter => (OptionsPresenter)base.Presenter;
        public new OptionsView View => (OptionsView)base.View;

        public OptionsScene(NightfallBastionGame game, OptionsPresenter presenter, OptionsView view)
            : base(game, presenter, view) { }
    }
}
