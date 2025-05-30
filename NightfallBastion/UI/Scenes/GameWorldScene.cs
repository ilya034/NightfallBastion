using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class GameWorldScene(
        NightfallBastionGame game,
        GameWorldPresenter presenter,
        GameWorldView view
    ) : Scene(game, presenter, view)
    {
        public new GameWorldPresenter Presenter => (GameWorldPresenter)base.Presenter;
        public new GameWorldView View => (GameWorldView)base.View;

        public override void Update(GameTime gameTime)
        {
            Presenter.Update(gameTime);
            Game.GameWorld.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Dispose()
        {
            Game.GameWorld.Dispose();
            base.Dispose();
        }
    }
}
