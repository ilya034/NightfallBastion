using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;

namespace NightfallBastion.UI.Screens
{
    public class PauseScreen : Screen
    {
        private KeyboardState _previousKeyboardState;

        public PauseScreen(UI ui) : base(ui)
        {
        }

        protected override void BuildUI()
        {
            var pausePanel = new VerticalStackPanel
            {
                Spacing = 10
            };

            var titleLabel = new Label
            {
                Text = "Pause",
                TextColor = Color.White,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            pausePanel.Widgets.Add(titleLabel);

            var resumeButton = new Button
            {
                Width = 200,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = new Label
                {
                    Text = "Resume",
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                },
            };
            resumeButton.Click += (s, a) => UI.GameplayScreen.Show();
            pausePanel.Widgets.Add(resumeButton);

            var optionsButton = new Button
            {
                Width = 200,
                Height = 30,
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
            pausePanel.Widgets.Add(optionsButton);

            var mainMenuButton = new Button
            {
                Width = 200,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = new Label
                {
                    Text = "Main Menu",
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };
            mainMenuButton.Click += (s, a) => UI.MainMenuScreen.Show();
            pausePanel.Widgets.Add(mainMenuButton);

            pausePanel.HorizontalAlignment = HorizontalAlignment.Center;
            pausePanel.VerticalAlignment = VerticalAlignment.Center;

            RootElement.Widgets.Add(pausePanel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyUp(Keys.Escape) && _previousKeyboardState.IsKeyUp(Keys.Escape))
                UI.GameplayScreen.Show();

            _previousKeyboardState = currentKeyboardState;
        }
    }
}