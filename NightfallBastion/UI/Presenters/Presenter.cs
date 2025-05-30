using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public abstract class Presenter(NightfallBastionGame game)
    {
        protected readonly NightfallBastionGame _game = game;

        public virtual void RegisterEvents() { }

        public virtual void Dispose() { }
    }
}
