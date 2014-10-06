using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SuperMarioBros1XNA
{
    public class Player2 :IGameObject
    {
        private Dictionary<String, Animation> animations;
        private String currentAnimation = "idle";
        private SpriteEffects spriteEffect;
        public bool dead = false;
        private Vector2 position = Vector2.Zero;
        private Vector2 velocity = Vector2.Zero;
        public bool onGround = false;

        public Player2(Vector2 position, ContentManager content)
        {

            //ANIMATION SETUP
            animations = new Dictionary<string, Animation>();
            animations.Add("idle", new Animation(content.Load<Texture2D>(@"Textures/mario_idle"), 16, 70));
            animations.Add("die", new Animation(content.Load<Texture2D>(@"Textures/mario_die"), 16, 70));
            animations.Add("jump", new Animation(content.Load<Texture2D>(@"Textures/mario_jump"), 16, 70));
            animations.Add("run", new Animation(content.Load<Texture2D>(@"Textures/mario_run2"), 16, 70));   
            this.spriteEffect = SpriteEffects.None;
            this.velocity = new Vector2(0, 20);
            this.position = position;
             
        }

        
        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            this.velocity = new Vector2(0, velocity.Y);

            if (keyboard.IsKeyDown(Keys.Left))
            {
                this.velocity.X = -6100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                currentAnimation = "run";
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                this.velocity.X = 6100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                currentAnimation = "run";
                spriteEffect = SpriteEffects.None;
            }
            else
            {
                currentAnimation = "idle";
            }


            if (keyboard.IsKeyDown(Keys.Space) && (onGround))
            {
                this.velocity.Y = -17000 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                SoundPlayer.Instance.playSound(SoundPlayer.JUMP_SMALL);
            }

            //Physics.collision(this, gameTime);
            //SimplePhysics.collision2(this, gameTime);

            if (!onGround)
            {
                currentAnimation = "jump";
            }


            if (dead)
            {
                currentAnimation = "die";
            }

            //animations[currentAnimation].Update(gameTime, this.ve);
        }

        public bool isOnGround()
        {
            return this.onGround;
        }

        public void setOnGround(bool grounded) 
        {
            this.onGround = grounded;
        }

        public void kill(GameTime gameTime)
        {
            this.dead = true;
            this.velocity.Y = -15000 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public bool isPlayerDead()
        {
            return this.dead;
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Vector2 camPosition = getCameraPosition();
            Rectangle rect = new Rectangle((int)camPosition.X, (int) camPosition.Y, (int)(16 * 1), (int)(16 * 1));

            spriteBatch.Draw(animations[currentAnimation].Texture, rect,
                animations[currentAnimation].CurrentFrameRectangle, Color.White, 0, Vector2.Zero, spriteEffect, 0);
        }

        public void setVelocity(Vector2 newVel)
        {
            this.velocity = newVel;
        }

        public Vector2 getVelocity()
        {
            return this.velocity;
        }

        public Vector2 getCameraPosition()
        {
            return Camera.TransformToCameraPosition(this.position);
        }

        public void setPosition(Vector2 newPosition)
        {
            this.position = newPosition;
        }

        public Vector2 getPosition()
        {
            return this.position;
        }

        public int getWidth()
        {
            return this.animations[currentAnimation].Texture.Width;
        }

        public int getHeight()
        {
            return this.animations[currentAnimation].Texture.Height;
        }

        public Rectangle getRectangleBounds()
        {
            float offset = 0.0f;
            return new Rectangle((int)(this.position.X + offset), (int)(this.position.Y + offset), (int)(this.getWidth() - offset), (int)(this.getHeight() - offset)); 
        }
    }
}
