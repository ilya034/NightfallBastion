using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using System;

namespace NightfallBastion.UI.MainMenu
{
    public class MainMenuView : BaseView
    {
        private Panel _mainPanel;
        private VerticalStackPanel _menuPanel;
        private Label _titleLabel;
        private Button[] _menuButtons;

        public event Action<int> MenuItemSelected;

        public MainMenuView(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void SetupMenu(string title, string[] menuItems)
        {
            _mainPanel = new Panel();

            _titleLabel = new Label
            {
                Text = title,
                TextColor = Color.White,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            _menuPanel = new VerticalStackPanel
            {
                Spacing = 10,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            _menuButtons = new Button[menuItems.Length];
            for (int i = 0; i < menuItems.Length; i++)
            {
                int index = i;
                var button = new Button
                {
                    Width = 200,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Content = new Label { 
                        Text = menuItems[i],
                        HorizontalAlignment = HorizontalAlignment.Center
                    }
                };

                button.Click += (s, e) => MenuItemSelected?.Invoke(index);

                _menuButtons[i] = button;
                _menuPanel.Widgets.Add(button);
            }

            _mainPanel.Widgets.Add(_titleLabel);
            _mainPanel.Widgets.Add(_menuPanel);
            Desktop.Root = _mainPanel;
        }
    }
} 