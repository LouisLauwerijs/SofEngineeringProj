using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Animations;
using projectSoftwareEngineering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters.Hero
{
    public class MeleeAttack : IAttack
    {
        private float _cooldownTimer;
        private float _cooldownDuration;
        private float _attackTimer;
        private float _attackDuration;
        private int _heroWidth;

        private Vector2 _attackPosition;
        private bool _facingRight;
        private bool _isAttacking;

        private int _attackWidth = 40;
        private int _attackHeight = 55;
        private int _attackOffsetX = -20;
        private int _attackOffsetY = -15;

        private HashSet<IDamageable> _hitThisAttack;

        public bool IsActive => _isAttacking;
        public bool CanAttack => _cooldownTimer <= 0;

        public Rectangle Hitbox
        {
            get
            {
                if (!_isAttacking) return Rectangle.Empty;

                int xPos = _facingRight
                    ? (int)_attackPosition.X + _heroWidth + _attackOffsetX
                    : (int)_attackPosition.X - _attackWidth - _attackOffsetX;

                return new Rectangle(
                    xPos,
                    (int)_attackPosition.Y + _attackOffsetY,
                    _attackWidth,
                    _attackHeight
                );
            }
        }

        public MeleeAttack(int heroWidth, float cooldownDuration = 0.5f, float attackDuration = 0.18f)
        {
            _heroWidth = heroWidth;
            _cooldownDuration = cooldownDuration;
            _attackDuration = attackDuration;
            _cooldownTimer = 0;
            _attackTimer = 0;
            _isAttacking = false;
            _hitThisAttack = new HashSet<IDamageable>();
        }

        public void Execute(Vector2 position, bool facingRight)
        {
            if (!CanAttack)
                return;

            _isAttacking = true;
            _attackPosition = position;
            _facingRight = facingRight;
            _cooldownTimer = _cooldownDuration;
            _attackTimer = _attackDuration;
            _hitThisAttack.Clear();
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_cooldownTimer > 0)
            {
                _cooldownTimer -= deltaTime;
            }

            if (_isAttacking)
            {
                _attackTimer -= deltaTime;

                if (_attackTimer <= 0)
                {
                    _isAttacking = false;
                }
            }
        }

        public List<IDamageable> CheckCollisions(List<IDamageable> targets)
        {
            if (!_isAttacking) return new List<IDamageable>();

            var hitTargets = new List<IDamageable>();
            Rectangle attackHitbox = Hitbox;

            foreach (var target in targets)
            {
                if (_hitThisAttack.Contains(target)) continue;

                if (target is ICollidable collidable)
                {
                    if (attackHitbox.Intersects(collidable.Bounds))
                    {
                        hitTargets.Add(target);
                        _hitThisAttack.Add(target);
                    }
                }
            }

            return hitTargets;
        }
    }
}
