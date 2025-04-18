using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;

namespace NightfallBastion.UI.Screens
{
    public class OptionsScreen : Screen
    {
        public OptionsScreen(UI ui) : base(ui)
        {
        }

        protected override void BuildUI()
        {
            var grid = new Grid
            {
                RowSpacing = 10,
                ColumnSpacing = 10
            };

            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));

            var titleLabel = new Label
            {
                Text = "Options",
                TextColor = Color.White,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            Grid.SetColumnSpan(titleLabel, 2);
            grid.Widgets.Add(titleLabel);

            var volumeLabel = new Label
            {
                Text = "Volume:",
            };
            Grid.SetRow(volumeLabel, 1);
            Grid.SetColumn(volumeLabel, 0);

            grid.Widgets.Add(volumeLabel);

            var volumeSlider = new HorizontalSlider
            {
                Width = 200,
                Value = 100
            };
            Grid.SetRow(volumeSlider, 1);
            Grid.SetColumn(volumeSlider, 1);

            grid.Widgets.Add(volumeSlider);

            var fullscreenLabel = new Label
            {
                Text = "Fullscreen:",
            };
            Grid.SetRow(fullscreenLabel, 2);
            Grid.SetColumn(fullscreenLabel, 0);

            grid.Widgets.Add(fullscreenLabel);

            var fullscreenCheckButton = new CheckButton();
            Grid.SetRow(fullscreenCheckButton, 2);
            Grid.SetColumn(fullscreenCheckButton, 1);
            grid.Widgets.Add(fullscreenCheckButton);

            var backButton = new Button
            {
                Width = 200,
                Height = 35,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = new Label
                {
                    Text = "Back",
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };
            Grid.SetRow(backButton, 3);
            Grid.SetColumnSpan(backButton, 2);

            backButton.Click += (s, a) => UI.MainMenuScreen.Show();
            grid.Widgets.Add(backButton);

            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;

            RootElement.Widgets.Add(grid);
        }
    }
} 