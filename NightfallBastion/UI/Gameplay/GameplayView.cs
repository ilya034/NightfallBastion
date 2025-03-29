using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;
using System;

namespace NightfallBastion.UI.Gameplay
{
    public class GameplayView : BaseView
    {
        private Panel _mainPanel;
        private Button _menuButton;

        public event Action OnMenuRequested;

        public GameplayView(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            _mainPanel = new Panel();

            _menuButton = new Button
            {
                Width = 80,
                Height = 30,
                Left = Game.GraphicsDevice.Viewport.Width - 90,
                Top = 10,
                Content = new Label
                {
                    Text = "Menu",
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };

            _menuButton.Click += (s, e) => OnMenuRequested?.Invoke();

            _mainPanel.Widgets.Add(_menuButton);
            Desktop.Root = _mainPanel;
        }
    }
}