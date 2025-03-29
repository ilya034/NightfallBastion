using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NightfallBastion.UI.MainMenu;

namespace NightfallBastion.UI.States
{
    public class MainMenuState : UIState
    {
        private readonly MainMenuPresenter _presenter;
        private readonly MainMenuView _view;
        private readonly UIManager _ui;

        public MainMenuState(Game game, UIManager ui) : base(game)
        {
            _ui = ui;
            
            var model = new MainMenuModel();
            _view = new MainMenuView(game);
            _presenter = new MainMenuPresenter(model, _view, ui);
            
            _presenter.OnNewGameSelected += () => _ui.ChangeState<GameplayState>();
            _presenter.OnExitSelected += () => game.Exit();
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