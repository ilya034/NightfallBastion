using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NightfallBastion.World
{
    public class CameraSystem(GameWorld world) : System(world)
    {
        public override void Update(GameTime gameTime)
        {
            var camera = _world.ECSManager.GetComponent<CameraComponent>(_world.CameraEntity);
            if (camera == null)
                return;

            var game = _world.Game;
            var keyboard = game.CurrentKeyboardState;
            float speed = game.Settings.CameraSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            var pos = camera.Position;
            if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
                pos.Y += speed;
            if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
                pos.Y -= speed;
            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
                pos.X += speed;
            if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
                pos.X -= speed;
            camera.Position = pos;

            if (keyboard.IsKeyDown(Keys.OemPlus) || keyboard.IsKeyDown(Keys.E))
                camera.Zoom += 0.01f;
            if (keyboard.IsKeyDown(Keys.OemMinus) || keyboard.IsKeyDown(Keys.Q))
                camera.Zoom = Math.Max(0.1f, camera.Zoom - 0.01f);
        }
    }
}
