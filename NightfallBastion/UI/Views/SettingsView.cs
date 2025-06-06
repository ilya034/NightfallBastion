using System;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class SettingsView(NightfallBastionGame game) : View(game)
    {
        public event Action? OnBackButtonClicked;

        public override void BuildUI()
        {
            var mainMenuPanel = new VerticalStackPanel
            {
                Spacing = _game.CoreSettings.MenuPanelSpacing,
            };

            var titleLabel = new Label
            {
                Text = "Settings",
                TextColor = Color.Violet,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            mainMenuPanel.Widgets.Add(titleLabel);

            mainMenuPanel.Widgets.Add(
                CreateMenuButton("Back", () => OnBackButtonClicked?.Invoke())
            );

            mainMenuPanel.HorizontalAlignment = HorizontalAlignment.Center;
            mainMenuPanel.VerticalAlignment = VerticalAlignment.Center;

            RootElement.Widgets.Add(mainMenuPanel);
        }
    }
}
