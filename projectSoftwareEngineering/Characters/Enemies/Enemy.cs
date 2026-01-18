using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Animations;
using projectSoftwareEngineering.Interfaces;
using projectSoftwareEngineering.Systems;
using System.Collections.Generic;
using System.Linq;

namespace projectSoftwareEngineering.Characters.Enemies
{
    public abstract class Enemy : IGameObject, ICollidable, IDamageable
    {
        protected Dictionary<string, Texture2D> _textures;
        protected bool _isDying = false;
        protected float _deathTimer = 0f;
        protected const float DEATH_DURATION = 1.0f;
        public bool ReadyToRemove { get; protected set; } = false;

        public Physics _physics;       
        public bool _facingRight;
        protected AnimationController _animationController;
        protected SpriteEffects _direction = SpriteEffects.None;
        public abstract int Width { get; }
        public abstract int Height { get; }
        public virtual Rectangle Bounds => new Rectangle(
            (int)_physics.Position.X + 18,
            (int)_physics.Position.Y + 18,
            28, 30
        );

        public Health Health { get; set; }
        public bool IsSolid => false;

        public Enemy(Dictionary<string, Texture2D> textures, ICharacterConfig config, int health, AnimationSet animationSet)
        {
            _textures = textures;
            _facingRight = true;

            _physics = new Physics(
                config.StartPosition,
                config.Gravity,
                config.JumpStrength,
                config.MoveSpeed
            );

            Health = new Health(health);
            _animationController = new AnimationController(animationSet);
        }
        protected abstract string GetCurrentTextureKey();
        protected Texture2D GetCurrentTexture()
        {
            string key = GetCurrentTextureKey();
            if (_textures.ContainsKey(key))
            {
                return _textures[key];
            }
            return _textures.Values.First();
        }
        public virtual void Die()
        {
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gametime);
        public abstract void Update(GameTime gametime, List<ICollidable> collidables, CollisionManager collisionManager);
    }
}
