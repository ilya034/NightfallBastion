using Myra.Graphics2D.UI;

namespace NightfallBastion.UI
{
    public abstract class BaseView
    {
        public Panel RootElement { get; }

        public BaseView()
        {
            RootElement = new Panel();
        }

        public virtual void BuildUI() { }
    }
}
