using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioBros1XNA
{
    public static class Physics
    {
        //public static Level level;
        public static World world;

        // Only used for Y coordinate
        public static float gravity = 1100.0f;
        public static float friction = 400.0f;


        public static void Init(World w)
        {
            world = w;
        }

        public static void applyPhysics(GameObject gameObject, GameTime gameTime) 
        {
            float gravityApply = gravity * (float) gameTime.ElapsedGameTime.TotalSeconds;
            gameObject.VelocityY += gravityApply;

            horizontalCollision(gameObject, gameTime);
            verticalCollision(gameObject, gameTime);
            
            gameObject.Position += gameObject.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;  
      
        }

        private static void horizontalCollision(GameObject gameObject, GameTime gameTime)
        {
            Rectangle rectObject = gameObject.HitBox;
            rectObject.Offset((int)(gameObject.VelocityX * (float)gameTime.ElapsedGameTime.TotalSeconds), 0);

            Vector2 pos1;
            Vector2 pos2;

            if(gameObject.VelocityX > 0)
            {
                pos1 = new Vector2(rectObject.Right + 1, rectObject.Top);
                pos2 = new Vector2(rectObject.Right + 1, rectObject.Bottom - 1 );    
            }
            else
            {
                pos1 = new Vector2(rectObject.Left - 1, rectObject.Top );
                pos2 = new Vector2(rectObject.Left - 1, rectObject.Bottom - 1);   
            }
            
            int tile1 = world.getTileAtPosition(pos1);
            int tile2 = world.getTileAtPosition(pos2);
            bool collide = world.BlockCollision(rectObject, gameObject);

            if ((tile1 != 0) || (tile2 != 0) || (collide))
            {
               gameObject.VelocityX = 0; 
            }
        }

        private static void verticalCollision(GameObject gameObject, GameTime gameTime)
        {
            Rectangle rectObject = gameObject.HitBox;
            rectObject.Offset(0, (int)(gameObject.VelocityY * (float)gameTime.ElapsedGameTime.TotalSeconds));

            Vector2 pos1; 
            Vector2 pos2; 

            if (gameObject.VelocityY < 0)
            {
                pos1 = new Vector2(rectObject.Left, rectObject.Top);
                pos2 = new Vector2(rectObject.Right, rectObject.Top); 
            }
            else
            {
                pos1 = new Vector2(rectObject.Left, rectObject.Bottom);
                pos2 = new Vector2(rectObject.Right, rectObject.Bottom);
            }

            int tile1 = world.getTileAtPosition(pos1);
            int tile2 = world.getTileAtPosition(pos2);

            bool collide = world.BlockCollision(rectObject, gameObject);
            if ((tile1 != 0) || (tile2 != 0) || (collide))
            {
                if (gameObject.VelocityY > 0)
                {
                    gameObject.onGround = true;
                }
                gameObject.VelocityY = 0;

                
            }
            else 
            {
                gameObject.onGround = false;
            }

        }

       
    }
}
