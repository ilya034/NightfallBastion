using Microsoft.Xna.Framework;

namespace NightfallBastion.UI
{
    public interface IPresenter
    {
        void Initialize();
        void LoadContent();
        void Update(GameTime gameTime);
    }
}