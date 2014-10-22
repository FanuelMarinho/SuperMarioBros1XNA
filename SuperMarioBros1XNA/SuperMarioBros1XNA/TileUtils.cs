using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioBros1XNA
{
    public static class TileUtils
    {
        public static Rectangle getTileSourceRectangle(int tileValue)
        {
            int XValue = (tileValue % 33) - 1;
            int YValue = (tileValue / 33);
            Rectangle tileRectangle = new Rectangle(XValue * 16, YValue * 16, 16, 16);
            return tileRectangle;
        }
    }
}
