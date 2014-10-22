using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SuperMarioBros1XNA
{
    public class Koopa : GameObject
    {
        private Dictionary<String, Animation> animations;
        private float direction = 400.0f;
        private float maxSpeed = 60.0f;
        private string currentAnimation;
        public bool insideShell;
        private SpriteEffects effect = SpriteEffects.FlipHorizontally;

        public Koopa(Vector2 position, Dictionary<string, Animation> animations)
        {
            this.position = position;
            this.animations = animations;
            this.currentAnimation = "run";
            insideShell = false;
        }

        public Koopa(Vector2 position,ContentManager content)
        {
            animations = new Dictionary<string, Animation>();
            animations.Add("run", new Animation(content.Load<Texture2D>(@"Textures/koopa_run"), 16, 300f));
            animations.Add("shell_in", new Animation(content.Load<Texture2D>(@"Textures/koopa_shell_in"), 16, 300f));
            animations.Add("shell_out", new Animation(content.Load<Texture2D>(@"Textures/koopa_shell_out"), 16, 300f));

            this.position = position;
            this.currentAnimation = "run";
        }

        public override void onHit(GameObject gameObject)
        {
            if(gameObject is Mario)
            {
                if(this.insideShell)
                {
                    //TODO: empurra o casco para a direção que o Mario esta indo
                }
                else 
                {
                    this.insideShell = true;
                    currentAnimation = "shell_in";
                }                
            }            
            if(gameObject is Koopa)
            {
                //faz ele voar muito loko tambem
            }
            
        }


        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (!this.dead)
            {
                if(!this.insideShell)
                {
                    if (Math.Abs(this.VelocityX) < Math.Abs(maxSpeed))
                    {
                        this.VelocityX += direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    Physics.applyPhysics(this, gameTime);

                    if (this.VelocityX == 0.0f)
                    {
                        this.direction *= -1;

                        if (effect == SpriteEffects.None)
                        {
                            effect = SpriteEffects.FlipHorizontally;
                        }
                        else
                        {
                            effect = SpriteEffects.None;
                        }
                    }

                    if (!this.insideShell)
                    {
                        currentAnimation = "run";
                    }

                }

            }
            else
            {
                 //currentAnimation = "die";
            }

            animations[currentAnimation].Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Rectangle rect = new Rectangle((int)CameraPosition.X, (int)CameraPosition.Y, (int)(16 * 1), (int)(24 * 1));

            spriteBatch.Draw(animations[currentAnimation].Texture, rect, animations[currentAnimation].CurrentFrameRectangle, Color.White, 0, Vector2.Zero, effect, 0);
        }

        public override int Width
        {
            get { return this.animations[currentAnimation].Width ; }
        }

        public override int Height
        {
            get { return this.animations[currentAnimation].Texture.Height; }
        }

        public override Rectangle DrawHitBox
        {
            get {  return new Rectangle((int)this.CameraPosition.X + 4, (int)this.CameraPosition.Y + 10, Width - 6, Height - 10); }
        }

        public override Rectangle HitBox
        {
            get { return new Rectangle((int)       this.position.X + 4,       (int)this.position.Y + 10, Width - 6, Height - 10); }
        }
    }
}
