using System;
using Microsoft.Xna.Framework;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public abstract class Presenter(NightfallBastionGame game)
    {
        protected readonly NightfallBastionGame _game = game;

        public virtual void RegisterEvents() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Dispose() { }

        protected void RegisterButtonEvent<TView>(TView view, string eventName, Action handler)
        {
            var eventInfo = typeof(TView).GetEvent(eventName);
            if (eventInfo != null)
            {
                eventInfo.AddEventHandler(view, handler);
            }
        }
    }
}
