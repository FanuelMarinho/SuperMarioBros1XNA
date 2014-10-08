using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SuperMarioBros1XNA
{
    public class TileMap
    {
        private int width;
        private int height;
        private int layers = 3; 
        private int tileWidth;
        private int tileHeight;

        private ushort[,,] Tiles;

        //private static const int BACKGROUND = 0;
        //private static const int SCENARIO = 1;
        //private static const int INTERACTIVE = 2;

        private Texture2D tileMapTexture;
        
        public TileMap(int Width, int Height, int TileBase)
        {
            this.width = Width;
            this.height = Height;
            this.tileWidth = TileBase;
            this.tileHeight = TileBase;
            this.tileMapTexture = ServiceLocator.TextureService().getTexture("TileMap");
            Tiles = new ushort[width, height, layers];
            clearMap();
        }

        public TileMap(int Width, int Height, int TileWidth, int TileHeight, Texture2D mapTexture) 
        {
            this.width  = Width;
            this.height = Height;
            this.tileWidth  = TileWidth;
            this.tileHeight = TileHeight;

            Tiles = new ushort[width, height, layers];
            this.tileMapTexture = mapTexture;
            clearMap();
        }

        private void clearMap()
        {
            for (int x = 0; x < width - 1; x++) 
            {
                for (int y = 0; y < height - 1; y++) 
                {
                    for (int z = 0; z < layers; z++) 
                    {
                        setTileAt(x, y, z, 0);
                    }
                }
            }
        }

        private Rectangle getTileSourceRectangle(int tileValue)
        {
            int XValue = (tileValue % 33) - 1;
            int YValue = (tileValue / 33);
            Rectangle tileRectangle = new Rectangle(XValue * tileWidth, YValue * tileWidth, tileWidth, tileWidth);
            return tileRectangle;
        }

        public int getTileSourceRectangle(int x, int y)
        {
            int xValue = x / tileWidth;
            int yValue = y / tileHeight;

            //if((xValue >= width) || (yValue >= height) || (yValue < 0))
            if (!inRange(xValue, yValue))
            {
                return 0;
            }
            return Tiles[xValue, yValue, 2];
        }


        private Rectangle generateRectangleTile(int x, int y)
        {
            Vector2 newVec = Camera.TransformToCameraPosition(new Vector2(x * tileWidth, y * tileWidth));
            Rectangle newRect = new Rectangle((int)(newVec.X), (int)(newVec.Y), (int)(tileWidth), (int)(tileWidth));
            return newRect;
        }        

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 firstPosition = Camera.position;

            for (int y = (int)firstPosition.Y / tileWidth; y <= (int) height - 1; y++)
            {
                //TODO: Consertar erro, quando chega no fim da tela da erro pois excedo o indice em X do array. o Clamp do limite deve ser diferente
                for (int x = (int)firstPosition.X / tileWidth; x <= (int)((firstPosition.X + Camera.cameraViewWidth) / tileWidth); x++)
                {
                    for (int z = 0; z <= 2; z++) 
                    {
                        Rectangle newRect = generateRectangleTile(x, y);
                        if(inRange(x, y))
                        if (Tiles[x, y, z] != 0) 
                        {
                            spriteBatch.Draw(tileMapTexture, newRect, getTileSourceRectangle(Tiles[x, y, z]), Color.White);
                        }
                    }
                }
            }
        }

        private bool inRange(int x, int y) 
        {
            return ((x < width) && (y < height) && (y >= 0) && (x >= 0));
        }

        public void setTileAt(int x, int y, int z, ushort value)
        {
            this.Tiles[x, y, z] = value;
        }

    
    }
    
}
