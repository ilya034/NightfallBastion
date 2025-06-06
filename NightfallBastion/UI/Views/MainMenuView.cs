using System;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class MainMenuView(NightfallBastionGame game) : View(game)
    {
        public event Action? OnStartButtonClicked;
        public event Action? OnSettingsButtonClicked;
        public event Action? OnExitButtonClicked;

        public override void BuildUI()
        {
            var mainMenuPanel = new VerticalStackPanel { Spacing = _game.CoreSettings.MenuPanelSpacing };

            var titleLabel = new Label
            {
                Text = _game.CoreSettings.GameTitle,
                TextColor = Color.Violet,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            mainMenuPanel.Widgets.Add(titleLabel);

            var startButton = new Button
            {
                Width = _game.CoreSettings.DefaultButtonWidth,
                Height = _game.CoreSettings.DefaultButtonHeight,
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

            var SettingsButton = new Button
            {
                Width = _game.CoreSettings.DefaultButtonWidth,
                Height = _game.CoreSettings.DefaultButtonHeight,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = new Label
                {
                    Text = "Settings",
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };
            SettingsButton.Click += (_, __) => OnSettingsButtonClicked?.Invoke();
            mainMenuPanel.Widgets.Add(SettingsButton);

            var exitButton = new Button
            {
                Width = _game.CoreSettings.DefaultButtonWidth,
                Height = _game.CoreSettings.DefaultButtonHeight,
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
