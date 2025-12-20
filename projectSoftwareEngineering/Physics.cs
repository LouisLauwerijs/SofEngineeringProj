using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering
{
    public class Physics
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public bool IsGrounded { get; private set; }

        private readonly float _gravity;
        private readonly float _jumpStrength;
        private readonly float _moveSpeed;
        private readonly float _groundLevel;

        public Physics(Vector2 startPosition, float gravity, float jumpStrength, float moveSpeed, float groundLevel)
        {
            Position = startPosition;
            _gravity = gravity;
            _jumpStrength = jumpStrength;
            _moveSpeed = moveSpeed;
            _groundLevel = groundLevel;
            Velocity = Vector2.Zero;
            IsGrounded = true;
        }

        public void ApplyGravity()
        {
            if (!IsGrounded)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + _gravity);
            }
        }

        public void MoveHorizontal(float direction)
        {
            Velocity = new Vector2(direction * _moveSpeed, Velocity.Y);
            Position += new Vector2(Velocity.X, 0);
        }

        public void Jump()
        {
            if (IsGrounded)
            {
                Velocity = new Vector2(Velocity.X, _jumpStrength);
                IsGrounded = false;
            }
        }

        public void UpdateVerticalPosition()
        {
            Position += new Vector2(0, Velocity.Y);

            // Ground collision
            if (Position.Y >= _groundLevel)
            {
                Position = new Vector2(Position.X, _groundLevel);
                Velocity = new Vector2(Velocity.X, 0);
                IsGrounded = true;
            }
        }

        public void StopHorizontalMovement()
        {
            Velocity = new Vector2(0, Velocity.Y);
        }
    }
}
