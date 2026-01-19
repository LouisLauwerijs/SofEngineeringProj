using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Animations
{
    /// <summary>
    /// Beheert een enkele animatie door frames te doorlopen op basis van tijd.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Deze klasse heeft één verantwoordelijkheid - 
    ///   het beheren van een animatie sequentie met frames, timing en loop logica.
    /// - Open/Closed Principle (OCP): De klasse is open voor uitbreiding (je kan frames toevoegen)
    ///   maar gesloten voor modificatie (de kern animatie logica blijft ongewijzigd).
    /// </summary>
    public class Animation
    {
        /// <summary>
        /// Lijst van alle frames die deze animatie bevat.
        /// </summary>
        public List<AnimationFrame> Frames { get; set; } = new List<AnimationFrame>();

        /// <summary>
        /// Tijd in milliseconden tussen elk frame.
        /// Standaard waarde is 100ms (10 frames per seconde).
        /// </summary>
        public double FrameInterval { get; set; } = 100;

        /// <summary>
        /// Bepaalt of de animatie moet herhalen wanneer het laatste frame bereikt is.
        /// </summary>
        public bool Loop { get; set; } = true;

        /// <summary>
        /// Interne timer die bijhoudt hoeveel tijd er verstreken is sinds het laatste frame.
        /// </summary>
        private double timer = 0;

        /// <summary>
        /// Index van het huidige frame dat wordt weergegeven.
        /// </summary>
        public int CurrentFrame { get; private set; } = 0;

        /// <summary>
        /// Geeft aan of een niet-loopende animatie klaar is (laatste frame bereikt).
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        /// Voegt een nieuw frame toe aan de animatie.
        /// </summary>
        /// <param name="frame">Het AnimationFrame object om toe te voegen</param>
        public void AddFrame(AnimationFrame frame)
        {
            Frames.Add(frame);
        }

        /// <summary>
        /// Haalt de source rectangle op van het huidige frame.
        /// Dit wordt gebruikt om het juiste deel van de sprite sheet te tekenen.
        /// </summary>
        /// <returns>Rectangle die het huidige frame definieert in de texture</returns>
        public Rectangle GetCurrentFrameRectangle()
        {
            return Frames[CurrentFrame].SourceRectangle;
        }

        /// <summary>
        /// Update de animatie logica op basis van verstreken game tijd.
        /// Verhoogt de interne timer en schakelt naar het volgende frame wanneer nodig.
        /// </summary>
        /// <param name="gameTime">GameTime object met timing informatie</param>
        public void Update(GameTime gameTime)
        {
            // Stop met updaten als de animatie klaar is
            if (IsFinished)
                return;

            // Verhoog de timer met verstreken tijd
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            // Controleer of het tijd is om naar het volgende frame te gaan
            if (timer >= FrameInterval)
            {
                CurrentFrame++;

                // Controleer of we het einde van de animatie hebben bereikt
                if (CurrentFrame >= Frames.Count)
                {
                    if (Loop)
                    {
                        // Start opnieuw vanaf het begin bij looping animaties
                        CurrentFrame = 0;
                    }
                    else
                    {
                        // Bij niet-looping animaties blijven we op het laatste frame
                        CurrentFrame = Frames.Count - 1;
                        IsFinished = true;
                    }
                }

                // Reset de timer voor het volgende frame interval
                timer = 0;
            }
        }

        /// <summary>
        /// Reset de animatie naar de beginstate.
        /// Nuttig wanneer je een animatie opnieuw wil afspelen.
        /// </summary>
        public void Reset()
        {
            CurrentFrame = 0;
            timer = 0;
            IsFinished = false;
        }
    }
}