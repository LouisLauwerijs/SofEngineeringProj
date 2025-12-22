using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering
{
    public class Camera
    {
        private float _position;
        private int _visibility;

        public Matrix Transform { get; private set; }

        public Camera(int visibility)
        {
            _visibility = visibility;
            _position = 0;
            UpdateTransform();
        }

        public void Follow(Vector2 positionPlayer)
        {
            _position = positionPlayer.X - (_visibility / 2f) + 80;

            UpdateTransform();
        }

        public void UpdateTransform()
        {
            Transform = Matrix.CreateTranslation(-_position, 0, 0);
        }

    }
}
