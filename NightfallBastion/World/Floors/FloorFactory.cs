using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.World.Floors
{
    public static class FloorFactory
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

        public static Floor CreateStandartFloor(NightfallBastionGame game) =>
            new(true, 1.0f, true, false, 0);
    }
}
