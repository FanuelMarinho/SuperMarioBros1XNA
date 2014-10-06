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
    public class XMLWorldLoader : IWorldLoader
    {

        private const string extension = ".tmx";
        private const string path = @"Content\Levels\";

        private TileMap getTileMapFromXML(int width, int height, int tileBase)
        {
            TileMap tileMap = new TileMap(width, height, tileBase);
            return tileMap;
        }

        public World LoadWorld(string worldName)
        {
            int tileMapWidth = 0;
            int tileMapHeight = 0;
            int x = 0, y = 0, z = -1;
            
            World world = null;
            TileMap tileMap = null;

            List<GameObject> enemies = new List<GameObject>();

            using (XmlReader xmlReader = XmlReader.Create(path + worldName + extension))
            {
                while(xmlReader.Read())
                {
                    if(xmlReader.IsStartElement())
                    { 
                        switch(xmlReader.Name.ToString())
                        {
                            case "layer":
                                if (tileMap == null) 
                                {
                                    tileMapWidth = Int32.Parse(xmlReader.GetAttribute("width"));
                                    tileMapHeight = Int32.Parse(xmlReader.GetAttribute("height"));
                                    tileMap = getTileMapFromXML(tileMapWidth, tileMapHeight, 16);
                                }
                                z++;
                                break;

                            case "tile":
                                ushort gid = ushort.Parse(xmlReader.GetAttribute("gid"));
                                tileMap.setTileAt(x, y, z, gid);
                                x++;

                                if (x == tileMapWidth)
                                {
                                    x = 0;
                                    y++;
                                }
                                if (y == tileMapHeight)
                                {
                                    y = 0;
                                }
                                break;
                            
                        }

                    }

                }

                world = new World(tileMap, enemies);
            }
            return world;
            
        }

        public Level LoadLevel(string name)
        {
            
            Level level = new Level();
            /*this.tileMap = null;
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
                    if (xmlReaderLevel.GetAttribute("name") != "finish_point")
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
                    GameObject gameObject = factory.createEnemy(enemyType, tempPos, this.content);
                    this.enemies.Add(gameObject);

                }

                if (xmlReaderLevel.Name == "object" && xmlReaderLevel.NodeType != XmlNodeType.EndElement)
                {
                    if (xmlReaderLevel.GetAttribute("name") == "finish_point")
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
           */
            return level;
        }
    }
}
