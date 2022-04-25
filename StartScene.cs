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
        const float transitionTime=1.0f;
        float counter;
        bool transition;
        RectangleShape shape;
        readonly Color transitionColor;
        public StartScene() 
        {
            LevelSprites.playButton.Origin = (Vector2f)LevelSprites.playButton.Texture.Size / 2;
            LevelSprites.playButton.Position = new Vector2f(Game.Size.X / 2, Game.Size.Y / 7*4);
            transitionColor = new Color(77,43,50,0);
            shape = new RectangleShape((Vector2f)Game.Size);
            LevelSprites.pointText.DisplayedString = "Jump!";
            LevelSprites.pointText.CharacterSize = 200;
            LevelSprites.pointText.Position = new Vector2f(Game.Size.X/2-LevelSprites.pointText.GetGlobalBounds().Width/2, Game.Size.Y/22);

            SoundManager.SetMusic(MusicState.Menu);
        }
        public void Enable()
        {
        }
        public void Update()
        {
            if (!transition&&(Keyboard.IsKeyPressed(Keyboard.Key.Space)|| Keyboard.IsKeyPressed(Keyboard.Key.Up))) 
            {
                transition = true;
                SoundManager.PlaySound(SoundType.Menu);

            }
            if (transition) 
            {
                counter += Time.FrameTime;

            }
            if (counter >= transitionTime) 
            {
                Game.StartGame();
            }
            var finalColor= new Color(transitionColor);
            finalColor.A = (byte)Math.Clamp(counter / transitionTime*255, 0, 255);
            shape.FillColor = finalColor;
        }
        public void Dispose() {}
        public void Draw(RenderTarget target, RenderStates states) {  target.Draw(LevelSprites.background,states); target.Draw(LevelSprites.playButton,states); target.Draw(shape, states); LevelSprites.pointText.Draw(target, states); }
    }
}
