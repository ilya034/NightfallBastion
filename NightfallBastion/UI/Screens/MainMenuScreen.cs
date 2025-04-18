using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D.UI;

namespace NightfallBastion.UI.Screens
{
    public class MainMenuScreen : Screen
    {
        public MainMenuScreen(UI ui) : base(ui)
        {
        }

        protected override void BuildUI()
        {
            var mainMenuPanel = new VerticalStackPanel
            {
                Spacing = 10
            };

            var titleLabel = new Label
            {
                Text = "Nightfall Bastion",
                TextColor = Color.Violet,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            mainMenuPanel.Widgets.Add(titleLabel);

            var startButton = new Button
            {
                Width = 200,
                Height = 35,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = new Label
                {
                    Text = "Start Game",
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };
            startButton.Click += (sender, args) => UI.GameplayScreen.Show();
            mainMenuPanel.Widgets.Add(startButton);

            var optionsButton = new Button
            {
                Width = 200,
                Height = 35,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = new Label
                {
                    Text = "Options",
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };
            optionsButton.Click += (s, a) => UI.OptionsScreen.Show();
            mainMenuPanel.Widgets.Add(optionsButton);

            var exitButton = new Button
            {
                Width = 200,
                Height = 35,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = new Label
                {
                    Text = "Exit",
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };
            exitButton.Click += (s, a) => MyraEnvironment.Game.Exit();
            mainMenuPanel.Widgets.Add(exitButton);

            mainMenuPanel.HorizontalAlignment = HorizontalAlignment.Center;
            mainMenuPanel.VerticalAlignment = VerticalAlignment.Center;

            RootElement.Widgets.Add(mainMenuPanel);
        }
    }
} 