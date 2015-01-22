using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Vector_Space
{
    class PointDouble
    {
        //X horizontal position.
        private double x;
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        //Y vertical position.
        private double y;
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        //Constructor with integer parameters.
        public PointDouble(int _x, int _y)
        {
            x = _x;
            x = _y;
        }

        //Constructor with double parameters.
        public PointDouble(double _x, double _y)
        {
            x = _x;
            x = _y;
        }

        //Returns the double point as an ordinary point.
        public Point ToPoint()
        {
            return new Point((int)x, (int)y);
        }
    }
}
