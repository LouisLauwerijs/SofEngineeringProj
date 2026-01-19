using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Animations
{
    /// <summary>
    /// Representeert een enkel frame binnen een animatie.
    /// Bevat de rectangle die definieert welk deel van de sprite sheet getekend moet worden.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Heeft één duidelijke verantwoordelijkheid - 
    ///   het vasthouden van de source rectangle data voor een animatie frame.
    /// - Data Transfer Object (DTO) Pattern: Fungeert als een eenvoudig data container object
    ///   zonder complexe business logica.
    /// </summary>
    public class AnimationFrame
    {
        /// <summary>
        /// De rectangle die het gebied in de sprite sheet texture definieert.
        /// X en Y geven de positie aan, Width en Height geven de grootte.
        /// </summary>
        public Rectangle SourceRectangle { get; set; }

        /// <summary>
        /// Constructor die een AnimationFrame initialiseert met een source rectangle.
        /// </summary>
        /// <param name="sourceRectangle">Rectangle die het frame gebied in de texture definieert</param>
        public AnimationFrame(Rectangle sourceRectangle)
        {
            SourceRectangle = sourceRectangle;
        }
    }
}   