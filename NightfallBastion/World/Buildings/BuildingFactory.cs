using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.World.Buildings
{
    public static class BuildingFactory
    {
        public static EmptySpace CreateEmptySpace(NightfallBastionGame game)
        {
            var sourceRect = new Rectangle(
                game.CoreSettings.DefaultTileTextureX,
                game.CoreSettings.DefaultTileTextureY,
                game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileSize
            );
            return new EmptySpace(sourceRect);
        }

        public static WeakWall CreateWeakWall(NightfallBastionGame game)
        {
            var sourceRect = new Rectangle(
                game.CoreSettings.DefaultTileTextureX + game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileTextureY,
                game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileSize
            );
            return new WeakWall(sourceRect);
        }

        public static StandardWall CreateStandardWall(NightfallBastionGame game)
        {
            var sourceRect = new Rectangle(
                game.CoreSettings.DefaultTileTextureX + game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileTextureY,
                game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileSize
            );
            return new StandardWall(sourceRect);
        }

        public static MediumWall CreateMediumWall(NightfallBastionGame game)
        {
            var sourceRect = new Rectangle(
                game.CoreSettings.DefaultTileTextureX + game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileTextureY,
                game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileSize
            );
            return new MediumWall(sourceRect);
        }

        public static StrongWall CreateStrongWall(NightfallBastionGame game)
        {
            var sourceRect = new Rectangle(
                game.CoreSettings.DefaultTileTextureX + game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileTextureY,
                game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileSize
            );
            return new StrongWall(sourceRect);
        }

        public static EnemySpawn CreateEnemySpawn(NightfallBastionGame game)
        {
            var sourceRect = new Rectangle(
                game.CoreSettings.DefaultTileTextureX + 2 * game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileTextureY,
                game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileSize
            );
            return new EnemySpawn(sourceRect);
        }

        public static PlayerCore CreatePlayerCore(NightfallBastionGame game)
        {
            var sourceRect = new Rectangle(
                game.CoreSettings.DefaultTileTextureX + 3 * game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileTextureY,
                game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileSize
            );
            return new PlayerCore(sourceRect);
        }

        public static Turret CreateTurret(
            NightfallBastionGame game,
            int damage = 10,
            int range = 3,
            float attackSpeed = 1.0f
        )
        {
            var sourceRect = new Rectangle(
                game.CoreSettings.DefaultTileTextureX + 4 * game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileTextureY,
                game.CoreSettings.DefaultTileSize,
                game.CoreSettings.DefaultTileSize
            );
            return new Turret(sourceRect, damage, range, attackSpeed);
        }
    }
}
