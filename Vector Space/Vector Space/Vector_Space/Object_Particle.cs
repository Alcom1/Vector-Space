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
    class Particle
    {
        //Relative positioning arrays.
        private Vector2[] pointsRelative; //Relative position of points to the center of the mesh.
        private float[] lengthRelative;   //Distance between the points and the center of the mesh.
        private double[] angleRelative; //Direction angle of the points relative to the mesh.
        
        //Absolute positioning arrays.
        private Vector2[] pointsAbsolute; //Absolute position of the points.

        //Absolute positioning variables.
        private Vector2 center;           //Absolute center of the mesh.
        private double angle;           //Absolute angle of the mesh.
        public double Angle
        {
            get { return angle; }
        }

        //Dimensioning variables.
        int width;

        //Stats
        double lifeDistance;
        double currentDistance;

        public Particle(Vector2[] points, Vector2 _center, int _width, double _angle, double _lifeDistance)
        {
            //Establish object center, angle, width, and life length.
            center = _center;
            angle = _angle;
            width = _width;
            lifeDistance = _lifeDistance;
            currentDistance = 0;

            //Use parameters to instantiate points array and adjacency array.
            pointsRelative = points;

            //Instantiate all other arrays.
            lengthRelative = new float[points.Length];
            angleRelative = new double[points.Length];
            pointsAbsolute = new Vector2[points.Length];

            //Establish relative length and angle.
            for (int i = 0; i < points.Length; ++i)
            {
                lengthRelative[i] = (float)Math.Sqrt(
                    Math.Pow(pointsRelative[i].X, 2) +
                    Math.Pow(pointsRelative[i].Y, 2));

                angleRelative[i] = Math.Atan2(
                    (pointsRelative[i].X),
                    (pointsRelative[i].Y));
            }

            for (int i = 0; i < pointsAbsolute.Length; ++i)
            {
                pointsAbsolute[i].X = (float)(Math.Sin(_angle + angleRelative[i]) * lengthRelative[i]) + center.X;
                pointsAbsolute[i].Y = (float)(Math.Cos(_angle + angleRelative[i]) * lengthRelative[i]) + center.Y;
            }
        }

        //Form the projectile a step ahead to make it travel forward.
        public bool Form(double distance)
        {
            bool kill = false;  //True if projectile has travelled its life length.

            //Test if projectile has travelled its life length.
            currentDistance += distance;
            if(currentDistance > lifeDistance)
            {
                kill = true;
            }

            //Move projectile.
            for (int i = 0; i < pointsAbsolute.Length; ++i)
            {
                pointsAbsolute[i].X += (float)(distance * Math.Sin(angle));
                pointsAbsolute[i].Y += (float)(distance * Math.Cos(angle));
            }

            //Return.
            return kill;
        }

        //Draw the mesh.
        public void Draw(Texture2D circle, SpriteBatch spriteBatch)
        {
            //Draw all circles.
            foreach (Vector2 point in pointsAbsolute)
            {
                this.DrawCircle(circle, spriteBatch, point);
            }
        }

        //Draw a point in the mesh.
        private void DrawCircle(Texture2D circle, SpriteBatch spriteBatch, Vector2 pointI)
        {
            spriteBatch.Draw(
                circle,
                new Rectangle(
                    (int)pointI.X - width / 2,
                    (int)pointI.Y - width / 2,
                    width,
                    width),
                Color.White);
        }
    }
}
