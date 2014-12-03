using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioBros1XNA
{
   /*
    * Ainda não sei o que fazer com essa porra, quem sabe ainda tenho alguma ideia
    * <animation>
    <frame tileid="26" duration="200"/>
    <frame tileid="25" duration="200"/>
    <frame tileid="24" duration="300"/>
    <frame tileid="25" duration="200"/>
   </animation> */



    public class QuestionCoinBlock : Block
    {
        private int coins;
        private int currentIndex = 0;
        private ushort emptyGid;
        private TiledAnimation[] tilesAnimation;
        private float currentDuration;
        private Vector2 originalPosition;

        public QuestionCoinBlock(ushort gid,Rectangle hitBox , int coins)
        {
            this.hitBox = hitBox;
            this.baseGid = gid;
            this.coins = coins;

            this.tilesAnimation = new TiledAnimation[4];
            
            //Provisorio
            this.tilesAnimation[0] = new TiledAnimation(26, 200);
            this.tilesAnimation[1] = new TiledAnimation(25, 200);
            this.tilesAnimation[2] = new TiledAnimation(24, 300);
            this.tilesAnimation[3] = new TiledAnimation(25, 200);
            this.emptyGid = gid;
            this.baseGid = tilesAnimation[0].Gid;
            this.currentDuration = tilesAnimation[0].Duration;
            this.originalPosition = new Vector2(hitBox.X, hitBox.Y);

        }

        public override void Update(GameTime gameTime)
        {
            this.currentDuration -= (float)gameTime.ElapsedGameTime.Milliseconds;
            if(this.currentDuration <= 0)
            {
                currentIndex++;
                if (currentIndex > 3) 
                {
                    currentIndex = 0;
                }
                this.baseGid = this.tilesAnimation[currentIndex].Gid;
                this.currentDuration = this.tilesAnimation[currentIndex].Duration;
            }

            if (this.hitBox.Y != this.originalPosition.Y)
            {
                //this.hitBox.Y = (int)this.originalPosition.Y;
                this.hitBox.Y += 1;
            }
        }

        public override void onHit(Mario player)
        {
            this.hitBox.Offset(0, -7);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tileTexture = ServiceLocator.TextureService().getTexture("TileMap");
            Rectangle sourceRect = TileUtils.getTileSourceRectangle(this.baseGid + 1);
            spriteBatch.Draw(tileTexture, CameraPosition, sourceRect, Color.White);
        }
    }
}
