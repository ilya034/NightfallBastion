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

        public static WeakWall CreateWeakWall(NightfallBastionGame game) =>
            new(CreateSourceRect(game, 1));

        public static StandardWall CreateStandardWall(NightfallBastionGame game) =>
            new(CreateSourceRect(game, 1));

        public static MediumWall CreateMediumWall(NightfallBastionGame game) =>
            new(CreateSourceRect(game, 1));

        public static StrongWall CreateStrongWall(NightfallBastionGame game) =>
            new(CreateSourceRect(game, 1));

        public static EnemySpawn CreateEnemySpawn(NightfallBastionGame game) =>
            new(CreateSourceRect(game, 2));

        public static PlayerCore CreatePlayerCore(NightfallBastionGame game) =>
            new(CreateSourceRect(game, 3));

        public static Turret CreateTurret(
            NightfallBastionGame game,
            int damage = 10,
            int range = 3,
            float attackSpeed = 1.0f
        ) => new(CreateSourceRect(game, 4), damage, range, attackSpeed);
    }
}
