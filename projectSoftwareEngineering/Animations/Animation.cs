using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Animations
{
    public class Animation
    {
        public List<AnimationFrame> Frames { get; set; } = new List<AnimationFrame>();
        public double FrameInterval { get; set; } = 100;
        private double timer = 0;
        public int CurrentFrame { get; private set; } = 0;

        public void AddFrame(AnimationFrame frame)
        {
            Frames.Add(frame);
        }

        public Rectangle GetCurrentFrameRectangle()
        {
            return Frames[CurrentFrame].SourceRectangle;
        }

        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= FrameInterval)
            {
                CurrentFrame++;
                if (CurrentFrame >= Frames.Count)
                    CurrentFrame = 0;

                timer = 0;
            }
        }
    }

}
