namespace NightfallBastion.World.Floors
{
    public class Floor(
        bool isPassable,
        float movementSpeedModifier,
        bool canBuild,
        bool isDamaging,
        float damagePerSecond
    )
    {
        public bool IsPassable { get; } = isPassable;

        public float MovementSpeedModifier { get; } = movementSpeedModifier;

        public bool CanBuild { get; } = canBuild;

        public bool IsDamaging { get; } = isDamaging;

        public float DamagePerSecond { get; } = damagePerSecond;
    }
}
