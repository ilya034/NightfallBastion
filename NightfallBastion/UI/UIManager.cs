using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NightfallBastion.UI.States;
using System;
using System.Collections.Generic;

namespace NightfallBastion.UI
{
    public class UIManager
    {
        private Dictionary<Type, UIState> _states;
        private UIState _currentState;
        private readonly Game _game;

        public UIManager(Game game)
        {
            _game = game;
            _states = new Dictionary<Type, UIState>();
        }

        public void Initialize()
        {
            foreach (var state in _states.Values)
                state.Initialize();
        }

        public void LoadContent()
        {
            foreach (var state in _states.Values)
                state.LoadContent();
        }

        public T RegisterState<T>(T state) where T : UIState
        {
            _states[typeof(T)] = state;
            return state;
        }

        public void ChangeState<T>() where T : UIState
        {
            if (!_states.TryGetValue(typeof(T), out var state))
                throw new ArgumentException($"State {typeof(T).Name} is not registered");

            _currentState = state;
        }

        public void Update(GameTime gameTime)
        {
            _currentState?.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _currentState?.Draw(gameTime, spriteBatch);
        }
    }
} 