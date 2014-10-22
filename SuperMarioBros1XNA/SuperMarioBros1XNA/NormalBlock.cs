using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioBros1XNA
{

    public class NormalBlock : Block
    {
        private Vector2 originalPosition;
        public NormalBlock(int x, int y, int width, int height, ushort baseGid)
        {
            this.hitBox = new Rectangle(x, y, width, height);
            this.baseGid = baseGid;
            this.originalPosition = new Vector2(x, y);
        }

        public NormalBlock(Rectangle hitBox , ushort baseGid)
        {
            this.hitBox = hitBox;
            this.baseGid = baseGid;
            this.originalPosition = new Vector2(hitBox.X, hitBox.Y);
        }

        public override void onHit(Mario player)
        {
            this.hitBox.Offset(0, -7);
        }

        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
            if(this.hitBox.Y != this.originalPosition.Y)
            {
                //this.hitBox.Y = (int)this.originalPosition.Y;
                this.hitBox.Y += 1;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tileTexture = ServiceLocator.TextureService().getTexture("TileMap");
            Rectangle sourceRect =  TileUtils.getTileSourceRectangle(this.baseGid);
            spriteBatch.Draw(tileTexture, CameraPosition, sourceRect, Color.White);
            
        }
    }
}
