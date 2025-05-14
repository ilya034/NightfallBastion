using System;
using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D.UI;

namespace NightfallBastion.UI
{
    public class MainMenuView : BaseView
    {
        public event Action OnStartButtonClicked;
        public event Action OnOptionsButtonClicked;
        public event Action OnExitButtonClicked;

        public MainMenuView()
            : base() { }

        public override void BuildUI()
        {
            var mainMenuPanel = new VerticalStackPanel { Spacing = 10 };

            var titleLabel = new Label
            {
                Text = "Nightfall Bastion",
                TextColor = Color.Violet,
                HorizontalAlignment = HorizontalAlignment.Center,
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
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };
            startButton.Click += (_, __) => OnStartButtonClicked?.Invoke();
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
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };
            optionsButton.Click += (_, __) => OnOptionsButtonClicked?.Invoke();
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
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };
            exitButton.Click += (_, __) => OnExitButtonClicked?.Invoke();
            mainMenuPanel.Widgets.Add(exitButton);

            mainMenuPanel.HorizontalAlignment = HorizontalAlignment.Center;
            mainMenuPanel.VerticalAlignment = VerticalAlignment.Center;

            RootElement.Widgets.Add(mainMenuPanel);
        }
    }
}
