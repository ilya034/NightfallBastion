using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NightfallBastion.UI.Input
{
    public struct CameraInputData(Vector2 movementDirection, float zoomDelta, float movementSpeed)
    {
        public Vector2 MovementDirection { get; set; } = movementDirection;
        public float ZoomDelta { get; set; } = zoomDelta;
        public float MovementSpeed { get; set; } = movementSpeed;
    }

    public struct GameplayInputData(
        Vector2? mouseClickPosition,
        bool leftMouseClicked,
        bool rightMouseClicked,
        Vector2 mousePosition,
        bool buildingSelectionPressed
    )
    {
        public Vector2? MouseClickPosition { get; set; } = mouseClickPosition;
        public bool LeftMouseClicked { get; set; } = leftMouseClicked;
        public bool RightMouseClicked { get; set; } = rightMouseClicked;
        public Vector2 MousePosition { get; set; } = mousePosition;
        public bool BuildingSelectionPressed { get; set; } = buildingSelectionPressed;
    }

    public class InputHandler
    {
        private MouseState _previousMouseState;
        private KeyboardState _previousKeyboardState;

        public CameraInputData HandleCameraInput(
            KeyboardState keyboardState,
            MouseState mouseState,
            GameTime gameTime,
            float cameraSpeed
        )
        {
            var movementDirection = Vector2.Zero;
            float zoomDelta = 0f;
            float speed = cameraSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                movementDirection.Y -= 1f;
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                movementDirection.Y += 1f;
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                movementDirection.X -= 1f;
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                movementDirection.X += 1f;

            if (movementDirection.LengthSquared() > 1f)
                movementDirection.Normalize();

            if (keyboardState.IsKeyDown(Keys.OemPlus) || keyboardState.IsKeyDown(Keys.E))
                zoomDelta += 0.01f;
            if (keyboardState.IsKeyDown(Keys.OemMinus) || keyboardState.IsKeyDown(Keys.Q))
                zoomDelta -= 0.01f;

            int scrollDelta = mouseState.ScrollWheelValue - _previousMouseState.ScrollWheelValue;
            if (scrollDelta != 0)
                zoomDelta += scrollDelta * 0.001f;

            return new CameraInputData(movementDirection, zoomDelta, speed);
        }

        public GameplayInputData HandleGameplayInput(
            KeyboardState keyboardState,
            MouseState mouseState,
            GameTime gameTime
        )
        {
            Vector2? mouseClickPosition = null;
            bool leftMouseClicked = false;
            bool rightMouseClicked = false;
            bool buildingSelectionPressed = false;

            if (
                mouseState.LeftButton == ButtonState.Pressed
                && _previousMouseState.LeftButton == ButtonState.Released
            )
            {
                leftMouseClicked = true;
                mouseClickPosition = new Vector2(mouseState.X, mouseState.Y);
            }

            if (
                mouseState.RightButton == ButtonState.Pressed
                && _previousMouseState.RightButton == ButtonState.Released
            )
            {
                rightMouseClicked = true;
                if (!mouseClickPosition.HasValue)
                    mouseClickPosition = new Vector2(mouseState.X, mouseState.Y);
            }

            if (keyboardState.IsKeyDown(Keys.B) && !_previousKeyboardState.IsKeyDown(Keys.B))
                buildingSelectionPressed = true;

            var mousePosition = new Vector2(mouseState.X, mouseState.Y);

            return new GameplayInputData(
                mouseClickPosition,
                leftMouseClicked,
                rightMouseClicked,
                mousePosition,
                buildingSelectionPressed
            );
        }

        public void UpdatePreviousStates(KeyboardState keyboardState, MouseState mouseState)
        {
            _previousKeyboardState = keyboardState;
            _previousMouseState = mouseState;
        }

        public bool IsKeyPressed(Keys key, KeyboardState currentState) =>
            currentState.IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key);

        public bool IsKeyReleased(Keys key, KeyboardState currentState) =>
            !currentState.IsKeyDown(key) && _previousKeyboardState.IsKeyDown(key);
    }
}
