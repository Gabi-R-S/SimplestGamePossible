using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace SimplestGamePossible
{
    internal class GameOverScene:Scene
    {
        Sprite lastFrame;
        Texture deathFrame;
        public GameOverScene() 
        {
            lastFrame = new Sprite();
        }
        public void Enable() 
        {
            deathFrame = new Texture(Game.GetScreenImage());

            lastFrame.Texture = deathFrame;
          
        }
        public void Update()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) || Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                Game.StartGame();
            }
        }
        public void Dispose() { deathFrame.Dispose(); }
        public void Draw(RenderTarget target, RenderStates states) 
        {
            lastFrame.Draw(target, states);
        }
    }
}
