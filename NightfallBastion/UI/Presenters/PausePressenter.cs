using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class PausePresenter(NightfallBastionGame game, PauseView view) : Presenter(game)
    {
        private readonly PauseView _view = view;

        public override void RegisterEvents()
        {
            base.RegisterEvents();

            _view.OnResumeButtonClicked += OnResumeButtonClicked;
            _view.OnSettingsButtonClicked += OnSettingsButtonClicked;
            _view.OnMenuButtonClicked += OnMenuButtonClicked;
        }

        public void OnResumeButtonClicked() => _game.SceneManager.HideCurrentScene();

        public void OnSettingsButtonClicked() => _game.SceneManager.ShowScene(Scenes.Settings);

        public void OnMenuButtonClicked() => _game.SceneManager.ShowScene(Scenes.MainMenu);

        public override void Update(GameTime gameTime)
        {
            if (_game.InputHandler.IsKeyPressed(Keys.Escape))
            {
                OnMenuButtonClicked();
                return;
            }

            base.Update(gameTime);
        }
    }
}
