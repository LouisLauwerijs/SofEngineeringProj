using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Interfaces;
using System.Collections.Generic;

namespace projectSoftwareEngineering.Characters.Enemies
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

        public Enemy(Texture2D texture, ICharacterConfig config, int health)
        {
            _texture = texture;
            _facingRight = true;

            _physics = new Physics(
                config.StartPosition,
                config.Gravity,
                config.JumpStrength,
                config.MoveSpeed
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
