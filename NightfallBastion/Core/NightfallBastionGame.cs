using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NightfallBastion.Core
{
    public class NightfallBastionGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private GameView _gameView;
        private GameState _gameState;

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
            _gameView = new GameView(this);
            _gameState = new GameState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _gameView.LoadContent();
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            _gameState.Update(gameTime);
            _gameView.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);

            _gameView.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}