using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioBros1XNA
{
    public class World
    {
        private TileMap tileMap;
        private List<GameObject> enemies;
        private List<Block> blocks;
        private Mario player;
        private Vector2 marioSpawnLocation;

        public World(TileMap tileMap, List<GameObject> enemies, List<Block> blocks, Vector2 marioLocation)
        {
            this.enemies = enemies;
            this.tileMap = tileMap;
            this.blocks = blocks;
            this.marioSpawnLocation = marioLocation;
        }

        public void setMarioLocation(Vector2 vect)
        {
            marioSpawnLocation = vect;
        }

        public int getTileAtPosition(int x, int y)
        {
            return tileMap.getTileSourceRectangle(x, y);
        }

        public int getTileAtPosition(Vector2 position)
        {
            return getTileAtPosition((int)position.X, (int)position.Y);
        }

        public void Update(GameTime gameTime)
        { 
            foreach(GameObject enemie in enemies)
            {
                enemyCollision(enemie, gameTime);
                enemyCollision2(enemie, gameTime);
                enemie.Update(gameTime);
            }

            foreach (Block block in blocks)
            {
                block.Update(gameTime);
            }

        }

         private void enemyCollision2(GameObject enemy, GameTime gameTime)
         {
             foreach (GameObject enemy2 in enemies)
             {
                 if (enemy2 == enemy)
                     continue;
                 if (enemy.HitBox.Intersects(enemy2.HitBox) && (!enemy2.dead))
                 {
                     enemy2.onHit(enemy, gameTime);
                 }
             }
         }

        private void enemyCollision(GameObject enemy, GameTime gameTime)
        {
            if ((enemy.HitBox.Intersects(player.HitBox)) && (!enemy.dead) && (!player.dead))
            {
                if(player.HitBox.Top < enemy.HitBox.Top)
                {
                    player.jump(-12000, gameTime);
                    enemy.onHit(player, gameTime);
                }
                else 
                {
                    if(enemy is Koopa)
                    {
                        Koopa koopa = (Koopa)enemy;
                        if (koopa.insideShell)
                        {
                            //sei la, depois eu vejo
                            player.kill(gameTime);
                        }
                        else 
                        {
                            player.kill(gameTime);
                        }
                    }
                    
                }

            }
        }

        private bool intersectVecRect(Vector2 vect, Rectangle rect)
        {
            return (vect.X >= rect.X) && (vect.Y >= rect.Y) && (vect.X <= rect.Right) && (vect.Y <= rect.Bottom);
        }

        public bool BlockCollision(Vector2 position, GameObject gameObject)
        {
            bool collision = false;
            
            foreach (Block block in blocks)
            {
                if(intersectVecRect(position, block.HitBox()))
                {
                    collision = true;
                    break;
                }
            }

            return collision;
        }


        public bool BlockCollision(Rectangle gameObjectRect, GameObject gameObject)
        {
            bool collision = false;
            foreach (Block block in blocks)
            {
                if (block.HitBox().Intersects(gameObjectRect)) 
                {
                    collision = true;
                    if(gameObject is Mario)
                    {
                        //Pretty bug, but will be use for now
                        if( (gameObject.VelocityY < 0)  && (block.HitBox().Bottom >= gameObject.HitBox.Top) )
                        {
                            block.onHit((Mario) gameObject);
                        }
                    }                    
                    break;
                }                
            }
            return collision;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            tileMap.Draw(spriteBatch);
            foreach (GameObject enemie in enemies)
            {
                enemie.Draw(spriteBatch);
            }

            foreach(Block block in blocks)
            {
                block.Draw(spriteBatch);
            }

        }

        public void setPlayer(Mario player)
        {
            this.player = player;
            if(marioSpawnLocation != null)
            {
                this.player.Position = marioSpawnLocation;
            }
        }
    }
}
