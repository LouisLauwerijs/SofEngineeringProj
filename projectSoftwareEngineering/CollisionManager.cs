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
        public bool IsStandingOnGroud(Rectangle characterBounds, List<ICollidable> platforms)
        {
            Rectangle floorCheck = new Rectangle(
                characterBounds.X + 5,
                characterBounds.Bottom - 2,
                characterBounds.Width - 10,
                4
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

        public void SolidCollisionCheck(Physics physics, List<ICollidable> collidables)
        {
            Rectangle bounds = new Rectangle(
                (int)physics.Position.X + 18,
                (int)physics.Position.Y + 18,
                28, 32
            );

            foreach (var collidable in collidables)
            {
                if (!collidable.IsSolid) continue;
                if (!(collidable is Floor)) continue;

                if (bounds.Intersects(collidable.Bounds))
                {
                    // Calculate overlap on each axis
                    int overlapLeft = bounds.Right - collidable.Bounds.Left;
                    int overlapRight = collidable.Bounds.Right - bounds.Left;
                    int overlapTop = bounds.Bottom - collidable.Bounds.Top;
                    int overlapBottom = collidable.Bounds.Bottom - bounds.Top;

                    // Find where player collides
                    int minOverlap = Math.Min(
                        Math.Min(overlapLeft, overlapRight),
                        Math.Min(overlapTop, overlapBottom)
                    );

                    // Check for lowest overlap
                    if (minOverlap == overlapTop && physics.Velocity.Y > 0)
                    {
                        // Land on floor 
                        physics.Position = new Vector2(
                            physics.Position.X,
                            collidable.Bounds.Top - 32 - 18
                        );
                        physics.Velocity = new Vector2(physics.Velocity.X, 0);
                        physics.IsGrounded = true;
                    }
                    else if (minOverlap == overlapBottom && physics.Velocity.Y < 0)
                    {
                        // Hit ceiling 
                        physics.Position = new Vector2(
                            physics.Position.X,
                            collidable.Bounds.Bottom - 18
                        );
                        physics.Velocity = new Vector2(physics.Velocity.X, 0);
                    }
                    else if (minOverlap == overlapLeft && physics.Velocity.X > 0)
                    {
                        // Hit left side
                        physics.Position = new Vector2(
                            collidable.Bounds.Left - 28 - 18,
                            physics.Position.Y
                        );
                        physics.Velocity = new Vector2(0, physics.Velocity.Y);
                    }
                    else if (minOverlap == overlapRight && physics.Velocity.X < 0)
                    {
                        // Hit right side
                        physics.Position = new Vector2(
                            collidable.Bounds.Right - 18,
                            physics.Position.Y
                        );
                        physics.Velocity = new Vector2(0, physics.Velocity.Y);
                    }
                }
            }
        }

        public void PlatformCollisionCheck(Physics physics, List<ICollidable> collidables)
        {
            Rectangle bounds = new Rectangle(
                (int)physics.Position.X + 18,
                (int)physics.Position.Y + 18,
                28, 32
            );

            foreach (var collidable in collidables)
            {
                if (!collidable.IsSolid) continue;
                if (!(collidable is Platform)) continue;

                if (bounds.Intersects(collidable.Bounds))
                {
                    if (physics.Velocity.Y > 0)
                    {
                        float previousBottom = physics.Position.Y + 18 + 32 - physics.Velocity.Y;
                        if (previousBottom <= collidable.Bounds.Top + 5)
                        {
                            physics.Position = new Vector2(
                                physics.Position.X,
                                collidable.Bounds.Top - 32 - 18
                            );
                            physics.Velocity = new Vector2(physics.Velocity.X, 0);
                            physics.IsGrounded = true;
                        }
                    }
                }
            }
        }
    }
}
