using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace SuperMarioBros1XNA
{
    public class FactoryBlock
    {
        private enum Blocks { NORMAL, QUESTION_COIN, QUESTION_ITEM };

        public Block createBlock(XmlReader xmlReader)
        {
            Blocks block;
            String blockType = "";
            
            Block blockObject = null;
            Rectangle blockRect = new Rectangle();

            int xPos = Int32.Parse(xmlReader.GetAttribute("x"));
            int yPos = Int32.Parse(xmlReader.GetAttribute("y"));
            int width = Int32.Parse(xmlReader.GetAttribute("width"));
            int height = Int32.Parse(xmlReader.GetAttribute("height"));
            blockRect = new Rectangle(xPos, yPos, width, height);
                
            blockType = xmlReader.GetAttribute("type");
            blockType = blockType.ToUpper();
            Enum.TryParse<Blocks>(blockType, out block);  
            
            switch(block)
            {
                case Blocks.NORMAL:
                    blockObject = createNormalBlock(xmlReader, blockRect);
                    break;
                case Blocks.QUESTION_COIN:
                    blockObject = createQuestionCoinBlock(xmlReader, blockRect);
                    break;
            }
            return blockObject;
        }

        private QuestionCoinBlock createQuestionCoinBlock(XmlReader xmlReader, Rectangle blockRect)
        {
            ushort gid = 0;
            int coins = 0;

            while (xmlReader.Read())
            {
                if (xmlReader.Name == "property")
                {
                    String attribute = xmlReader.GetAttribute("name");
                    switch (attribute)
                    {
                        case "gid":
                            gid = ushort.Parse(xmlReader.GetAttribute("value"));
                            break;
                    }

                }

                if ((xmlReader.Name == "properties") && (!xmlReader.IsStartElement()))
                {
                    break;
                }
            }


            return new QuestionCoinBlock(gid, blockRect, coins);
        }

        private NormalBlock createNormalBlock(XmlReader xmlReader, Rectangle blockRect)
        {
            ushort gid = 0;
            while(xmlReader.Read())
            {
                if (xmlReader.Name == "property")
                {
                    String attribute = xmlReader.GetAttribute("name");
                    switch(attribute)
                    {
                        case "gid":
                            gid = ushort.Parse(xmlReader.GetAttribute("value"));
                            break;
                    }

                }

                if ((xmlReader.Name == "properties") && (!xmlReader.IsStartElement()))
                {
                    break;
                }
            }


            return new NormalBlock(blockRect, gid);
        }

    }
}
