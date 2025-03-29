using Microsoft.Xna.Framework;
using System;

namespace NightfallBastion.UI.Gameplay
{
    public class GameplayPresenter : IPresenter
    {
        private readonly GameplayModel _model;
        private readonly GameplayView _view;
        private readonly UIManager _ui;

        public event Action GoToMenu;

        public GameplayPresenter(GameplayModel model, GameplayView view, UIManager ui)
        {
            _model = model;
            _view = view;
            _ui = ui;

            _view.OnMenuRequested += HandleMenuRequested;
        }

        public void Initialize()
        {
            _model.Initialize();
            _view.Initialize();
        }

        public void LoadContent()
        {
            _view.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            _model.Update(gameTime);
            _view.Update(gameTime);
        }

        private void HandleMenuRequested()
        {
            GoToMenu?.Invoke();
            // _stateMachine.ChangeState<PauseMenuState>();
        }
    }
}