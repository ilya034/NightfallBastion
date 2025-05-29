using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class GameWorldPresenter(NightfallBastionGame game, GameWorldView view) : Presenter(game)
    {
        private readonly GameWorldView _view = view;
    }
}
