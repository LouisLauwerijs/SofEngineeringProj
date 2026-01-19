using projectSoftwareEngineering.Interfaces;
using HeroClass = projectSoftwareEngineering.Characters.Hero.Hero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters.Enemies.ShooterEnemy
{
    /// <summary>
    /// Beheert collision detection en damage voor alle projectielen in de game.
    /// Controleert collision met targets (Hero) en solid objecten (muren, platforms).
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Enige verantwoordelijkheid is het afhandelen
    ///   van projectiel collisions en het toepassen van damage/knockback effecten.
    /// - Dependency Inversion Principle (DIP): Werkt met interfaces (IDamageable, ICollidable)
    ///   in plaats van concrete implementaties, behalve voor Hero-specifieke knockback.
    /// - Open/Closed Principle (OCP): Kan uitgebreid worden met nieuwe collision types
    ///   zonder bestaande logica te wijzigen.
    /// 
    /// Design Consideration:
    /// - Heeft een directe dependency op Hero klasse voor knockback functionality.
    ///   Dit kan verbeterd worden door een IKnockbackable interface te introduceren.
    /// </summary>
    public class ProjectileManager
    {
        /// <summary>
        /// Verwerkt alle collision checks voor een lijst van projectielen.
        /// Controleert zowel collision met het target (Hero) als met solid objecten.
        /// Past damage en knockback toe bij target collision.
        /// Deactiveert projectielen bij elke collision.
        /// </summary>
        /// <param name="projectiles">Lijst van actieve projectielen om te controleren</param>
        /// <param name="target">Het damageable target (meestal de Hero)</param>
        /// <param name="solidObjects">Lijst van solid objecten waarmee collision gecontroleerd moet worden</param>
        public void HandleProjectileCollisions(List<Projectile> projectiles, IDamageable target, List<ICollidable> solidObjects)
        {
            // Gebruik ToList() om een kopie te maken - voorkomt collection modified exception
            // tijdens iteratie als projectielen gedeactiveerd worden
            foreach (var projectile in projectiles.ToList())
            {
                // Skip inactieve projectielen
                if (!projectile.IsActive) continue;

                #region Target Collision (Hero)

                // Check of het target ook een collidable is (voor bounds checking)
                if (target is ICollidable collidableTarget)
                {
                    // Check rectangle intersection tussen projectiel en target
                    if (projectile.Bounds.Intersects(collidableTarget.Bounds))
                    {
                        // Pas damage toe aan het target
                        target.Health.TakeDamage();

                        // Extra functionaliteit voor Hero: knockback effect
                        if (target is HeroClass hero)
                        {
                            // Bereken knockback richting: weg van het projectiel
                            // Als projectiel links van hero is, knockback naar rechts (1)
                            // Als projectiel rechts van hero is, knockback naar links (-1)
                            float knockbackDirection = projectile.Bounds.Center.X < hero.Bounds.Center.X ? 1 : -1;
                            hero.ApplyKnockback(knockbackDirection);
                        }

                        // Deactiveer het projectiel na raken van target
                        projectile.Deactivate();
                        continue;  // Ga naar volgend projectiel
                    }
                }

                #endregion

                #region Solid Object Collision (Walls, Platforms, etc.)

                // Check collision met alle solid objecten
                foreach (var solid in solidObjects)
                {
                    // Alleen echte solid objecten checken (platforms zijn soms niet solid)
                    if (solid.IsSolid && projectile.Bounds.Intersects(solid.Bounds))
                    {
                        // Deactiveer het projectiel bij collision met solid object
                        projectile.Deactivate();
                        break;  // Stop met checken voor dit projectiel
                    }
                }

                #endregion
            }
        }
    }
}