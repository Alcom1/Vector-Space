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
    class Player
    {
        private Point[] pointsRelative;
        private Point[] pointsAbsolute;
        private int[] lengthRelative;
        private double[] angleRelative;

        private Point center;
        private double angle;
        public double Angle
        {
            get { return angle; }
        }

        private int thickness;

        public Player(int length, int width, GraphicsDevice screenReference)
        {
            center = new Point(screenReference.Viewport.Width / 2, screenReference.Viewport.Height / 2);
            angle = 0;

            pointsRelative = new Point[3];
            pointsAbsolute = new Point[3];
            lengthRelative = new int[3];
            angleRelative = new double[3];

            pointsRelative[0].X = center.X;
            pointsRelative[0].Y = center.Y + length / 2;

            pointsRelative[1].X = center.X + width / 2;
            pointsRelative[1].Y = center.Y - length / 2;

            pointsRelative[2].X = center.X - width / 2;
            pointsRelative[2].Y = center.Y - length / 2;

            for (int i = 0; i < pointsRelative.Length; ++i)
            {
                lengthRelative[i] = (int)Math.Sqrt(
                    Math.Pow(pointsRelative[i].X - center.X, 2) +
                    Math.Pow(pointsRelative[i].Y - center.Y, 2));

                angleRelative[i] = Math.Atan2(
                    (center.X - pointsRelative[i].X),
                    (center.Y - pointsRelative[i].Y)) + MathHelper.Pi;
            }

            thickness = 6;
        }

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

        private void DrawLine(Texture2D cube, SpriteBatch spriteBatch, Point pointI, Point pointJ)
        {
            spriteBatch.Draw(
                cube,
                new Rectangle(
                    (int)pointI.X,
                    (int)pointI.Y,
                    thickness,
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

        private void DrawCircle(Texture2D circle, SpriteBatch spriteBatch, Point pointI, int radius)
        {
            spriteBatch.Draw(
                circle,
                new Rectangle(
                    (int)pointI.X - radius / 2,
                    (int)pointI.Y - radius / 2,
                    radius,
                    radius),
                Color.White);
        }

        public void Draw(Texture2D circle, Texture2D cube, SpriteBatch spriteBatch)
        {
            this.DrawLine(cube, spriteBatch, pointsAbsolute[0], pointsAbsolute[1]);
            this.DrawLine(cube, spriteBatch, pointsAbsolute[1], pointsAbsolute[2]);
            this.DrawLine(cube, spriteBatch, pointsAbsolute[2], pointsAbsolute[0]);
            this.DrawLine(cube, spriteBatch, pointsAbsolute[0], center);
            this.DrawLine(cube, spriteBatch, pointsAbsolute[1], center);
            this.DrawLine(cube, spriteBatch, pointsAbsolute[2], center);

            this.DrawCircle(circle, spriteBatch, center, thickness);
            this.DrawCircle(circle, spriteBatch, pointsAbsolute[0], thickness);
            this.DrawCircle(circle, spriteBatch, pointsAbsolute[1], thickness);
            this.DrawCircle(circle, spriteBatch, pointsAbsolute[2], thickness);
        }
    }
}
