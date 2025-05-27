using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D.UI;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public enum Scenes
    {
        MainMenu,
        GameWorld,
        Settings
    }

    public class SceneManager
    {
        public NightfallBastionGame Game { get; }
        private readonly Dictionary<Scenes, BaseScene> _scenes;
        private readonly Stack<BaseScene> _shownScenes;
        private readonly Desktop _desktop;

        public SceneManager(NightfallBastionGame game)
        {
            Game = game;
            MyraEnvironment.Game = game;

            _scenes = new Dictionary<Scenes, BaseScene>();
            _shownScenes = new Stack<BaseScene>();
            _desktop = new Desktop();
        }

        public void LoadContent()
        {
            _scenes[Scenes.MainMenu] = BaseScene.Create<MainMenuScene, MainMenuPresenter, MainMenuView>(Game);
            _scenes[Scenes.GameWorld] = BaseScene.Create<GameWorldScene, GameWorldPresenter, GameWorldView>(Game);
            _scenes[Scenes.Settings] = BaseScene.Create<SettingsScene, SettingsPresenter, SettingsView>(Game);
            ShowScene(Scenes.MainMenu);
        }

        public void ShowScene(Scenes key)
        {
            var scene = _scenes[key];
            if (!_shownScenes.Contains(scene))
                _shownScenes.Push(scene);
        }

        public void HideScene(Scenes key)
        {
            var scene = _scenes[key];
            if (_shownScenes.Count > 0 && _shownScenes.Peek() == scene)
                _shownScenes.Pop();
        }

        public void ChangeScene(Scenes key)
        {
            if (_shownScenes.Count > 0)
                _shownScenes.Pop();
            ShowScene(key);
        }

        public void HideCurrentScene()
        {
            if (_shownScenes.Count > 1)
                _shownScenes.Pop();
        }

        public void Update(GameTime gameTime)
        {
            if (_shownScenes.Count > 0)
                _shownScenes.Peek().Update(gameTime);
        }

        public void Draw()
        {
            foreach (var scene in _shownScenes.Reverse())
            {
                if (scene is GameWorldScene gameWorldScene)
                    gameWorldScene.View.Draw();

                _desktop.Root = scene.View.RootElement;
                _desktop.Render();
            }
        }
    }
}
