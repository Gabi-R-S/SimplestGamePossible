using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML;
using SFML.System;
namespace SimplestGamePossible
{
    internal static class Game
    {
        public readonly static RenderWindow window;
        static RenderTexture renderTexture;
        public static readonly Vector2u Size;
        static Scene gameScene;
        static Scene startScene;
        static Scene gameOverScene;
        static Scene scene;
        static Sprite sprite;
        public static void StartGame() { scene.Dispose(); scene = gameScene;scene.Enable(); }
        public static void GameOver() { scene.Dispose(); scene = gameOverScene; scene.Enable(); }
        static Game() 
        {
            Size = new Vector2u(1920 , 1080);
            ContextSettings settings= new ContextSettings();
            settings.AntialiasingLevel = 8;
            window = new RenderWindow(new VideoMode(Size.X,Size.Y),"Ninja Run",Styles.Default,settings);
            renderTexture = new RenderTexture(Size.X,Size.Y);
            window.Closed += (object? sender, EventArgs args) => { window.Close(); };
            sprite = new Sprite();

            LevelSprites.SetUp();
            //change later
            gameScene = new GameScene();
            startScene = new StartScene();
            gameOverScene = new GameOverScene();
            scene = startScene;
            
        }
        public static void Run() 
        {
            while (window.IsOpen) 
            {
                window.DispatchEvents();
                Time.Click();
                scene?.Update();
                renderTexture.Clear();
                scene?.Draw(renderTexture, RenderStates.Default);
                renderTexture.Display();

                sprite.Texture = renderTexture.Texture;
                window.Clear();
                window.Draw(sprite);
                window.Display();
            }
        }
        public static Texture GetScreenImage() { return renderTexture.Texture; }
    }
}
