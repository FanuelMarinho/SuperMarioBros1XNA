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

        public World(TileMap tileMap, List<GameObject> enemies, List<Block> blocks)
        {
            this.enemies = enemies;
            this.tileMap = tileMap;
            this.blocks = blocks; 
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
                enemie.Update(gameTime);

            }

            foreach (Block block in blocks)
            {
                block.Update(gameTime);
            }

        }

        private void enemyCollision(GameObject enemy, GameTime gameTime)
        {
            if((enemy.HitBox.Intersects(player.HitBox)) && (!enemy.dead))
            {
                if(player.HitBox.Top < enemy.HitBox.Top)
                {
                    enemy.onHit(player);
                }
                else 
                {
                    if(enemy is Koopa)
                    {
                        Koopa koopa = (Koopa)enemy;
                        if (koopa.insideShell)
                        {
                            //sei la, depois eu vejo
                        }
                        else 
                        {
                            player.kill(gameTime);
                        }
                    }
                    
                }

            }
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
        }
    }
}
