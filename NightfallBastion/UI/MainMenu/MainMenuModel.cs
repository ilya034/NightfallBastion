using Microsoft.Xna.Framework;

namespace NightfallBastion.UI.MainMenu
{
    public class MainMenuModel : IModel
    {
        public string GameTitle { get; private set; }
        public string[] MenuItems { get; private set; }

        public MainMenuModel()
        {
            GameTitle = "Nightfall Bastion";
            MenuItems = new[]
            {
                "New Game",
                "Load Game",
                "Options",
                "Exit"
            };
        }

        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
        }
    }
} 