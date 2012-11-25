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

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        HumanPlayer pokemon1;
        //Element pokemon2;
        //Element pokemon3;
        //Element pokemon4;

        static Game1 _instance;

        static public Game getGameInstance()
        {
            return _instance;
        }

        public Game1()
        {
            _instance = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Defaults.window_size_x;
            graphics.PreferredBackBufferHeight = Defaults.window_size_y;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            pokemon1 = new HumanPlayer(@"Images/pikachu", 5f, 5f);
            //pokemon1 = new Element(Window, Content, @"Images/pikachu", 0, 0, 5f, 5f, false);
            //pokemon4 = new Element(Window, Content, @"Images/machoc", 0, 0, 5f, 5f, true);
            //pokemon2 = new Element(Window, Content, @"Images/poke1", 500, 200, 0.8f, 2.3f);
            //pokemon3 = new Element(Window, Content, @"Images/poke2", 0, 0, 4.2f, 2.4f);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            pokemon1.update();
            //pokemon2.update();
            //pokemon3.update();
            //pokemon4.update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            pokemon1.draw(spriteBatch);
            //pokemon2.draw(spriteBatch);
            //pokemon3.draw(spriteBatch);
            //pokemon4.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
