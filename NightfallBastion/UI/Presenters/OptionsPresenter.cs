using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class OptionsPresenter : BasePresenter
    {
        private readonly OptionsView _view;

        public OptionsPresenter(NightfallBastionGame game, OptionsView view)
            : base(game)
        {
            _view = view;
        }

        public override void RegisterEvents()
        {
            base.RegisterEvents();
        }

        private void OnBackButtonClicked() => _game.SceneManager.HideCurrentScene();
    }
}
