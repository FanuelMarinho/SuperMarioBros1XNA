using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// player.VelocityY = player.VelocityY * 0.1f;
namespace SuperMarioBros1XNA
{
    public static class SimplePhysics
    {
       // public static GameObject otherObject;
        //public static TileMap2 tileMap;
        public static Level level;
        public static Vector2 gravity = new Vector2(0, 30);
        public static Vector2 gravity2 = new Vector2(0, 560);

        public static void Init(Level lvl)
        {
            level = lvl;
        }

        private static Vector2 amoutMovement(GameTime gameTime, GameObject gameObject) 
        {
            float  elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            return gameObject.Velocity * elapsed; 
        }

        #region Public Methods for GameObjects(Testing , not final)
        public static void collision(Mario player,  GameTime gameTime) 
        {
           // player.
            player.Velocity += gravity;//* (float)gameTime.ElapsedGameTime.TotalSeconds; 
            //verticalCollision(player, gameTime);
            horizontalCollision(player, gameTime);
            player.Position += amoutMovement(gameTime, player);
        }


        public static void collision2(Mario player, GameTime gameTime)
        {
            if (!player.isPlayerDead())
            {
                Vector2 gravityStep = gravity2 * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Se a velocidade de Y ficar muito baixa, não sera suficiente para que o sistema entenda
                // Que o personagem está no chão, pois o valor será muito pequeno

                if ((player.VelocityY == 0) && (gravityStep.Y < 10))
                    gravityStep.Y += 47.7f;

                player.Velocity += gravityStep;
                AxisYCollision(player, gameTime);
                AxisXCollision(player, gameTime);

                player.VelocityY = MathHelper.Clamp(player.VelocityY, player.VelocityY, 270.0f);

                player.Position += player.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else 
            {
                Vector2 gravityStep = gravity2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if ((player.VelocityY == 0) && (gravityStep.Y < 10))
                    gravityStep.Y += 47.7f;

                player.Velocity += gravityStep;
                player.Position += player.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }




        private static void AxisXCollision(GameObject player, GameTime gameTime) 
        {
            Rectangle newRect = player.HitBox;

            newRect.Offset( (int)(player.VelocityX * (float) gameTime.ElapsedGameTime.TotalSeconds), 0);

            Vector2 pos1;
            Vector2 pos2;

            if (player.VelocityX > 0)
            {
                if (!player.onGround)
                {
                    pos1 = new Vector2(newRect.Right, newRect.Bottom);
                }
                else 
                {
                    pos1 = new Vector2(newRect.Right , newRect.Center.Y);
                }

                pos2 = new Vector2(newRect.Right , newRect.Y );
            }
            else 
            {
                if (!player.onGround)
                {
                    pos1 = new Vector2(newRect.X - 1, newRect.Bottom);
                }
                else
                {
                    pos1 = new Vector2(newRect.X - 1, newRect.Center.Y);
                }

                //pos1 = new Vector2(newRect.Left , newRect.Center.Y );
                //pos1 = new Vector2(newRect.Left , newRect.Bottom);
                pos2 = new Vector2(newRect.X - 1, newRect.Y);
            }

            int tile1 = level.getTileAtPosition(pos1);
            int tile2 = level.getTileAtPosition(pos2);

            //Debug.Write("\nX : " + player.VelocityX);
            if ((tile1 != 0) || (tile2 != 0))
            {
                //Debug.Write("\nX Will be Zero: " + player.VelocityX);
                player.VelocityX = 0;
                //player.Position = new Vector2(player.Position.X, player.Position.Y + 4);
            }
            


        }

        private static void AxisYCollision(GameObject player, GameTime gameTime) 
        {
            Rectangle newRect = player.HitBox;
            
            newRect.Offset(0, (int)(player.VelocityY * (float) gameTime.ElapsedGameTime.TotalSeconds));

            Vector2 pos1;
            Vector2 pos2;

            //Debug.Write("\n Velocity Y : " + player.VelocityY);
           
            if (player.VelocityY > 0)
            {
                pos1 = new Vector2(newRect.X, newRect.Bottom);
                pos2 = new Vector2(newRect.Right, newRect.Bottom);
            }
            else 
            {
                pos1 = new Vector2(newRect.X, newRect.Top);
                pos2 = new Vector2(newRect.Right, newRect.Top);                
            }
            
            int tile1 = level.getTileAtPosition(pos1);
            int tile2 = level.getTileAtPosition(pos2);

            if ((tile1 != 0) || (tile2 != 0))
            {
                if (player.VelocityY > 0) 
                {
                    player.onGround = true;
                }

                //ebug.Write("\nY Will be Zero: " + player.VelocityY);
                player.VelocityY = 0;
                //player.Position = new Vector2(player.Position.X, player.Position.Y + 4);
            }
            else
            {
                if( !((player.VelocityY > 9) && (player.VelocityY < 60)) )
                    player.onGround = false;
            }
        }

        private static void horizontalCollision(Mario player, GameTime gameTime) 
        {
            if (player.VelocityX > 0)
            {
                Vector2 newPosition = player.Position + amoutMovement(gameTime, player);
                Vector2 pos1 = new Vector2(newPosition.X + player.Width , player.Position.Y);
                Vector2 pos2 = new Vector2(newPosition.X + player.Width, player.Position.Y + player.Height);
                
                int tile1 = level.getTileAtPosition(pos1);
                int tile2 = level.getTileAtPosition(pos2);

                if ((tile1 != 0) || (tile2 != 0)) 
                {
                    player.VelocityX = 0;
                }

            }
            else 
            {
                Vector2 newPosition = player.Position + amoutMovement(gameTime, player);
                Vector2 pos1 = new Vector2(newPosition.X , player.Position.Y);
                Vector2 pos2 = new Vector2(newPosition.X , player.Position.Y + player.Height);

                int tile1 = level.getTileAtPosition(pos1);
                int tile2 = level.getTileAtPosition(pos2);

                if ((tile1 != 0) || (tile2 != 0))
                {
                    player.VelocityX = 0;
                }
            }
        }
        #endregion
    }
}
