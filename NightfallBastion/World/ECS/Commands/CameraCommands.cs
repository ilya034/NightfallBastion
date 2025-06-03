using Microsoft.Xna.Framework;

namespace NightfallBastion.World.ECS.Commands
{
    public class MoveCameraCommand : ICommand
    {
        private readonly Vector2 _direction;
        private readonly float _speed;

        public MoveCameraCommand(Vector2 direction, float speed)
        {
            _direction = direction;
            _speed = speed;
        }

        public void Execute(ECSManager ecsManager)
        {
            // Камера не является ECS сущностью, поэтому эта команда будет обрабатываться
            // специальной системой или напрямую в GameWorld
            // Пока оставляем пустой, реализуем позже
        }
    }

    public class ZoomCameraCommand : ICommand
    {
        private readonly float _zoomDelta;

        public ZoomCameraCommand(float zoomDelta)
        {
            _zoomDelta = zoomDelta;
        }

        public void Execute(ECSManager ecsManager)
        {
            // Аналогично команде движения камеры
            // Будет обрабатываться специальной системой
        }
    }
}