using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;

namespace NightfallBastion.UI.Screens
{
    public class GameplayScreen : Screen
    {
        private KeyboardState _previousKeyboardState;

        public GameplayScreen(UI ui) : base(ui)
        {
        }

        protected override void BuildUI()
        {
            var grid = new Grid
            {
                RowSpacing = 10,
                ColumnSpacing = 10
            };

            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion());
            grid.ColumnsProportions.Add(new Proportion());
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));

            var healthBar = new HorizontalProgressBar
            {
                Width = 200,
                Height = 20,
                Value = 100,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            Grid.SetColumn(healthBar, 0);
            
            grid.Widgets.Add(healthBar);

            var scoreLabel = new Label
            {
                Text = "Score: 0",
                HorizontalAlignment = HorizontalAlignment.Right
            };
            Grid.SetColumn(scoreLabel, 1);

            grid.Widgets.Add(scoreLabel);

            RootElement.Widgets.Add(grid);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Escape) && _previousKeyboardState.IsKeyUp(Keys.Escape))
                UI.PauseScreen.Show();

            _previousKeyboardState = currentKeyboardState;
        }
    }
} 