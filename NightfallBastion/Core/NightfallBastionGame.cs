using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using NightfallBastion.UI;
using NightfallBastion.UI.States;

namespace NightfallBastion.Core
{
    public class NightfallBastionGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private UIManager _ui;

        public NightfallBastionGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            InitializeGraphics();
            InitializeUI();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _ui.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            _ui.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.DimGray);

            _spriteBatch.Begin();
            _ui.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
        }

        private void InitializeGraphics()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
        }

        private void InitializeUI()
        {
            MyraEnvironment.Game = this;

            _ui = new UIManager(this);
            _ui.RegisterState(new MainMenuState(this, _ui));
            _ui.RegisterState(new GameplayState(this, _ui));
            _ui.ChangeState<MainMenuState>();

            _ui.Initialize();
        }
    }
}