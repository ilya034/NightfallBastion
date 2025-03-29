using Microsoft.Xna.Framework;
using System;

namespace NightfallBastion.UI.MainMenu
{
    public class MainMenuPresenter : IPresenter
    {
        private readonly MainMenuModel _model;
        private readonly MainMenuView _view;
        private readonly UIManager _ui;

        public event Action OnNewGameSelected;
        public event Action OnLoadGameSelected;
        public event Action OnOptionsSelected;
        public event Action OnExitSelected;

        public MainMenuPresenter(MainMenuModel model, MainMenuView view, UIManager ui)
        {
            _model = model;
            _view = view;
            _ui = ui;

            _view.MenuItemSelected += HandleMenuItemSelected;
        }

        public void Initialize()
        {
            _model.Initialize();
            _view.Initialize();

            _view.SetupMenu(_model.GameTitle, _model.MenuItems);
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

        // TODO: refactor
        private void HandleMenuItemSelected(int menuIndex)
        {
            switch (menuIndex)
            {
                case 0:
                    OnNewGameSelected?.Invoke();
                    break;
                case 1:
                    OnLoadGameSelected?.Invoke();
                    break;
                case 2:
                    OnOptionsSelected?.Invoke();
                    break;
                case 3:
                    OnExitSelected?.Invoke();
                    break;
            }
        }
    }
}