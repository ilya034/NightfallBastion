using System;
using Myra.Graphics2D.UI;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public abstract class View(NightfallBastionGame game)
    {
        protected readonly NightfallBastionGame _game = game;
        public Panel RootElement { get; } = new Panel();

        public virtual void LoadContent() { }

        public virtual void BuildUI() { }

        public virtual void Draw() { }

        public virtual void UnloadContent() { }

        protected Button CreateMenuButton(string text, Action? onClick)
        {
            var button = new Button
            {
                Width = _game.CoreSettings.DefaultButtonWidth,
                Height = _game.CoreSettings.DefaultButtonHeight,
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = new Label
                {
                    Text = text,
                    TextColor = Microsoft.Xna.Framework.Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };
            if (onClick != null)
                button.Click += (_, __) => onClick.Invoke();
            return button;
        }
    }
}
