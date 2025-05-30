using Microsoft.Xna.Framework;

namespace NightfallBastion.UI.ViewModels
{
    public class EnemyViewModel
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public Rectangle TextureRegion { get; set; }
        public float Scale { get; set; }

        public Vector2 Position
        {
            get => new(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public float HealthPercentage
        {
            get => MaxHealth > 0 ? (float)Health / MaxHealth : 0f;
        }

        public EnemyViewModel(
            float x,
            float y,
            int health,
            int maxHealth,
            Rectangle textureRegion,
            float scale = 1.0f
        )
        {
            X = x;
            Y = y;
            Health = health;
            MaxHealth = maxHealth;
            TextureRegion = textureRegion;
            Scale = scale;
        }

        public EnemyViewModel()
        {
            Scale = 1.0f;
        }
    }
}
