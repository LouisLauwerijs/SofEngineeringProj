using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace projectSoftwareEngineering
{
    public abstract class Enemy : IGameObject, ICollidable, IDamageable
    {
        public Texture2D _texture;
        public Physics _physics;       
        public bool _facingRight;

        //For different size enemies
        public abstract int Width { get; }
        public abstract int Height { get; }
        public Rectangle Bounds => new Rectangle(
            (int)_physics.Position.X + 18,
            (int)_physics.Position.Y + 18,
            28, 32
        );

        public Health Health { get; set; }
        public bool IsSolid => false;

        public Enemy(Texture2D texture, Vector2 spawnPoint, int health)
        {
            _texture = texture;
            _facingRight = true;

            _physics = new Physics(
                spawnPoint,
                gravity: 0.4f,
                jumpStrength: 0,
                moveSpeed: 0.75f
            );

            Health = new Health(health);
        }
        public virtual void Die()
        {
            //TODO death animation
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gametime);
        public abstract void Update(GameTime gametime, List<ICollidable> collidables, CollisionManager collisionManager);
    }
}
