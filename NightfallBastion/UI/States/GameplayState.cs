using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NightfallBastion.UI.Gameplay;

namespace NightfallBastion.UI.States
{
    public class GameplayState : UIState
    {
        private readonly GameplayPresenter _presenter;
        private readonly GameplayView _view;
        private readonly UIManager _ui;

        public GameplayState(Game game, UIManager ui) : base(game)
        {
            _ui = ui;
            
            var model = new GameplayModel();
            _view = new GameplayView(game);
            _presenter = new GameplayPresenter(model, _view, ui);
            
            _presenter.GoToMenu += () => _ui.ChangeState<MainMenuState>();
        }

        public override void Initialize()
        {
            _presenter.Initialize();
        }

        public override void LoadContent()
        {
            _presenter.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _presenter.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _view.Draw(gameTime, spriteBatch);
        }
    }
} 