using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class SettingsScene(
        NightfallBastionGame game,
        SettingsPresenter presenter,
        SettingsView view
    ) : BaseScene(game, presenter, view)
    {
        public new SettingsPresenter Presenter => (SettingsPresenter)base.Presenter;
        public new SettingsView View => (SettingsView)base.View;
    }
}
