using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters.Enemies.ShooterEnemy
{
    public class Projectile : IProjectile
    {
        private Vector2 _position;
        private Vector2 _velocity;
        private float _maxDistance;
        private float _distanceTraveled;
        private Texture2D _texture;

        public bool IsActive { get; private set; }

        public Rectangle Bounds => new Rectangle(
            (int)_position.X,
            (int)_position.Y,
            8, 8
        );

        public bool IsSolid => false;

        public Projectile(Texture2D texture, Vector2 startPosition, float direction, float speed, float maxDistance)
        {
            _texture = texture;
            _position = startPosition;
            _velocity = new Vector2(direction * speed, 0);
            _maxDistance = maxDistance;
            _distanceTraveled = 0;
            IsActive = true;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsActive) return;

            _position += _velocity;
            _distanceTraveled += System.Math.Abs(_velocity.X);

            if (_distanceTraveled >= _maxDistance)
            {
                Deactivate();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsActive) return;

            spriteBatch.Draw(_texture, Bounds, Color.Yellow);
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
