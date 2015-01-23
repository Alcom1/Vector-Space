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
    class Mesh
    {
        //Relative positioning arrays.
        private Vector2[] pointsRelative; //Relative position of points to the center of the mesh.
        private int[] lengthRelative;   //Distance between the points and the center of the mesh.
        private double[] angleRelative; //Direction angle of the points relative to the mesh.
        
        //Absolute positioning arrays.
        private Vector2[] pointsAbsolute; //Absolute position of the points.

        //Line arrays.
        private int[][] adjacency;       //2D array determining what points are connected by lines.

        //Absolute positioning variables.
        private Vector2 center;           //Absolute center of the mesh.
        private double angle;           //Absolute angle of the mesh.
        public double Angle
        {
            get { return angle; }
        }

        //Dimensioning variables.
        int width;

        public Mesh(Vector2[] points, int[][] _adjacency, Vector2 _center, int _width)
        {
            //Establish object center, angle, and width.
            center = _center;
            angle = 0;
            width = _width;

            //Use parameters to instantiate points array and adjacency array.
            pointsRelative = points;
            adjacency = _adjacency;

            //Instantiate all other arrays.
            lengthRelative = new int[points.Length];
            angleRelative = new double[points.Length];
            pointsAbsolute = new Vector2[points.Length];

            //Establish relative length and angle.
            for (int i = 0; i < points.Length; ++i)
            {
                lengthRelative[i] = (int)Math.Sqrt(
                    Math.Pow(pointsRelative[i].X, 2) +
                    Math.Pow(pointsRelative[i].Y, 2));

                angleRelative[i] = Math.Atan2(
                    (pointsRelative[i].X),
                    (pointsRelative[i].Y));
            }
        }

        //Form the mesh based on an angle. Forming generates absolute positions based on relative angle and length.
        public void Form(double angle)
        {
            for (int i = 0; i < pointsAbsolute.Length; ++i)
            {
                pointsAbsolute[i].X = (int)(Math.Sin(angle + angleRelative[i]) * lengthRelative[i]) + center.X;
                pointsAbsolute[i].Y = (int)(Math.Cos(angle + angleRelative[i]) * lengthRelative[i]) + center.Y;
            }
        }

        //Form the mesh based on a direction to face.
        public void Form(int x, int y)
        {
            angle = Math.Atan2(
                (center.X - x),
                (center.Y - y)) + MathHelper.Pi;

            for (int i = 0; i < pointsAbsolute.Length; ++i)
            {
                pointsAbsolute[i].X = (int)(Math.Sin(angle + angleRelative[i]) * lengthRelative[i]) + center.X;
                pointsAbsolute[i].Y = (int)(Math.Cos(angle + angleRelative[i]) * lengthRelative[i]) + center.Y;
            }
        }

        //Draw the mesh.
        public void Draw(Texture2D circle, Texture2D cube, SpriteBatch spriteBatch)
        {
            //Draw all circles.
            foreach (Vector2 point in pointsAbsolute)
            {
                this.DrawCircle(circle, spriteBatch, point);
            }

            //Draw all lines.
            foreach (int[] lineIndex in adjacency)
            {
                this.DrawLine(
                    cube,
                    spriteBatch,
                    pointsAbsolute[lineIndex[0]],
                    pointsAbsolute[lineIndex[1]]);
            }
        }

        //Draw a line in the mesh.
        private void DrawLine(Texture2D cube, SpriteBatch spriteBatch, Vector2 pointI, Vector2 pointJ)
        {
            spriteBatch.Draw(
                cube,
                new Rectangle(
                    (int)pointI.X,
                    (int)pointI.Y,
                    width,
                    (int)Math.Sqrt(
                        Math.Pow(pointI.X - pointJ.X, 2) +
                        Math.Pow(pointI.Y - pointJ.Y, 2))),
                null,
                Color.White,
                (float)Math.Atan2(
                    (pointJ.X - pointI.X),
                    (pointI.Y - pointJ.Y)) + MathHelper.Pi,
                new Vector2(0.5f, 0),
                SpriteEffects.None,
                0f);
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
