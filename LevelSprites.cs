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
    static class LevelSprites
    {
        public static Sprite player;
        public static Sprite ground;
        public static Sprite enemy;
        public static Sprite backgroundMountain;
        public static Sprite cloud;
        public static Sprite background;
        public static Sprite playButton;
        public static Text pointText;
        public static Sprite restartButton;
        public static void SetUp() {
            Texture tex = new Texture("Assets/Images/Player.png");
            player = new Sprite(tex);
            tex.Smooth = true;
            player.Origin = (Vector2f)tex.Size / 2;

            tex = new Texture("Assets/Images/BackgroundMountain.png");
            backgroundMountain = new Sprite(tex);
            tex.Smooth = true;

            tex = new Texture("Assets/Images/Cloud.png");
            cloud = new Sprite(tex);
            tex.Smooth = true;

            tex = new Texture("Assets/Images/Spiky.png");
            enemy = new Sprite(tex);
            tex.Smooth = true;

            tex = new Texture("Assets/Images/Ground.png");
            ground = new Sprite(tex);
            ground.Position = new Vector2f(0, Game.Size.Y - tex.Size.Y);
            tex.Smooth = true;

            tex = new Texture("Assets/Images/Background.png");
            background = new Sprite(tex);
            tex.Smooth = true;

            tex = new Texture("Assets/Images/PlayButton.png");
            playButton = new Sprite(tex);
            tex.Smooth = true;

            tex = new Texture("Assets/Images/RestartButton.png");
            restartButton = new Sprite(tex);
            tex.Smooth = true;
            restartButton.Origin = (Vector2f)tex.Size / 2;

            Font font = new Font("Assets/Fonts/BROADW.TTF");
            pointText = new Text();
            pointText.Font = font;
            pointText.FillColor = new Color(77, 43, 50, 255);
        }
    }
}
