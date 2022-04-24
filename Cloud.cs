using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace SimplestGamePossible
{
    internal class Cloud
    {
        public readonly float speed;
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
        public Cloud(Vector2u texSize)
        {
            speed = (float)Random.Shared.NextDouble();
            position = new Vector2f(Game.Size.X + texSize.X+ Random.Shared.Next(50, 1000), Game.Size.Y / 16 + texSize.Y / 2+ (float)Random.Shared.Next(0, (int)texSize.Y*6));
            Scale = new Vector2f((float)Random.Shared.Next(50, 150) / 100, (float)Random.Shared.Next(50, 150) / 100);
        }
    }
}
