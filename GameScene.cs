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
    internal class GameScene:Scene
    {
        Shader depthShader;
        
        Vector2f playerBasePos;

        List<Hill> hills;
        const int numberOfHills = 8;
        
        List<Cloud> clouds;
        const int numberOfClouds = 10;

        readonly float killDistanceSqr;
        
        const float jumpTime=2.0f;
        float jumpTimeCounter;
        const float jumpHeight = 350;
        const float jumpRotationBoost = 1.5f;
        float averageScrollSpeed;
        float rotationSpeed;

        const float increase =1.05f;

        uint points;

        void RandomizeBackGround() { //setRandomPositionFor Background Decorations
            foreach (var cld in clouds) 
            {
                cld.position.X = (float)Random.Shared.NextDouble() * Game.Size.X;
            }
            foreach (var hill in hills)
            {
                hill.position.X = (float)Random.Shared.NextDouble() * Game.Size.X;
            }
        }
        Vector2f NextEnemyPosition() 
        {
            //change to variable later?
            return new Vector2f(Game.Size.X+ LevelSprites.enemy.Texture.Size.X+Random.Shared.Next(10,100), Game.Size.Y/11*9- LevelSprites.enemy.Texture.Size.Y);
        }
        void AddHill() 
        {
            Hill hill = new Hill(LevelSprites.backgroundMountain.Texture.Size);
            if (hills.Count == 0) { hills.Add(hill); }
            for (int i = 0; i < hills.Count-1; i++) 
            {
                if (hill.depth > hills[i].depth && hill.depth < hills[i + 1].depth) 
                {
                    hills.Insert(i,hill);
                    return;
                }
            }
            if (hill.depth > hills[hills.Count - 1].depth)
            {
                hills.Add(hill);
            }
            else { hills.Insert(0, hill); }
        }
        void FillHills()
        {
            while (hills.Count != numberOfHills) { AddHill(); }
        }
        void FillClouds()
        {
            while (clouds.Count != numberOfClouds) { clouds.Add(new Cloud(LevelSprites.cloud.Texture.Size)); }
        }
        float GetPlayerHeight() 
        {
            return playerBasePos.Y+jumpTimeCounter*(jumpTimeCounter-jumpTime)*jumpHeight;
        }
        void OnKeyPress(object? sender, KeyEventArgs args) 
        {
            switch (args.Code)
            {
                case Keyboard.Key.Up:
                    if (jumpTimeCounter == 0) 
                    {
                        jumpTimeCounter = jumpTime;
                    }
                    break;
                case Keyboard.Key.Space:
                    if (jumpTimeCounter == 0)
                    {
                        jumpTimeCounter = jumpTime;
                    }
                    break;
                default:
                    break;
            }
        }
        public GameScene() 
        {
        
                LevelSprites.enemy.Position = NextEnemyPosition();
                playerBasePos = new Vector2f(Game.Size.X / 5, Game.Size.Y - LevelSprites.ground.Texture.Size.Y - LevelSprites.player.Texture.Size.Y / 2 + LevelSprites.player.Texture.Size.Y / 6.5f);
                LevelSprites.player.Position = playerBasePos;

            depthShader = new Shader(null,null,"Assets/Shaders/Shader.glsl");

            hills = new List<Hill>();
            FillHills();
            killDistanceSqr = LevelSprites.player.Texture.Size.Y* LevelSprites.player.Texture.Size.Y / 20; 
            
            clouds = new List<Cloud>();
            FillClouds();

            averageScrollSpeed = 350;
            rotationSpeed = 150;
            RandomizeBackGround();
        }

        public void Draw(RenderTarget target, RenderStates states) 
        {
            LevelSprites.background.Draw(target,states);
            //draw hills
            foreach (Hill hill in hills) 
            {
                RenderStates newState= states;
                newState.Transform=states.Transform* hill.Transform;
                LevelSprites.backgroundMountain.Draw(target, newState);
            }
            foreach (Cloud cld in clouds)
            {
                RenderStates newState = states;
                newState.Transform = states.Transform * cld.Transform;
                LevelSprites.cloud.Draw(target, newState);
            }
            LevelSprites.ground.Draw(target,states);
            LevelSprites.enemy.Draw(target,states);
            LevelSprites.player.Draw(target,states);
        }
        
        public void Update() 
        {
            //set the numbers to variables later
            if (jumpTimeCounter==0) 
            {
                LevelSprites.player.Rotation += Time.FrameTime * rotationSpeed;
            }
            else 
            {
                LevelSprites.player.Rotation += Time.FrameTime * rotationSpeed*jumpRotationBoost;
            }
            for (var x=0; x<hills.Count;x++)
            {
                hills[x].position.X -= averageScrollSpeed * hills[x].depth * Time.FrameTime;
                if (hills[x].position.X <-LevelSprites.backgroundMountain.Texture.Size.X* hills[x].Scale.X) 
                {
                    hills.RemoveAt(x);
                    x--;
                    
                }
            }
            FillHills();
            for (var x = 0; x < clouds.Count; x++)
            {
                clouds[x].position.X -= averageScrollSpeed * clouds[x].speed*Time.FrameTime;
                if (clouds[x].position.X < 0 - LevelSprites.cloud.Texture.Size.X * clouds[x].Scale.X)
                {
                    clouds.RemoveAt(x);
                    x--;

                }
            }
            FillClouds();
            jumpTimeCounter -= Time.FrameTime;
            if (jumpTimeCounter < 0) {jumpTimeCounter = 0;}

            LevelSprites.player.Position = new Vector2f(playerBasePos.X,GetPlayerHeight());

            LevelSprites.enemy.Position -= new Vector2f(averageScrollSpeed * Time.FrameTime,0);
            if (LevelSprites.enemy.Position.X < -LevelSprites.enemy.Texture.Size.X) 
            {
                LevelSprites.enemy.Position=NextEnemyPosition();
                points++;
                averageScrollSpeed *= increase;
                rotationSpeed *= increase;
            }
            var dist = LevelSprites.enemy.Position - LevelSprites.player.Position;
            var distancesqr= dist.X*dist.X + dist.Y*dist.Y;

            if (distancesqr <killDistanceSqr) 
            {
                Game.GameOver();
            }
        }

        public void Dispose() 
        {

            Game.window.KeyPressed -= OnKeyPress;
        }

        public void Enable()
        {
            Game.window.KeyPressed += OnKeyPress;
            points = 0;
            LevelSprites.enemy.Position = NextEnemyPosition();
            averageScrollSpeed = 350;
            rotationSpeed = 150;
        }
    }
}
