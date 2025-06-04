using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NightfallBastion.UI
{
    public struct CameraInputData(Vector2 movementDirection, float zoomDelta)
    {
        public Vector2 MovementDirection { get; set; } = movementDirection;
        public float ZoomDelta { get; set; } = zoomDelta;
    }

    public struct GameplayInputData(
        Vector2? mouseClickPosition,
        bool leftMouseClicked,
        bool rightMouseClicked,
        Vector2 mousePosition
    )
    {
        public Vector2? MouseClickPosition { get; set; } = mouseClickPosition;
        public bool LeftMouseClicked { get; set; } = leftMouseClicked;
        public bool RightMouseClicked { get; set; } = rightMouseClicked;
        public Vector2 MousePosition { get; set; } = mousePosition;
    }

    public class InputHandler
    {
        private MouseState _currentMouseState;
        private KeyboardState _currentKeyboardState;
        private MouseState _previousMouseState;
        private KeyboardState _previousKeyboardState;

        public CameraInputData HandleCameraInput()
        {
            var movementDirection = Vector2.Zero;
            float zoomDelta = 0f;

            if (_currentKeyboardState.IsKeyDown(Keys.W) || _currentKeyboardState.IsKeyDown(Keys.Up))
                movementDirection.Y -= 1f;
            if (
                _currentKeyboardState.IsKeyDown(Keys.S)
                || _currentKeyboardState.IsKeyDown(Keys.Down)
            )
                movementDirection.Y += 1f;
            if (
                _currentKeyboardState.IsKeyDown(Keys.A)
                || _currentKeyboardState.IsKeyDown(Keys.Left)
            )
                movementDirection.X -= 1f;
            if (
                _currentKeyboardState.IsKeyDown(Keys.D)
                || _currentKeyboardState.IsKeyDown(Keys.Right)
            )
                movementDirection.X += 1f;

            if (movementDirection.LengthSquared() > 1f)
                movementDirection.Normalize();

            int scrollDelta =
                _currentMouseState.ScrollWheelValue - _previousMouseState.ScrollWheelValue;
            if (scrollDelta != 0)
                zoomDelta += scrollDelta * 0.001f;

            return new CameraInputData(movementDirection, zoomDelta);
        }

        public GameplayInputData HandleGameplayInput()
        {
            Vector2? mouseClickPosition = null;
            bool leftMouseClicked = false;
            bool rightMouseClicked = false;

            if (
                _currentMouseState.LeftButton == ButtonState.Pressed
                && _previousMouseState.LeftButton == ButtonState.Released
            )
            {
                leftMouseClicked = true;
                mouseClickPosition = new Vector2(_currentMouseState.X, _currentMouseState.Y);
            }

            if (
                _currentMouseState.RightButton == ButtonState.Pressed
                && _previousMouseState.RightButton == ButtonState.Released
            )
            {
                rightMouseClicked = true;
                if (!mouseClickPosition.HasValue)
                    mouseClickPosition = new Vector2(_currentMouseState.X, _currentMouseState.Y);
            }

            var mousePosition = new Vector2(_currentMouseState.X, _currentMouseState.Y);

            return new GameplayInputData(
                mouseClickPosition,
                leftMouseClicked,
                rightMouseClicked,
                mousePosition
            );
        }

        public void Update(KeyboardState keyboardState, MouseState mouseState)
        {
            _previousKeyboardState = _currentKeyboardState;
            _previousMouseState = _currentMouseState;

            _currentKeyboardState = keyboardState;
            _currentMouseState = mouseState;
        }

        public bool IsKeyDown(Keys key) => _currentKeyboardState.IsKeyDown(key);

        public bool IsKeyPressed(Keys key) =>
            _previousKeyboardState.IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key);

        public bool IsKeyReleased(Keys key) =>
            !_previousKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyDown(key);
    }
}
