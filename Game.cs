using SFML.Graphics;
using SFML.Window;
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
            window = new RenderWindow(new VideoMode((uint)(VideoMode.DesktopMode.Width * 0.6f), (uint)(VideoMode.DesktopMode.Height*0.6f)),"Ninja Run",Styles.Default,settings);

            renderTexture = new RenderTexture(Size.X,Size.Y);
            window.Closed += (object? sender, EventArgs args) => { window.Close(); };
            window.Resized+= (object? sender, SizeEventArgs args) => 
           {   var xGrowth = (float)args.Width/(float)Size.X;
               var yGrowth = (float)args.Height / (float)Size.Y;

               View view = new View(new FloatRect(0,0,(int)args.Width, (int)args.Height));
               window.SetView(view);

               Vector2f newSize = (Vector2f)Size*MathF.Min(xGrowth, yGrowth);
               Vector2f screenSize = new Vector2f(args.Width,args.Height);
               sprite.Position = (Vector2f)screenSize / 2 - (Vector2f)newSize / 2;
               sprite.Scale= new Vector2f(Math.Min(xGrowth, yGrowth), Math.Min(xGrowth, yGrowth));
           };
            window.GainedFocus += (object? sender, EventArgs args) => { window.RequestFocus(); };
            sprite = new Sprite();
            var xGrowth = (float)window.Size.X / (float)Size.X;
            var yGrowth = (float)window.Size.Y / (float)Size.Y;

            View view = new View(new FloatRect(0, 0, (int)window.Size.X, (int)window.Size.Y));
            window.SetView(view);

            Vector2f newSize = (Vector2f)Size * MathF.Min(xGrowth, yGrowth);
            Vector2f screenSize = new Vector2f(window.Size.X, window.Size.Y);
            sprite.Position = (Vector2f)screenSize / 2 - (Vector2f)newSize / 2;
            sprite.Scale = new Vector2f(Math.Min(xGrowth, yGrowth), Math.Min(xGrowth, yGrowth));

            LevelSprites.SetUp();
            SoundManager.SetUp();
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
                sprite.Texture.Smooth = true;
                window.Clear();
                window.Draw(sprite);
                window.Display();
            }
        }
        public static Texture GetScreenTexture() { return new Texture(renderTexture.Texture); }
    }
}
