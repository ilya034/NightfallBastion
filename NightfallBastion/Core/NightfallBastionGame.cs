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

        public KeyboardState CurrentKeyboardState { get; private set; }
        public SceneManager SceneManager { get; private set; }
        public GameWorld GameWorld { get; private set; }

        public NightfallBastionGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
            };

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SceneManager = new SceneManager(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SceneManager.LoadContent();
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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);
            SceneManager.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}
