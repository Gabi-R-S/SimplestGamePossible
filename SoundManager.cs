using System;
using System.Collections.Generic;
using SFML.Audio;
namespace SimplestGamePossible
{
    enum MusicState 
    {
    Game,
    Menu
    }
    enum SoundType{Jump,Menu }
    static  class SoundManager
    {
        static Music gameMusic;
        static Music menuMusic;
        static Sound jump;
        static Sound menuPress;
        public static void PlaySound(SoundType type) 
        {
            if (type == SoundType.Jump) { jump.Play(); } else { menuPress.Play(); }
        }
        public static void SetMusic(MusicState state) { if (state == MusicState.Game) 
            {
                gameMusic.Play();
                menuMusic.Stop();
            }
            else 
            {
            menuMusic.Play();
                gameMusic.Stop();
            } 
        }

        public static void SetUp() 
        {
            SoundBuffer buffer = new SoundBuffer("Assets/MusicAndSound/Jump.wav");
            jump= new Sound(buffer);
            jump.Loop = false;

            buffer = new SoundBuffer("Assets/MusicAndSound/MenuSelect.wav");
            menuPress = new Sound(buffer);
            menuPress.Loop = false;

            gameMusic = new Music("Assets/MusicAndSound/GameMusic.wav");
            gameMusic.Loop = true;

            menuMusic = new Music("Assets/MusicAndSound/MenuMusic.wav");
            menuMusic.Loop = true;
        }
    }
}
