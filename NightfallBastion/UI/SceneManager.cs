using System;
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
        Settings,
    }

    public class SceneManager
    {
        public NightfallBastionGame Game { get; }
        private readonly Dictionary<Scenes, Scene> _scenes;
        private readonly Stack<Scene> _shownScenes;
        private readonly Desktop _desktop;

        public SceneManager(NightfallBastionGame game)
        {
            Game = game;
            MyraEnvironment.Game = game;

            _scenes = new Dictionary<Scenes, Scene>();
            _shownScenes = new Stack<Scene>();
            _desktop = new Desktop();
        }

        public void LoadContent()
        {
            _scenes[Scenes.MainMenu] = Scene.Create<MainMenuScene, MainMenuPresenter, MainMenuView>(Game);
            _scenes[Scenes.GameWorld] = Scene.Create<GameWorldScene, GameWorldPresenter, GameWorldView>(Game);
            _scenes[Scenes.Settings] = Scene.Create<SettingsScene, SettingsPresenter, SettingsView>(Game);

            ShowScene(Scenes.MainMenu);
        }

        public void ShowScene(Scenes key)
        {
            var scene = _scenes[key];
            if (!_shownScenes.Contains(scene))
                _shownScenes.Push(scene);
        }

        public void HideCurrentScene()
        {
            if (_shownScenes.Count > 1)
                _shownScenes.Pop();
        }

        public Scene? GetScene(Scenes key)
        {
            if (_scenes.TryGetValue(key, out var scene))
                return scene;
            return null;
        }

        public void Update(GameTime gameTime)
        {
            if (_shownScenes.Count > 0)
                _shownScenes.Peek().Update(gameTime);
        }

        public void Draw()
        {
            var currentScene = _shownScenes.Count > 0 ? _shownScenes.Peek() : null;

            if (currentScene is GameWorldScene gameWorldScene)
                gameWorldScene.View.Draw();

            _desktop.Root = currentScene?.View.RootElement;
            _desktop.Render();
        }
    }
}
