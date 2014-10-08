using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SuperMarioBros1XNA
{
    public class FactoryEnemy
    {
        private enum Enemies { GOOMBA, KOOPA };

        public GameObject createEnemy(String enemyClass, Vector2 position)
        {
            enemyClass = enemyClass.ToUpper();
            Enemies enemy;
            GameObject gameObject = null;

            Enum.TryParse<Enemies>(enemyClass,out enemy);
            TextureResource textureRS = ServiceLocator.TextureService();

            switch (enemy) 
            {
                case Enemies.GOOMBA:
                    Dictionary<string, Animation> animationsGoomba = new Dictionary<string, Animation>();
                    animationsGoomba.Add("run", new Animation(textureRS.getTexture("goomba_run"), 16, 300f));
                    animationsGoomba.Add("die", new Animation(textureRS.getTexture("goomba_die"), 16, 300f));
                    //animationsGoomba.Add("run", new Animation(content.Load<Texture2D>(@"Textures/goomba_run"), 16, 300f));
                    //animationsGoomba.Add("die", new Animation(content.Load<Texture2D>(@"Textures/goomba_die"), 16, 300f));
                    gameObject = new Goomba(position, animationsGoomba);

                    break;

                case Enemies.KOOPA:
                    Dictionary<string, Animation> animationsKoopa = new Dictionary<string, Animation>();
                    animationsKoopa.Add("run", new Animation(textureRS.getTexture("koopa_run"), 16, 300f));
                    animationsKoopa.Add("shell_in", new Animation(textureRS.getTexture("koopa_shell_in"), 16, 300f));
                    animationsKoopa.Add("shell_out", new Animation(textureRS.getTexture("koopa_shell_out"), 16, 300f));
                    //animationsKoopa.Add("run", new Animation(content.Load<Texture2D>(@"Textures/koopa_run"), 16, 300f));
                    //animationsKoopa.Add("shell_in", new Animation(content.Load<Texture2D>(@"Textures/koopa_shell_in"), 16, 300f));
                    //animationsKoopa.Add("shell_out", new Animation(content.Load<Texture2D>(@"Textures/koopa_shell_out"), 16, 300f));
                    gameObject = new Koopa(position, animationsKoopa);

                    break;
            
            }

            return gameObject;
        }
    }
}
