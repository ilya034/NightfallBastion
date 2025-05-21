using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class GameWorldPresenter : BasePresenter
    {
        private readonly GameWorldView _view;

        public GameWorldPresenter(NightfallBastionGame game, GameWorldView view)
            : base(game)
        {
            _view = view;
            _view.SetGameWorld(game.GameWorld);
        }
    }
}
