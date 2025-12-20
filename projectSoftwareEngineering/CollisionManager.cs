using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering
{
    public class CollisionManager
    {
        public bool CheckCollision(Rectangle a, Rectangle b)
        {
            return a.Intersects(b);
        }

        public bool IsStandingOnGroud(Rectangle characterBounds, List<ICollidable> platforms)
        {
            // Check if character is standing on any platform
            Rectangle floorCheck = new Rectangle(
                characterBounds.X + 5,
                characterBounds.Bottom,
                characterBounds.Width - 10,
                2
            );

            foreach (var platform in platforms)
            {
                if (platform.IsSolid && floorCheck.Intersects(platform.Bounds))
                {
                    return true;
                }
            }
            return false;
        }

        public void FloorCollisionCheck(Physics physics, List<ICollidable> platforms)
        {
            Rectangle bounds = new Rectangle(
                (int)physics.Position.X,
                (int)physics.Position.Y,
                64, 64
            );

            foreach (var platform in platforms)
            {
                if (!platform.IsSolid) continue;

                if (bounds.Intersects(platform.Bounds))
                {
                    // Falling down - land on platform
                    if (physics.Velocity.Y > 0)
                    {
                        physics.Position = new Vector2(
                            physics.Position.X,
                            platform.Bounds.Top - 64
                        );
                        physics.Velocity = new Vector2(physics.Velocity.X, 0);
                        physics.IsGrounded = true;
                    }
                    // Moving up - hit ceiling
                    else if (physics.Velocity.Y < 0)
                    {
                        physics.Position = new Vector2(
                            physics.Position.X,
                            platform.Bounds.Bottom
                        );
                        physics.Velocity = new Vector2(physics.Velocity.X, 0);
                    }
                }
            }
        }

        public void WallCollisionCheck(Physics physics, List<ICollidable> platforms)
        {
            Rectangle bounds = new Rectangle(
                (int)physics.Position.X,
                (int)physics.Position.Y,
                64, 64
            );

            foreach (var platform in platforms)
            {
                if (!platform.IsSolid) continue;

                if (bounds.Intersects(platform.Bounds))
                {
                    // Moving right - hit wall
                    if (physics.Velocity.X > 0)
                    {
                        physics.Position = new Vector2(
                            platform.Bounds.Left - 64,
                            physics.Position.Y
                        );
                    }
                    // Moving left - hit wall
                    else if (physics.Velocity.X < 0)
                    {
                        physics.Position = new Vector2(
                            platform.Bounds.Right,
                            physics.Position.Y
                        );
                    }
                    physics.Velocity = new Vector2(0, physics.Velocity.Y);
                }
            }
        }
    }
}
