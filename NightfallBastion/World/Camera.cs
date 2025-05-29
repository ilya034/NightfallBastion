using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NightfallBastion.World
{
    public class Camera
    {
        public Vector2 Position { get; set; } = Vector2.Zero;

        public float Zoom { get; set; } = 1f;

        public float Rotation { get; set; } = 0f;

        public int ViewportWidth { get; set; }

        public int ViewportHeight { get; set; }

        public Camera(int viewportWidth, int viewportHeight)
        {
            if (viewportWidth <= 0)
                throw new ArgumentException();
            if (viewportHeight <= 0)
                throw new ArgumentException();

            ViewportWidth = viewportWidth;
            ViewportHeight = viewportHeight;
        }

        public Camera(GraphicsDevice graphicsDevice)
        {
            ArgumentNullException.ThrowIfNull(graphicsDevice);

            ViewportWidth = graphicsDevice.Viewport.Width;
            ViewportHeight = graphicsDevice.Viewport.Height;
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            Vector2 centeredScreen =
                screenPosition - new Vector2(ViewportWidth / 2f, ViewportHeight / 2f);

            Vector2 scaledPosition = centeredScreen / Zoom;

            float cos = (float)Math.Cos(-Rotation);
            float sin = (float)Math.Sin(-Rotation);
            Vector2 rotatedPosition = new Vector2(
                scaledPosition.X * cos - scaledPosition.Y * sin,
                scaledPosition.X * sin + scaledPosition.Y * cos
            );

            return rotatedPosition + Position;
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            Vector2 relativePosition = worldPosition - Position;

            float cos = (float)Math.Cos(Rotation);
            float sin = (float)Math.Sin(Rotation);
            Vector2 rotatedPosition = new Vector2(
                relativePosition.X * cos - relativePosition.Y * sin,
                relativePosition.X * sin + relativePosition.Y * cos
            );

            Vector2 scaledPosition = rotatedPosition * Zoom;

            return scaledPosition + new Vector2(ViewportWidth / 2f, ViewportHeight / 2f);
        }

        public Matrix GetTransformMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0))
                * Matrix.CreateRotationZ(Rotation)
                * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1))
                * Matrix.CreateTranslation(new Vector3(ViewportWidth / 2f, ViewportHeight / 2f, 0));
        }

        public RectangleF GetViewBounds()
        {
            Vector2 topLeft = ScreenToWorld(Vector2.Zero);
            Vector2 topRight = ScreenToWorld(new Vector2(ViewportWidth, 0));
            Vector2 bottomLeft = ScreenToWorld(new Vector2(0, ViewportHeight));
            Vector2 bottomRight = ScreenToWorld(new Vector2(ViewportWidth, ViewportHeight));

            float minX = Math.Min(
                Math.Min(topLeft.X, topRight.X),
                Math.Min(bottomLeft.X, bottomRight.X)
            );
            float maxX = Math.Max(
                Math.Max(topLeft.X, topRight.X),
                Math.Max(bottomLeft.X, bottomRight.X)
            );
            float minY = Math.Min(
                Math.Min(topLeft.Y, topRight.Y),
                Math.Min(bottomLeft.Y, bottomRight.Y)
            );
            float maxY = Math.Max(
                Math.Max(topLeft.Y, topRight.Y),
                Math.Max(bottomLeft.Y, bottomRight.Y)
            );

            return new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }

        public bool IsInView(Vector2 worldPosition)
        {
            RectangleF viewBounds = GetViewBounds();
            return viewBounds.Contains(worldPosition.X, worldPosition.Y);
        }

        public bool IsInView(RectangleF worldBounds)
        {
            RectangleF viewBounds = GetViewBounds();
            return viewBounds.Intersects(worldBounds);
        }

        public void Move(Vector2 delta) => Position += delta;

        public void SetPosition(Vector2 position) => Position = position;

        public void ScaleZoom(float factor, float minZoom = 0.1f, float maxZoom = 10.0f)
        {
            if (factor <= 0)
                throw new ArgumentException();

            Zoom = MathHelper.Clamp(Zoom * factor, minZoom, maxZoom);
        }

        public void SetZoom(float zoom, float minZoom = 0.1f, float maxZoom = 10.0f)
        {
            if (zoom <= 0)
                throw new ArgumentException();

            Zoom = MathHelper.Clamp(zoom, minZoom, maxZoom);
        }

        public void Rotate(float deltaRotation)
        {
            Rotation += deltaRotation;
            Rotation = Rotation % (2 * MathHelper.Pi);
            if (Rotation < 0)
                Rotation += 2 * MathHelper.Pi;
        }

        public void SetRotation(float rotation)
        {
            Rotation = rotation % (2 * MathHelper.Pi);
            if (Rotation < 0)
                Rotation += 2 * MathHelper.Pi;
        }
    }

    public struct RectangleF(float x, float y, float width, float height)
    {
        public float X { get; set; } = x;
        public float Y { get; set; } = y;
        public float Width { get; set; } = width;
        public float Height { get; set; } = height;

        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;

        public bool Contains(float x, float y) =>
            x >= Left && x <= Right && y >= Top && y <= Bottom;

        public bool Intersects(RectangleF other)
        {
            return !(
                Right < other.Left || Left > other.Right || Bottom < other.Top || Top > other.Bottom
            );
        }
    }
}
