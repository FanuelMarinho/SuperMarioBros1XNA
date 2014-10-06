using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperMarioBros1XNA
{
    public static class Camera
    {
        #region  Dimensions constants
        public  static int cameraViewWidth;
        public  static int cameraViewHeight;
        private static int worldViewWidth;
        private static int worldViewHeight;
        #endregion
        
        public  static Vector2 position;
        private static Vector2 moveSpeed = new Vector2(140, 140);

        private static GameObject objectToFollow = null;

        //private static Sprite spriteToFollow;

        public static void Initialize(Vector2 pos, int cameraWidth, int cameraHeight, int worldWidth, int worldHeight)
        {
            position = pos;
            cameraViewHeight = cameraHeight;
            cameraViewWidth = cameraWidth;
            worldViewHeight = worldHeight;
            worldViewWidth = worldWidth;
        }

        public static void setObjectToFollow(GameObject gameObject)
        {
            objectToFollow = gameObject;
        }

        public static void SetCameraPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        private static void followObject()
        {
            //If the followed object reach the camera center, so start to follow it
            if (objectToFollow.CameraPosition.X > CameraWidthCenter)
            {
                Vector2 newPosition = new Vector2(objectToFollow.CameraPosition.X - CameraWidthCenter, position.Y);
                moveCamera(newPosition);
            }


        }

        public static float CameraWidthCenter
        {
            get { return cameraViewWidth / 2; }
        }

        public static float WorldHeight
        {
            get { return worldViewHeight; }
        }

        public static Vector2 TransformToCameraPosition(Vector2 pos)
        {
            return pos - position;
        }

        public static void moveCamera(Vector2 movement)
        {
            position += movement;
            position.X = MathHelper.Clamp(position.X, 0, worldViewWidth - cameraViewWidth);
            position.Y = MathHelper.Clamp(position.Y, 0, worldViewHeight - cameraViewHeight);
        }


        public static void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //moveCameraWithKeyBoard(elapsed);                    
            followObject();
        }

        #region Debug Methods
        private static void moveCameraWithKeyBoard(float elapsedSeconds)
        {
            Vector2 velocity = Vector2.Zero;
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity.X = -moveSpeed.X;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                velocity.X = moveSpeed.X;
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                velocity.Y = -moveSpeed.Y;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                velocity.Y = moveSpeed.Y;
            }

            moveCamera(velocity * elapsedSeconds); 
        }    
        #endregion

    }
}
