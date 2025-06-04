using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class GameWorldPresenter(NightfallBastionGame game, GameWorldView view) : Presenter(game)
    {
        private readonly GameWorldView _view = view;

        public override void Update(GameTime gameTime)
        {
            _game.GameWorld.Update(gameTime);
            _view.UpdateRenderData(_game.GameWorld.GetRenderData());
            base.Update(gameTime);
        }
    }
}
