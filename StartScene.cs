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
    internal class StartScene:Scene
    {
        public StartScene() 
        {
            LevelSprites.playButton.Origin = (Vector2f)LevelSprites.playButton.Texture.Size / 2;
            LevelSprites.playButton.Position = new Vector2f(Game.Size.X / 2, Game.Size.Y / 7*4);
        }
        public void Enable()
        {
        }
        public void Update()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space)|| Keyboard.IsKeyPressed(Keyboard.Key.Up)) 
            {
                Game.StartGame();
            }
        }
        public void Dispose() {}
        public void Draw(RenderTarget target, RenderStates states) { target.Draw(LevelSprites.background,states); target.Draw(LevelSprites.playButton,states); }
    }
}
