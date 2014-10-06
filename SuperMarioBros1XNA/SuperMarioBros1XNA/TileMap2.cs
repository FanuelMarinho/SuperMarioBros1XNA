using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioBros1XNA
{
    public class TileMap2
    {

        private Texture2D tileMapTexture;
        public const int MAP_WIDTH = 212;
        //public const int MAP_WIDTH = 612;
        public const int MAP_HEIGHT = 12;
        public const int TileBaseSize = 32;
        private int scale = 1;
        
        private int[,] mapTiles;

        public TileMap2(Texture2D tileMapTexture)
        {
            this.tileMapTexture = tileMapTexture;
            this.mapTiles = new int[MAP_WIDTH, MAP_HEIGHT];
            setTileTypeTest();
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y <= MAP_HEIGHT - 1; y++)
            {
                for (int x = 0; x <= MAP_WIDTH - 1; x++)
                {
                    Rectangle newRect = generateRectangleTile(x, y);
                    spriteBatch.Draw(tileMapTexture, newRect, getTileSourceRectangle(mapTiles[x, y]), Color.White);
                }
            }
        }


        public void Draw2(SpriteBatch spriteBatch)
        {
            Vector2 firstPosition = Camera.position;
            //int x = (int) firstPosition.X / TileBaseSize;
            //int y = (int) firstPosition.Y / TileBaseSize;

            //int lastX = (int)firstPosition.X + Camera.cameraViewWidth / TileBaseSize;
            //int lastY = (int)firstPosition.Y + Camera.cameraViewHeight / TileBaseSize;


            //for (int y = (int)firstPosition.Y / TileBaseSize; y <= (int)(firstPosition.Y + Camera.cameraViewHeight) / TileBaseSize ; y++)
            for (int y = (int)firstPosition.Y / TileBaseSize; y <= (int)MAP_HEIGHT - 1; y++)
            {
                for (int x = (int)firstPosition.X / TileBaseSize; x <= (int)(firstPosition.X + Camera.cameraViewWidth) / TileBaseSize  ; x++)
                {
                    //if (x < MAP_WIDTH) 
                    //{
                        Rectangle newRect = generateRectangleTile(x, y);
                        spriteBatch.Draw(tileMapTexture, newRect, getTileSourceRectangle(mapTiles[x, y]), Color.White);
                    //}
                }
            }
        }

        #region Helper Methods
        public int getTileSourceRectangle(int x, int y) 
        {
            int xValue = x / TileBaseSize;
            int yValue = y / TileBaseSize;

            return mapTiles[xValue, yValue];
        }

        private Rectangle generateRectangleTile(int x, int y) 
        {
            Vector2 newVec = Camera.TransformToCameraPosition(new Vector2(x * TileBaseSize, y * TileBaseSize));
            Rectangle newRect = new Rectangle((int)(newVec.X), (int)(newVec.Y ), (int)(TileBaseSize ), (int)(TileBaseSize ));
            return newRect;
        }        
        
        private Rectangle getTileSourceRectangle(int tileValue)
        {
            int XValue = tileValue % 32;
            int YValue = tileValue / 32;
            Rectangle tileRectangle = new Rectangle(XValue * TileBaseSize, YValue * TileBaseSize, TileBaseSize, TileBaseSize);
            return tileRectangle;
        }
        
        public void setTileAtIndex(int x, int y, int tileValue)
        {
            this.mapTiles[x, y] = tileValue; 
        }
        #endregion

        #region Methods for Test 
        private void setTileTypeTest()
        {
            for (int x = 0; x <= MAP_WIDTH - 1; x++) 
            {
                for (int y = 0; y <= MAP_HEIGHT - 1; y++)
                {
                    //For ice terrain purposes
                    //if ((y > 9) && (y < 12))
                    if (y > 9)
                    {
                        setTileAtIndex(x, y, 0);
                    }
                    else 
                    {
                        setTileAtIndex(x, y, 675);
                    }
                     
                }
            }
        }
        #endregion


    }
}
