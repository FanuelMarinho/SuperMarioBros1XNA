﻿using System;
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

        private List<GameObject> loadeEnemies(XmlReader xmlReader)
        {
            List<GameObject> enemies = new List<GameObject>();
            Vector2 enemyPos = Vector2.Zero;
            FactoryEnemy factory = new FactoryEnemy();

            while(xmlReader.Read())
            {
                if ((!xmlReader.IsStartElement()) && (xmlReader.Name.ToString() == "objectgroup"))
                {
                    break;
                }
                else 
                {
                    if (xmlReader.IsStartElement())
                    {
                        if (xmlReader.Name == "object")
                        {
                            int xPos = Int32.Parse(xmlReader.GetAttribute("x"));
                            int yPos = Int32.Parse(xmlReader.GetAttribute("y"));
                            enemyPos.X = xPos;
                            enemyPos.Y = yPos;
                        }                    
                    }

                    if (xmlReader.Name == "property")
                    {
                        String enemyType = xmlReader.GetAttribute("value");
                        GameObject gameObject = factory.createEnemy(enemyType, enemyPos);
                        enemies.Add(gameObject);

                    }
                }
                
            }

            return enemies;
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
                            
                            case "objectgroup":
                                if (xmlReader.GetAttribute("name") == "enemies")
                                {
                                    enemies = loadeEnemies(xmlReader);
                                }
                                break;

                        }

                    }

                }

                world = new World(tileMap, enemies);
            }
            return world;
            
        }

    }
}
