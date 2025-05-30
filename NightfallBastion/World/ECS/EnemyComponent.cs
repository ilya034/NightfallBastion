namespace NightfallBastion.World
{
    public class EnemyComponent(int health, int damage) : Component
    {
        public int MaxHealth { get; } = health;
        public int CurrentHealth { get; set; } = health;
        public int Damage { get; } = damage;
        public bool IsAlive => CurrentHealth > 0;
    }
}
