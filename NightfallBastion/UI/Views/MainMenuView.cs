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
            var mainMenuPanel = new VerticalStackPanel
            {
                Spacing = _game.CoreSettings.MenuPanelSpacing,
            };

            var titleLabel = new Label
            {
                Text = _game.CoreSettings.GameTitle,
                TextColor = Color.Violet,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            mainMenuPanel.Widgets.Add(titleLabel);

            mainMenuPanel.Widgets.Add(
                CreateMenuButton("Start Game", () => OnStartButtonClicked?.Invoke())
            );
            mainMenuPanel.Widgets.Add(
                CreateMenuButton("Settings", () => OnSettingsButtonClicked?.Invoke())
            );
            mainMenuPanel.Widgets.Add(
                CreateMenuButton("Exit", () => OnExitButtonClicked?.Invoke())
            );

            mainMenuPanel.HorizontalAlignment = HorizontalAlignment.Center;
            mainMenuPanel.VerticalAlignment = VerticalAlignment.Center;

            RootElement.Widgets.Add(mainMenuPanel);
        }
    }
}
