using Microsoft.Xna.Framework;

namespace projectSoftwareEngineering.Characters
{
    public class Health
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public bool IsInvulnerable { get; set; }

        private float _invulnerabilityTimer;

        public Health(int maxHealth) {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            IsInvulnerable = false;
        }

        public void TakeDamage()
        {
            if (IsInvulnerable || CurrentHealth <= 0)
                return;

            CurrentHealth -= 1;
            if (CurrentHealth > 0)
            {
                IsInvulnerable = true;
                _invulnerabilityTimer = 1;
            }
        }

        public void Heal()
        {
            CurrentHealth += 1;
            if (CurrentHealth >= MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        public void Reset()
        {
            CurrentHealth = MaxHealth;
            IsInvulnerable = false;
            _invulnerabilityTimer = 0;
        }

        public void VulnerableUpdate(GameTime gameTime)
        {
            if (IsInvulnerable)
            {
                _invulnerabilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_invulnerabilityTimer <= 0)
                    IsInvulnerable = false;
            }
        }
    }
}
