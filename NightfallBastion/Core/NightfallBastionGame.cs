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
        public SpriteBatch SpriteBatch { get; private set; }
        public CoreSettings CoreSettings { get; private set; }
        public GameplaySettings GameplaySettings { get; private set; }
        public SceneManager SceneManager { get; private set; }
        public GameWorld GameWorld { get; private set; }
        public KeyboardState CurrentKeyboardState { get; private set; }

        public NightfallBastionGame()
        {
            CoreSettings = new CoreSettings();

            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = CoreSettings.DefaultScreenWidth,
                PreferredBackBufferHeight = CoreSettings.DefaultScreenHeight,
            };
            
            Content.RootDirectory = CoreSettings.ContentRootDirectoryName;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            GameplaySettings = new GameplaySettings();
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
            GraphicsDevice.Clear(Color.DimGray);
            SceneManager.Draw();
            base.Draw(gameTime);
        }
    }
}
