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
            var mainMenuPanel = new VerticalStackPanel
            {
                Spacing = _game.CoreSettings.MenuPanelSpacing,
            };

            var titleLabel = new Label
            {
                Text = "Pause",
                TextColor = Color.Violet,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            mainMenuPanel.Widgets.Add(titleLabel);

            mainMenuPanel.Widgets.Add(
                CreateMenuButton("Resume", () => OnResumeButtonClicked?.Invoke())
            );
            mainMenuPanel.Widgets.Add(
                CreateMenuButton("Options", () => OnSettingsButtonClicked?.Invoke())
            );
            mainMenuPanel.Widgets.Add(
                CreateMenuButton("Go to Main Menu", () => OnMenuButtonClicked?.Invoke())
            );

            mainMenuPanel.HorizontalAlignment = HorizontalAlignment.Center;
            mainMenuPanel.VerticalAlignment = VerticalAlignment.Center;

            RootElement.Widgets.Add(mainMenuPanel);
        }
    }
}
