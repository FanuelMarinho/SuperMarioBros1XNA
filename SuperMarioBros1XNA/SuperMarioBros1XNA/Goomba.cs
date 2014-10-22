using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SuperMarioBros1XNA
{
    public class Goomba  : GameObject
    {
        private Texture2D tempTexture;
        private Dictionary<String, Animation> animations;
        private float direction = 400.0f;
        private float maxSpeed = 60.0f;
        private string currentAnimation;
        //private Vector2 position;

        public Goomba(Vector2 position, Dictionary<string, Animation> animations)
        {
            this.position = position;
            this.animations = animations;
            currentAnimation = "run";
        }

        public Goomba(Texture2D tempTexture, Vector2 position, ContentManager content)
        {
            animations = new Dictionary<string, Animation>();
            animations.Add("run", new Animation(content.Load<Texture2D>(@"Textures/goomba_run"), 16, 300f));
            animations.Add("die", new Animation(content.Load<Texture2D>(@"Textures/goomba_die"), 16, 300f));

            this.tempTexture = tempTexture;
            this.position = position;
            this.currentAnimation = "run";
        }

        public override void onHit(GameObject gameObject)
        {
            if(gameObject is Mario)
            {
                this.dead = true;
                currentAnimation = "die";
            }
            else if(gameObject is Koopa)
            {
                //TODO : fazer o goomba voar muito loko de ponta cabeça
            }
        }

        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
            if (!this.dead)
            {
                if (Math.Abs(this.VelocityX) < Math.Abs(maxSpeed))
                {
                    this.VelocityX += direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                Physics.applyPhysics(this, gameTime);

                if (this.VelocityX == 0.0f)
                {
                    this.direction *= -1;
                }

                currentAnimation = "run";
            }
            else 
            {
                currentAnimation = "die";
            }

            animations[currentAnimation].Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Provisorio para teste
            Rectangle rect = new Rectangle((int)CameraPosition.X, (int)CameraPosition.Y, (int)(16 * 1), (int)(16 * 1));
            spriteBatch.Draw(animations[currentAnimation].Texture, rect,
                animations[currentAnimation].CurrentFrameRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

            //spriteBatch.Draw(this.tempTexture, this.CameraPosition, Color.White);
        }

        public override int Width
        {
            get { return this.animations[currentAnimation].Width; }
        }

        public override int Height
        {
            get { return this.animations[currentAnimation].Texture.Height; }
        }

        public override Rectangle DrawHitBox
        {
          get 
            {

                return new Rectangle((int)this.CameraPosition.X + 3, (int)this.CameraPosition.Y + 6, Width - 6, Height - 6);
            } 
        }

        public override Rectangle HitBox
        {
            get 
            {
                
                return new Rectangle((int)this.position.X + 3, (int)this.position.Y + 6, Width - 6, Height - 6);
            }
        }


    }
}
