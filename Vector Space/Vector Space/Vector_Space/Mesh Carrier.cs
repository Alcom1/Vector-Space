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
    class MeshCarrier
    {
        Point[] points;
        public Point[] Points
        { 
            get { return points; } 
            set { points = value; }
        }

        int[][] adjacency;
        public int[][] Adjacency
        {
            get { return adjacency; }
            set { adjacency = value; }
        }

        int width;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public MeshCarrier()
        {
            points = new Point[]{};
            adjacency = new int[][]{};
            width = 0;
        }
    }
}
