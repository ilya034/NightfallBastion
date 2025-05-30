using Microsoft.Xna.Framework;

namespace NightfallBastion.UI.ViewModels
{
    public class CameraViewModel
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Zoom { get; set; }
        public Vector2 Position
        {
            get => new(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public CameraViewModel(float x, float y, float zoom = 1.0f)
        {
            X = x;
            Y = y;
            Zoom = zoom;
        }

        public CameraViewModel()
        {
            Zoom = 1.0f;
        }

        public Matrix GetTransformMatrix(int screenWidth, int screenHeight)
        {
            return Matrix.CreateTranslation(-X, -Y, 0)
                * Matrix.CreateScale(Zoom, Zoom, 1)
                * Matrix.CreateTranslation(screenWidth * 0.5f, screenHeight * 0.5f, 0);
        }
    }
}
