using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class GameWorldPresenter(NightfallBastionGame game, GameWorldView view)
        : BasePresenter(game)
    {
        private readonly GameWorldView _view = view;
    }
}
