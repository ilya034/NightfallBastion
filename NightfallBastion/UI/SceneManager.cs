using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using Myra.Graphics2D.UI;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class SceneManager
    {
        public MainMenuScene MainMenuScene { get; private set; }
        public GameWorldScene GameWorldScene { get; private set; }
        public OptionsScene OptionsScene { get; private set; }

        public NightfallBastionGame Game { get; }
        private readonly HashSet<BaseScene> _shownScenes;
        private readonly Desktop _desktop;

        public SceneManager(NightfallBastionGame game)
        {
            Game = game;
            MyraEnvironment.Game = game;

            _shownScenes = new HashSet<BaseScene>();
            _desktop = new Desktop();
        }

        public void LoadContent()
        {
            MainMenuScene = MainMenuScene.Create(Game);
            GameWorldScene = GameWorldScene.Create(Game);
            OptionsScene = OptionsScene.Create(Game);

            MainMenuScene.Show();
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var scene in _shownScenes)
            {
                _desktop.Root = scene.View.RootElement;
                _desktop.Render();
            }
        }

        public void ShowScene(BaseScene scene) => _shownScenes.Add(scene);

        public void HideScene(BaseScene scene) => _shownScenes.Remove(scene);

        public void ChangeScene(BaseScene scene)
        {
            _shownScenes.Last()?.Hide();
            ShowScene(scene);
        }

        public void HideCurrentScene()
        {
            if (_shownScenes.Count > 1)
                _shownScenes.Last().Hide();
        }
    }
}
