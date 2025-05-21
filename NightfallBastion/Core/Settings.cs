namespace NightfallBastion.Core
{
    public class Settings
    {
        public int DefaultTileSize = 32;
        public int DefaultMapWidth = 100;
        public int DefaultMapHeight = 100;
        public int DefaultTileTextureX = 0;
        public int DefaultTileTextureY = 0;
        public int DefaultScreenWidth = 1280;
        public int DefaultScreenHeight = 720;

        public int MainMenuPanelSpacing = 10;
        public int DefaultButtonWidth = 200;
        public int DefaultButtonHeight = 35;

        public string GameTitle = "Nightfall Bastion";

        // public string StartGameButtonText = "Start Game";
        // public string OptionsButtonText = "Options";
        // public string ExitButtonText = "Exit";

        public string TilesetAssetName = "tileset";
        public string ContentRootDirectoryName = "Content";

        public static void Load() { }

        public static void Save() { }
    }
}
