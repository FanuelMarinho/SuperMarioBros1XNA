using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioBros1XNA
{
    /*
     NOT THE BEST DECISION:
     * HOW CAN A MUSHROOM DIE?
     * I'LL FIX THAT LATER, I'M NEW IN THIS SHIT
     */
    public class Mushroom : GameObject
    {
        private Texture2D texture;
        private Vector2 position;

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void onHit(GameObject gameObject, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override int Width
        {
            get { throw new NotImplementedException(); }
        }

        public override int Height
        {
            get { throw new NotImplementedException(); }
        }

        public override Rectangle DrawHitBox
        {
            get { throw new NotImplementedException(); }
        }

        public override Rectangle HitBox
        {
            get { throw new NotImplementedException(); }
        }
    }
}
