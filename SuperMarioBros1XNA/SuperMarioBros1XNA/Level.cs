using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using System.Text;
using System.Xml;

namespace SuperMarioBros1XNA
{
    public class Level
    {
        //data about the level file
        private const String path = @"Content\Levels\";

        //Tile Class
        private TileMap tileMap;
        private Texture2D tileMapTexture;
        private Rectangle finish_area;
        private bool finish_game = false;

        private Player player;
        private ContentManager content;

        private List<GameObject> enemies;
        
        public Level()
        {

        }

        public Level(Texture2D tileMapTexture, Player player, ContentManager content)
        {
            this.tileMapTexture = tileMapTexture;
            this.player = player;
            this.content = content;
            enemies = new List<GameObject>();
        }

        private void restartLevel()
        {
            player.dead = false;
            player.Position = new Vector2(30, 170);
            
            //player.setPosition(new Vector2(30, 170));
            Camera.SetCameraPosition(new Vector2(0, 0)); 

           // SoundPlayer.Instance.playBackgroundMusic();
        }

        public void Update(GameTime gametime) 
        {

            foreach(GameObject enemy in this.enemies)
            {
                enemy.Update(gametime);
            }
            Rectangle tempRect = player.HitBox;

            if (tempRect.Intersects(this.finish_area) && (!finish_game))
            {
                finish_game = true;
                SoundPlayer.Instance.playSound(SoundPlayer.DIE);
            }

            //Rectangle tempRect = player.getRectangleBounds();
            
            if ((player.Position.Y / 16 > 13) && (!player.isPlayerDead()))
            //if ((player.getPosition().Y / 16 > 13) && (!player.isPlayerDead()))
            {
                SoundPlayer.Instance.stopBackgroundMusic();
                player.kill(gametime);
                SoundPlayer.Instance.playSound(SoundPlayer.DIE);
            }

            if((player.isPlayerDead()) && (player.Position.Y / 120 > 13))
            //if ((player.isPlayerDead()) && (player.getPosition().Y / 120 > 13))
            {
                restartLevel();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            tileMap.Draw(spriteBatch);
            foreach (GameObject enemy in this.enemies)
            {
                enemy.Draw(spriteBatch);
            }


        }

        public int getTileAtPosition(int x, int y)
        {
            return tileMap.getTileSourceRectangle(x, y);
        }

        public int getTileAtPosition(Vector2 position)
        {
            return getTileAtPosition((int)position.X, (int)position.Y);
        }


        //Criar uma class parser que retorna um Tile Map ja preenchido
        public void loadLevel(String levelName)
        {
            this.tileMap = null;
            int x, y, z;
            int width, height;
            ushort gid;
            Vector2 tempPos = Vector2.Zero;
            FactoryEnemy factory = new FactoryEnemy();

            width = 0;
            height = 0;

            x = 0;
            y = 0;
            z = -1;

            XmlReader xmlReaderLevel = XmlReader.Create(path + levelName + ".tmx");
            while (xmlReaderLevel.Read())
            {
                if (xmlReaderLevel.Name == "layer" && xmlReaderLevel.NodeType != XmlNodeType.EndElement)
                {
                    Console.WriteLine(xmlReaderLevel.GetAttribute("name"));
                    if (tileMap == null)
                    {
                        width = Int32.Parse(xmlReaderLevel.GetAttribute("width"));
                        height = Int32.Parse(xmlReaderLevel.GetAttribute("height"));
                        tileMap = new TileMap(width, height, 16, 16, tileMapTexture);
                    }
                    z++;
                }

                if (xmlReaderLevel.Name == "object" && xmlReaderLevel.NodeType != XmlNodeType.EndElement)
                {
                    if( xmlReaderLevel.GetAttribute("name") != "finish_point")
                    {
                        int xPos = Int32.Parse(xmlReaderLevel.GetAttribute("x"));
                        int yPos = Int32.Parse(xmlReaderLevel.GetAttribute("y"));
                        tempPos.X = xPos;
                        tempPos.Y = yPos;                    
                    }
                }

                if (xmlReaderLevel.Name == "property")
                {
                    String enemyType = xmlReaderLevel.GetAttribute("value");
                    GameObject gameObject = null;
                    //GameObject gameObject =  factory.createEnemy(enemyType, tempPos, this.content);
                    this.enemies.Add(gameObject);
                    
                }

                if(xmlReaderLevel.Name == "object" && xmlReaderLevel.NodeType != XmlNodeType.EndElement)
                {
                    if( xmlReaderLevel.GetAttribute("name") == "finish_point")
                    {
                        finish_area = new Rectangle();
                        finish_area.X = Int32.Parse(xmlReaderLevel.GetAttribute("x"));
                        finish_area.Y = Int32.Parse(xmlReaderLevel.GetAttribute("y"));
                        finish_area.Width = Int32.Parse(xmlReaderLevel.GetAttribute("width"));
                        finish_area.Height = Int32.Parse(xmlReaderLevel.GetAttribute("height"));
                    }
                        
                }

                
                if (xmlReaderLevel.Name == "tile")
                {
                    gid = ushort.Parse(xmlReaderLevel.GetAttribute("gid"));

                    tileMap.setTileAt(x, y, z, gid);
                    x++;

                    if (x == width)
                    {
                        x = 0;
                        y++;
                    }

                    if (y == height)
                    {
                        y = 0;
                    }

                }

            }

            //SoundPlayer.Instance.playBackgroundMusic();
        }


    }
}
