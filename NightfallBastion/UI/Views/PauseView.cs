using System;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class PauseView(NightfallBastionGame game) : View(game)
    {
        public event Action? OnResumeButtonClicked;
        public event Action? OnSettingsButtonClicked;
        public event Action? OnMenuButtonClicked;

        public override void BuildUI()
        {
            var mainMenuPanel = new VerticalStackPanel { Spacing = _game.CoreSettings.MenuPanelSpacing };

            var titleLabel = new Label
            {
                Text = "Pause",
                TextColor = Color.Violet,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            mainMenuPanel.Widgets.Add(titleLabel);

            var ResumeButton = new Button
            {
                Width = _game.CoreSettings.DefaultButtonWidth,
                Height = _game.CoreSettings.DefaultButtonHeight,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = new Label
                {
                    Text = "Resume",
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };
            ResumeButton.Click += (_, __) => OnResumeButtonClicked?.Invoke();
            mainMenuPanel.Widgets.Add(ResumeButton);

            var settingsButton = new Button
            {
                Width = _game.CoreSettings.DefaultButtonWidth,
                Height = _game.CoreSettings.DefaultButtonHeight,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = new Label
                {
                    Text = "Options",
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };
            settingsButton.Click += (_, __) => OnSettingsButtonClicked?.Invoke();
            mainMenuPanel.Widgets.Add(settingsButton);

            var exitButton = new Button
            {
                Width = _game.CoreSettings.DefaultButtonWidth,
                Height = _game.CoreSettings.DefaultButtonHeight,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = new Label
                {
                    Text = "Go to Main Menu",
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };
            exitButton.Click += (_, __) => OnMenuButtonClicked?.Invoke();
            mainMenuPanel.Widgets.Add(exitButton);

            mainMenuPanel.HorizontalAlignment = HorizontalAlignment.Center;
            mainMenuPanel.VerticalAlignment = VerticalAlignment.Center;

            RootElement.Widgets.Add(mainMenuPanel);
        }
    }
}
