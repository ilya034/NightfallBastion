using System;
using Microsoft.Xna.Framework;

namespace NightfallBastion.World.Buildings
{
    public abstract class Building(Rectangle sourceRect, bool isSolid, int maxHealth)
    {
        public Rectangle SourceRect { get; protected set; } = sourceRect;

        public bool IsSolid { get; protected set; } = isSolid;

        public int MaxHealth { get; protected set; } = maxHealth;

        public int CurrentHealth { get; protected set; } = maxHealth;

        public bool IsDestroyed => CurrentHealth <= 0;

        public virtual bool TakeDamage(int damage)
        {
            if (CurrentHealth <= 0 || damage <= 0)
                return false;

            CurrentHealth = Math.Max(0, CurrentHealth - damage);

            if (IsDestroyed && IsSolid)
            {
                IsSolid = false;
                OnDestroyed();
            }

            return true;
        }

        public virtual bool Repair(int amount)
        {
            if (amount <= 0 || CurrentHealth >= MaxHealth)
                return false;

            CurrentHealth = Math.Min(MaxHealth, CurrentHealth + amount);

            if (!IsDestroyed && !IsSolid)
            {
                IsSolid = true;
                OnRepaired();
            }

            return true;
        }

        protected virtual void OnDestroyed() { }

        protected virtual void OnRepaired() { }

        public virtual Color GetRenderColor()
        {
            if (MaxHealth <= 0)
                return Color.White;

            float healthPercent = (float)CurrentHealth / MaxHealth;

            return new Color(1.0f, healthPercent, healthPercent, 1.0f);
        }
    }
}
