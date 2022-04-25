using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace SimplestGamePossible
{
    internal class GameOverScene:Scene
    {
        Sprite lastFrame;
        Texture deathFrame;
        const float initialWaitTime=2.5f;
        float counter;
        Shader timeShader;
        public GameOverScene() 
        {
            lastFrame = new Sprite();
            timeShader = new Shader(null,null,"Assets/Shaders/ShineShader.glsl");
            LevelSprites.restartButton.Position = (Vector2f)Game.Size / 2;
        }
        public void Enable() 
        {
            deathFrame = Game.GetScreenTexture();

            lastFrame.Texture = deathFrame;
          counter = 0;
            SoundManager.SetMusic(MusicState.Menu);
        }
        public void Update()
        {
            if (counter >= initialWaitTime)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space) || Keyboard.IsKeyPressed(Keyboard.Key.Up))
                {
                    Game.StartGame();
                    SoundManager.PlaySound(SoundType.Menu);
                }
            }
            else 
            {
                counter += Time.FrameTime;
            }
            timeShader.SetUniform("time", Time.GameTime);
        }
        public void Dispose() { deathFrame.Dispose(); }
        public void Draw(RenderTarget target, RenderStates states) 
        {
            RenderStates newState = states; newState.Shader = timeShader;
            lastFrame.Draw(target, newState);
            LevelSprites.restartButton.Draw(target,states);
        }
    }
}
