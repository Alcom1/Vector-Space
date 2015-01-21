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
    class Triangle
    {
        private Vector2 vectorA;
        private Vector2 vectorB;
        private Vector2 vectorC;

        public Triangle(int ax, int ay)
        {
            vectorA = new Vector2(ax, ay);
            vectorB = new Vector2(100, 150);
            vectorC = new Vector2(2 * vectorA.X - vectorB.X, vectorB.Y);
        }

        public void moveVectorB(int x, int y)
        {
            vectorB = new Vector2(x, y);
        }

        public void moveVectorC()
        {
            vectorC = new Vector2(2 * vectorA.X - vectorB.X, vectorB.Y);
        }

        private void DrawLine(Texture2D cube, SpriteBatch spriteBatch, Vector2 vectorI, Vector2 vectorJ)
        {
            spriteBatch.Draw(
                cube,
                new Rectangle(
                    (int)vectorI.X,
                    (int)vectorI.Y,
                    5,
                    (int)Math.Sqrt(
                        Math.Pow(vectorI.X - vectorJ.X, 2) +
                        Math.Pow(vectorI.Y - vectorJ.Y, 2))),
                null,
                Color.White,
                (float)Math.Atan2(
                    (vectorJ.X - vectorI.X),
                    (vectorI.Y - vectorJ.Y)) + MathHelper.Pi,
                new Vector2(0.5f, 0),
                SpriteEffects.None,
                0f);
        }

        private void DrawCircle(Texture2D circle, SpriteBatch spriteBatch, Vector2 vectorI, int radius)
        {
            spriteBatch.Draw(
                circle,
                new Rectangle(
                    (int)vectorI.X - radius / 2,
                    (int)vectorI.Y - radius / 2,
                    radius,
                    radius),
                Color.White);
        }

        public void Draw(Texture2D circle, Texture2D cube, SpriteBatch spriteBatch)
        {
            this.DrawLine(cube, spriteBatch, vectorA, vectorB);
            this.DrawLine(cube, spriteBatch, vectorB, vectorC);
            this.DrawLine(cube, spriteBatch, vectorC, vectorA);

            this.DrawCircle(circle, spriteBatch, vectorA, 20);
            this.DrawCircle(circle, spriteBatch, vectorB, 20);
            this.DrawCircle(circle, spriteBatch, vectorC, 20);
        }
    }
}
