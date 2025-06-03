using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.World.Buildings
{
    public static class BuildingFactory
    {
        private static Rectangle CreateSourceRect(
            NightfallBastionGame game,
            int offsetX = 0,
            int offsetY = 0
        )
        {
            return new Rectangle(
                game.CoreSettings.DefaultTileTextureX + offsetX * game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileTextureY + offsetY * game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileSize
            );
        }

        public static EmptySpace CreateEmptySpace(NightfallBastionGame game) =>
            new(CreateSourceRect(game));

        public static Wall CreateWeakWall(NightfallBastionGame game) =>
            new(CreateSourceRect(game, 1), 50);

        public static Wall CreateStandardWall(NightfallBastionGame game) =>
            new(CreateSourceRect(game, 1), 100);

        public static Wall CreateMediumWall(NightfallBastionGame game) =>
            new(CreateSourceRect(game, 1), 200);

        public static Wall CreateStrongWall(NightfallBastionGame game) =>
            new(CreateSourceRect(game, 1), 400);

        public static EnemySpawn CreateEnemySpawn(NightfallBastionGame game) =>
            new(CreateSourceRect(game, 2));

        public static PlayerCore CreatePlayerCore(NightfallBastionGame game) =>
            new(CreateSourceRect(game, 3));

        public static Turret CreateTurret(
            NightfallBastionGame game,
            int maxHealth = 100,
            int damage = 10,
            int range = 3,
            float attackSpeed = 1.0f
        ) => new(CreateSourceRect(game, 4), maxHealth, damage, range, attackSpeed);

        public static Mine CreateMine(
            NightfallBastionGame game,
            int maxHealth,
            int explosionRadius = 2
        ) => new(CreateSourceRect(game, 5), maxHealth, explosionRadius);
    }
}
