using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace SimplestGamePossible
{
    internal class Hill
    {
        public readonly float depth;
        public Transform Transform
        {
            get
            {
                var transform = Transform.Identity;
                transform.Translate(position);
                transform.Scale(Scale);

                return transform;
            }
        }
        public Vector2f position;
        public readonly Vector2f Scale;
        public Hill(Vector2u texSize)
        {
            depth = (float)Random.Shared.NextDouble();
            position = new Vector2f(Game.Size.X+texSize.X+ Random.Shared.Next(50, 1000), Game.Size.Y / 10 * 9-texSize.Y/2);
            Scale = new Vector2f((float)Random.Shared.Next(50, 150) / 100, (float)Random.Shared.Next(50, 150) / 100);
        }
    }
}
