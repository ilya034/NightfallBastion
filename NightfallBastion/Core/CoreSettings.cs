namespace NightfallBastion.Core
{
    public class CoreSettings
    {
        public int DefaultTileSize { get; } = 32;
        public int DefaultMapWidth { get; } = 100;
        public int DefaultMapHeight { get; } = 100;
        public int DefaultTileTextureX { get; } = 0;
        public int DefaultTileTextureY { get; } = 0;
        public int DefaultScreenWidth { get; } = 1280;
        public int DefaultScreenHeight { get; } = 720;
        public string GameTitle { get; } = "Nightfall Bastion";
        public string TilesetAssetName { get; } = "tileset";
        public string EnemyAssetName { get; } = "enemy";
        public string ContentRootDirectoryName { get; } = "Content";
        public int MainMenuPanelSpacing { get; } = 10;
        public int DefaultButtonWidth { get; } = 200;
        public int DefaultButtonHeight { get; } = 35;
    }
}
