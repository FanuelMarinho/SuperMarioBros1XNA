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
        private List<object> blocks;

        public World(TileMap tileMap, List<GameObject> enemies)
        {
            this.enemies = enemies;
            this.tileMap = tileMap;
            blocks = new List<object>();
        }

        public void Update(GameTime gameTime)
        { 
            foreach(GameObject enemie in enemies)
            {
                enemie.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject enemie in enemies)
            {
                enemie.Draw(spriteBatch);
            }
            tileMap.Draw(spriteBatch);
        }
    }
}
