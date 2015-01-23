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
using System.Threading;

namespace Vector_Space
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Game objects.
        private Mesh player;                //Player mesh.
        List<Particle> projectiles;         //Projectiles.

        //Misc
        private Texture2D cube;             //1x1 Cube texture.
        private Texture2D circle;           //circle texture.
        private MouseState mouse;           //Mouse state.
        private KeyboardState keyboard;     //Keyboard state.
        private MouseState mousePrev;       //Mouse state.
        private KeyboardState keyboardPrev; //Keyboard state.

        //Constructor
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        //Game Method Initialize
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            Window.Title = "";

            //Hardcoded player
            player = new Mesh(
                new Vector2[]
                {
                    new Vector2(0, 40),
                    new Vector2(20, -20),
                    new Vector2(-20, -20),
                    new Vector2(0,0)
                },
                new int[][]
                {
                    new int[] {0, 1},
                    new int[] {1, 2},
                    new int[] {2, 0},

                    new int[] {0, 3},
                    new int[] {1, 3},
                    new int[] {2, 3}
                },
                new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
                4);

            //Projectiles
            projectiles = new List<Particle>();
        }

        //Game Method load and generate content
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            cube = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            cube.SetData<Color>(new Color[1] { Color.White });

            circle = CreateCircleFilled(50);
        }

        //Game Method Update
        protected override void Update(GameTime gameTime)
        {
            //Gets mouse and keyboard state.
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();

            //Core game logic.
            player.Form(mouse.X, mouse.Y);
            this.HandleProjectiles();

            //Saves last frame's mouse and keyboard state.
            mousePrev = Mouse.GetState();
            keyboardPrev = Keyboard.GetState();
            base.Update(gameTime);
        }

        //Game Method Draw
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Begin SpriteBatch.
            spriteBatch.Begin();

            //Draw player
            player.Draw(circle, cube, spriteBatch);

            //Draw projectiles
            foreach (Particle projectile in projectiles)
            {
                projectile.Draw(circle, spriteBatch);
            }
            
            //Draw Mouse
            spriteBatch.Draw(
                circle,
                new Rectangle(mouse.X - 10, mouse.Y - 10, 20, 20),
                Color.White);

            //End SpriteBatch
            spriteBatch.End();
            base.Draw(gameTime);
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

        //Handles projectiles
        public void HandleProjectiles()
        {
            //Button click generates a projectile.
            if (mouse.LeftButton == ButtonState.Pressed && mousePrev.LeftButton == ButtonState.Released)
            {
                projectiles.Add(new Particle(
                    new Vector2[]
                    {
                        new Vector2(0, 0),
                    },
                    new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
                    4,
                    player.Angle,
                    200));
            }

            //Moves or clears projectiles.
            for(int i = 0; i < projectiles.Count; ++i)
            {
                if(projectiles[i].Form(3))
                {
                    //clear projectiles past thier lifeLength.
                    projectiles.RemoveAt(i);
                }
            }
        }
    }
}