using System;
using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World.ECS.Commands
{
    public class MoveEntityCommand : ICommand
    {
        private readonly Entity _entity;
        private readonly Vector2 _targetPosition;

        public MoveEntityCommand(Entity entity, Vector2 targetPosition)
        {
            _entity = entity;
            _targetPosition = targetPosition;
        }

        public void Execute(ECSManager ecsManager)
        {
            MovementTargetComp? movementTarget = ecsManager.GetComponent<MovementTargetComp>(_entity);
            if (movementTarget.HasValue)
            {
                var updated = movementTarget.Value;
                updated.target = _targetPosition;
                ecsManager.AddComponent(_entity, updated);
            }
            else
            {
                // Создаем новый компонент цели движения
                ecsManager.AddComponent(_entity, new MovementTargetComp { target = _targetPosition });
            }
        }
    }

    public class SelectEntityCommand : ICommand
    {
        private readonly Vector2 _worldPosition;

        public SelectEntityCommand(Vector2 worldPosition)
        {
            _worldPosition = worldPosition;
        }

        public void Execute(ECSManager ecsManager)
        {
            // Логика выбора сущности по позиции
            var entities = ecsManager.GetEntitiesWithComponents<PositionComp>();
            foreach (var entity in entities)
            {
                PositionComp? position = ecsManager.GetComponent<PositionComp>(entity);
                if (position.HasValue && Vector2.Distance(position.Value.position, _worldPosition) < 32f)
                {
                    // Можно добавить компонент выбора или отправить событие
                    // Пока просто выводим в консоль
                    Console.WriteLine($"Selected entity {entity.Id} at position {position.Value.position}");
                    break;
                }
            }
        }
    }

    public class SpawnEnemyCommand : ICommand
    {
        private readonly Vector2 _position;
        private readonly EnemyType _enemyType;

        public SpawnEnemyCommand(Vector2 position, EnemyType enemyType)
        {
            _position = position;
            _enemyType = enemyType;
        }

        public void Execute(ECSManager ecsManager)
        {
            var entity = ecsManager.CreateEntity();
            
            ecsManager.AddComponent(entity, new PositionComp { position = _position });
            ecsManager.AddComponent(entity, new EnemyComp { type = _enemyType });
            ecsManager.AddComponent(entity, new HealthComp { maxHealth = 100f, currentHealth = 100f });
            ecsManager.AddComponent(entity, new MovementComp { speed = 50f, isMoving = false });
            ecsManager.AddComponent(entity, new TeamComp { team = Team.Enemy });
        }
    }

    public class DamageEntityCommand : ICommand
    {
        private readonly Entity _entity;
        private readonly float _damage;

        public DamageEntityCommand(Entity entity, float damage)
        {
            _entity = entity;
            _damage = damage;
        }

        public void Execute(ECSManager ecsManager)
        {
            HealthComp? health = ecsManager.GetComponent<HealthComp>(_entity);
            if (health.HasValue)
            {
                var updated = health.Value;
                updated.currentHealth = Math.Max(0, updated.currentHealth - _damage);
                ecsManager.AddComponent(_entity, updated);
            }
        }
    }
}