using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioBros1XNA
{
    public class Animation
    {
        private float timeForNextAnimation;
        private float animationInterval;

        private int currentFrame;
        private int totalFrames;
        private int frameSize;
        private int width;

        private Texture2D texture;
        public Animation(Texture2D texture, int frameSize, float animationItrv)
        {
            this.texture = texture;
            this.frameSize = frameSize;

            this.totalFrames = texture.Width / frameSize;
            this.currentFrame = 0;
            //this.animationInterval = 70;
            this.animationInterval = animationItrv;
            timeForNextAnimation = this.animationInterval;

        }

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.Milliseconds;
            timeForNextAnimation -= elapsed;

            //float relativeVelocity = Math.Abs(velocityX / maxVelocity);

            if (timeForNextAnimation <= 0)
            {
                timeForNextAnimation = animationInterval;
                currentFrame++;


                //Eh temporario, SEM HURR DURR, por favor
                if (currentFrame >= totalFrames)
                {
                    currentFrame = 0;
                }
            }
        }


        #region Methods for Drawing
        public int Width 
        {
            get { return this.Texture.Width / totalFrames; }
        }

        public Texture2D Texture
        {
            get { return this.texture; }
        }

        public Rectangle CurrentFrameRectangle
        {
            get
            {
                Vector2 position = Vector2.Zero;
                position.X = frameSize * currentFrame;
                return new Rectangle((int)position.X, (int)position.Y, frameSize, Texture.Height);
            }
        }
        #endregion

    }
}
