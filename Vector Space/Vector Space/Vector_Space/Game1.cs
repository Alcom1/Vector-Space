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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Player triangle;
        private Texture2D cube;
        private Texture2D circle;
        private MouseState mouse;
        private KeyboardState keyboard;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        //Game Method
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            triangle = new Player(
                100,
                100,
                GraphicsDevice);
            Window.Title = "";
        }

        //Game Method
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            cube = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            cube.SetData<Color>(new Color[1] { Color.White });

            circle = CreateCircleFilled(200);
        }

        //Game Method
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        //Game Method
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();

            Console.Clear();
            triangle.Form(mouse.X, mouse.Y);
            Console.WriteLine(triangle.Angle);

            base.Update(gameTime);
        }

        //Game Method
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            base.Draw(gameTime);
            spriteBatch.Begin();
            triangle.Draw(circle, cube, spriteBatch);
            spriteBatch.End();
        }

        //Creates a filled circle texture.
        public Texture2D CreateCircleFilled(int radius)
        {
            Texture2D circleTexture = new Texture2D(GraphicsDevice, radius * 2, radius * 2);    //Texture
            Color[] circle = new Color[radius * radius * 4];                                    //Texture as color array

            //For each horizontal segment
            for (int segment = 0; segment < radius * 2; segment++)
            {
                //Calculate the length of the segment to be bitten off on each end.
                int biteOff = radius - (int)Math.Sqrt(
                            Math.Pow(radius, 2) -
                            Math.Pow(radius - segment, 2));

                //For each pixel that is not bitten off, color that as part of the circle.
                for (int i = segment * radius * 2 + biteOff;
                    i < segment * radius * 2 + radius * 2 - biteOff;
                    ++i)
                {
                    circle[i] = Color.White;    //Make pixel in circle white.
                }
            }

            //Put color array in texture.
            GraphicsDevice.Textures[0] = null;
            circleTexture.SetData(circle);
            
            return circleTexture;
        }
    }
}