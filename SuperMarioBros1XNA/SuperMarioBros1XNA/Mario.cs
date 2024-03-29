﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SuperMarioBros1XNA
{
    public class Mario : GameObject
    {
        private Dictionary<String, Animation> animations;
        private String currentAnimation = "idle";
        private SpriteEffects spriteEffect;
        public bool dead = false;

        //Temporario ate eu criar a interfacer de Input, aka InputManager ou qualquer coisa do genero
        private KeyboardState previousKeyBoardState;
        private float maxSpeedX = 6100.0f;
        private float speedX = 400.0f;
        private bool BigMario = false;

        //public bool onGround = false;
        public Mario(Vector2 position, ContentManager content) 
        {

            //ANIMATION SETUP
            animations = new Dictionary<string, Animation>();
            animations.Add("idle", new Animation(content.Load<Texture2D>(@"Textures/mario_idle"), 16, 70));
            animations.Add("die", new Animation(content.Load<Texture2D>(@"Textures/mario_die"), 16, 70));
            animations.Add("jump", new Animation(content.Load<Texture2D>(@"Textures/mario_jump"), 16, 70));
            animations.Add("run", new Animation(content.Load<Texture2D>(@"Textures/mario_run2"), 16, 70));   
            this.spriteEffect = SpriteEffects.None;
            //this.velocity = new Vector2(0, 20);
            this.position = position;
        }

        public void growUp()
        {
            this.BigMario = true;
        }

        public void growDown()
        {
            this.BigMario = false;
        }

        public void kill(GameTime gameTime) 
        {
            this.dead = true;
            this.velocity.Y = -11000 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void onHit(GameObject gameObject, GameTime gameTime)
        {
            this.dead = true;
        }

        public bool isPlayerDead() 
        {
            return this.dead;
        }


        public void jump(float jumpValue, GameTime gameTime)
        {
            this.velocity.Y = jumpValue * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        //DEBUG METHODS
        public void revive()
        {
            this.dead = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            Rectangle rect = new Rectangle((int)CameraPosition.X, (int)CameraPosition.Y, (int)(16 * 1), (int)(16 * 1));
            spriteBatch.Draw(animations[currentAnimation].Texture, rect, 
                animations[currentAnimation].CurrentFrameRectangle, Color.White, 0, Vector2.Zero, spriteEffect, 0);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            this.velocity = new Vector2(this.VelocityX, velocity.Y);

            if (keyboard.IsKeyDown(Keys.Left)) 
            {
                if((this.VelocityX) > -(maxSpeedX * (float)gameTime.ElapsedGameTime.TotalSeconds))
                {
                    this.VelocityX -= speedX * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                //this.velocity.X = -6100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                currentAnimation = "run";
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                if ((this.VelocityX) < (maxSpeedX * (float)gameTime.ElapsedGameTime.TotalSeconds)) 
                {
                    this.VelocityX += speedX * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                //this.velocity.X = 6100 * (float)gameTime.ElapsedGameTime.TotalSeconds;                    
                currentAnimation = "run";
                spriteEffect = SpriteEffects.None;
            }
            else 
            {
                this.VelocityX *= 0.85f;
                if((this.VelocityX < 10) || (this.VelocityX > -10)) 
                {
                    currentAnimation = "idle";
                }
                    
            }


            if (keyboard.IsKeyDown(Keys.Space) && (onGround) && (previousKeyBoardState.IsKeyUp(Keys.Space)) )
            {
                jump(-15000, gameTime);
                SoundPlayer.Instance.playSound(SoundPlayer.JUMP_SMALL);
                //this.velocity.Y = -15000  * (float)gameTime.ElapsedGameTime.TotalSeconds;
                //SoundPlayer.Instance.playSound(SoundPlayer.JUMP_SMALL);
            }
            else if (keyboard.IsKeyDown(Keys.Space) && (!onGround) && (VelocityY < 0))
            {
                this.velocity.Y += -670 * (float)gameTime.ElapsedGameTime.TotalSeconds;    
            }
            

            Physics.applyPhysics(this, gameTime);
    
            if ((!onGround))
            {
                currentAnimation = "jump";
            }

            if (dead)
            {
                currentAnimation = "die";
            }

            animations[currentAnimation].Update(gameTime);

            //base.Update(gameTime); , this.VelocityX, this.maxSpeedX
            previousKeyBoardState = Keyboard.GetState();

        }

        private Rectangle getHitBoxDimensions(Vector2 pos)
        {
            Rectangle rect = new Rectangle((int)pos.X + 3, (int)pos.Y + 4, this.Width - 6, this.Height - 4);
            return rect;
        }

        public override Rectangle DrawHitBox
        {
            get { return getHitBoxDimensions(this.CameraPosition); }
        }

        public override Rectangle HitBox
        {
            get { return getHitBoxDimensions(this.Position); }
        }

        public override int Width
        {
            get { return this.animations[currentAnimation].Texture.Height; }
        }

        public override int Height
        {
            get { return this.animations[currentAnimation].Texture.Height; }
        }

    }
}
