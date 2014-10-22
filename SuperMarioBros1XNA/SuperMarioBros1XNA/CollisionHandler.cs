using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioBros1XNA
{
    public static class CollisionHandler
    {
        public static void collisionBetweenTwo(Mario player, GameObject gameObject2, GameTime gameTime)
        {
            bool someoneIsDead = player.isPlayerDead() || gameObject2.dead;
            if (player.HitBox.Intersects(gameObject2.HitBox) && (!someoneIsDead))
            {
                //Considerin the cartesian plan starts(0,0) at left, top
                if (player.HitBox.Bottom > gameObject2.HitBox.Center.Y)
                {
                    player.kill(gameTime);
                }
                else 
                {
                    //gameObject2.
                }

            }
            //gameObject1.HitBox.In
        }
    }
}
