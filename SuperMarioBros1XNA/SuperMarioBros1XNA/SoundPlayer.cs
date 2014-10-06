using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SuperMarioBros1XNA
{
    public class SoundPlayer
    {
        private static SoundPlayer instance;

        public static String DIE = "die";
        public static String JUMP_SMALL = "jump_s";
        
        private Dictionary<String, SoundEffect> soundEffects;

        private Song overworld;
        
        public void Initialize(ContentManager content) 
        {
            soundEffects = new Dictionary<string, SoundEffect>();
            soundEffects.Add(JUMP_SMALL, content.Load<SoundEffect>(@"Audio/smb_jump_small"));
            soundEffects.Add(DIE, content.Load<SoundEffect>(@"Audio/smb_mario_die"));

            overworld = content.Load<Song>("Audio/smb_overworld21");
        }
        
        public void playBackgroundMusic() 
        {
            MediaPlayer.Play(overworld);
        }

        public void stopBackgroundMusic()
        {
            MediaPlayer.Stop();
        }



        public void playSound(String effectName) 
        {
            try
            {
                soundEffects[effectName].Play();
                //soundEffects[effectName].
            }
            catch (Exception e) 
            {
                Debug.Write("\nTry to play the effect " + effectName);
            }
            
        }

        public static SoundPlayer Instance
        {
            get
            {
                if (instance == null) 
                {
                    instance = new SoundPlayer();
                }
                return instance;
            }
        }

        private SoundPlayer() 
        {

        }
        
    }
}
