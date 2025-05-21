using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NightfallBastion.UI;
using NightfallBastion.World;

namespace NightfallBastion.Core
{
    public class NightfallBastionGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Settings Settings { get; private set; }
        public KeyboardState CurrentKeyboardState { get; private set; }
        public SceneManager SceneManager { get; private set; }
        public GameWorld GameWorld { get; private set; }

        public NightfallBastionGame()
        {
            Settings = new Settings();

            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Settings.DefaultScreenWidth,
                PreferredBackBufferHeight = Settings.DefaultScreenHeight,
            };

            Content.RootDirectory = Settings.ContentRootDirectoryName;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SceneManager = new SceneManager(this);
            GameWorld = new GameWorld(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SceneManager.LoadContent();
            GameWorld.LoadContent();
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            CurrentKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            if (CurrentKeyboardState.IsKeyDown(Keys.Escape))
                SceneManager.HideCurrentScene();

            SceneManager.Update(gameTime);
            GameWorld.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            GraphicsDevice.Clear(Color.DimGray);
            SceneManager.Draw(_spriteBatch);
            base.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}
