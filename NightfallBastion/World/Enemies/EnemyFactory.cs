using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.World.Enemies;

public static class EnemyFactory
{
    public static Entity CreateEnemy(
        ECSManager ecsManager,
        Vector2 spawnPosition,
        Vector2 targetPosition,
        GameplaySettings settings,
        GameWorld gameWorld
    )
    {
        var enemy = ecsManager.CreateEntity();

        ecsManager.AddComponent(enemy, new PositionComponent(spawnPosition));

        var direction = Vector2.Normalize(targetPosition - spawnPosition);
        ecsManager.AddComponent(
            enemy,
            new MovementComponent(direction, settings.EnemySpeed) { Target = targetPosition }
        );
        ecsManager.AddComponent(
            enemy,
            new EnemyComponent(settings.EnemyHealth, settings.EnemyDamage)
        );

        return enemy;
    }
}
