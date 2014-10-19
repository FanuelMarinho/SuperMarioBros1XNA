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
        private Player player;

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
                enemie.Update(gameTime);
            }

            foreach (Block block in blocks)
            {
                block.Update(gameTime);
            }

        }

        public bool BlockCollision(Rectangle player)
        {
            bool collision = false;
            foreach (Block block in blocks)
            {
                if (block.HitBox().Intersects(player)) 
                {
                    collision = true;
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

        public void setPlayer(Player player)
        {
            this.player = player;
        }
    }
}
