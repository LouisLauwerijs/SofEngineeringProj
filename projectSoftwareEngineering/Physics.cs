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
        public bool IsGrounded { get; set; }

        private readonly float _gravity;
        private readonly float _jumpStrength;
        private readonly float _moveSpeed;

        public Physics(Vector2 startPosition, float gravity, float jumpStrength, float moveSpeed)
        {
            Position = startPosition;
            _gravity = gravity;
            _jumpStrength = jumpStrength;
            _moveSpeed = moveSpeed;
            Velocity = Vector2.Zero;
            IsGrounded = false;
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
        }

        public void StopHorizontalMovement()
        {
            Velocity = new Vector2(0, Velocity.Y);
        }
    }
}
