using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using Myra.Graphics2D.UI;
using NightfallBastion.UI.Screens;

namespace NightfallBastion.UI
{
    public class UI
    {
        private readonly Desktop _desktop;
        private readonly Game _game;

        public MainMenuScreen MainMenuScreen { get; private set; }
        public GameplayScreen GameplayScreen { get; private set; }
        public PauseScreen PauseScreen { get; private set; }
        public OptionsScreen OptionsScreen { get; private set; }

        public Screen CurrentScreen { get; private set; }

        public static SpriteFont DefaultFont { get; private set; }

        public UI(Game game)
        {
            _game = game;
            MyraEnvironment.Game = game;

            _desktop = new Desktop();
        }

        public void LoadContent()
        {
            LoadFonts();

            MainMenuScreen = new MainMenuScreen(this);
            GameplayScreen = new GameplayScreen(this);
            PauseScreen = new PauseScreen(this);
            OptionsScreen = new OptionsScreen(this);

            MainMenuScreen.Show();
        }

        public void LoadFonts()
        {
            //DefaultFont = _game.Content.Load<SpriteFont>("Fonts/DefaultFont");
        }

        public void Show(Screen screen)
        {
            _desktop.Root = screen.RootElement;
            CurrentScreen = screen;
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen?.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _desktop.Render();
        }
    }
}
