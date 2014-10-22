using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SuperMarioBros1XNA
{
    public struct TiledAnimation
    {
        private ushort gid;
        private float duration;

        public TiledAnimation(ushort gid, float duration)
        {
            this.gid = gid;
            this.duration = duration;
        }

        public ushort Gid
        {
            get { return gid; }
            set { gid = value; }
        }

        public float Duration
        {
            get { return duration; }
            set { duration = value; }
        }

    }

    public abstract class Block
    {
        protected Rectangle hitBox;
        protected ushort baseGid;
        
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
        public abstract void onHit(Mario player);

        public Rectangle HitBox() 
        {
            return hitBox;
        }

        public Vector2 CameraPosition
        {
            get { return Camera.TransformToCameraPosition(new Vector2(this.hitBox.X, this.hitBox.Y)); }
        }

    }
}
