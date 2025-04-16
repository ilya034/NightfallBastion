using System;
using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Properties;

namespace NightfallBastion.UI
{
    public class UIManager
    {
        private readonly Desktop _desktop;

        public UIManager(Game game)
        {
            MyraEnvironment.Game = game;
            _desktop = new Desktop();
        }

        public void LoadContent()
        {
            var menuPanel = new VerticalStackPanel()
            {
                Spacing = 10,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            var titleLabel = new Label()
            {
                Text = "Nightfall Bastion",
                TextColor = Color.Violet,
                Padding = new Thickness(0, 5),
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            var buttonPlay = new Button()
            {
                Width = 200,
                Padding = new Thickness(0, 5),
                Content = new Label()
                {
                    Text = "Play",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };

            var buttonExit = new Button()
            {
                Width = 200,
                Padding = new Thickness(0, 5),
                Content = new Label()
                {
                    Text = "Exit",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };

            menuPanel.Widgets.Add(titleLabel);
            menuPanel.Widgets.Add(buttonPlay);
            menuPanel.Widgets.Add(buttonExit);

            _desktop.Root = menuPanel;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            _desktop.Render();
        }
    }
}
