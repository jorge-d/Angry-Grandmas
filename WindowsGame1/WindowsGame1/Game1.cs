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
        private GraphicsDeviceManager graphics;

        public SpriteFont font = null;
        public Texture2D textureGreen = null;
        public Texture2D textureRed = null;
        private SpriteBatch spriteBatch;
        static Game1 _instance;
        private Stage _stage;
        private string time_display;
        private TimeSpan timer = new TimeSpan();
        private Vector2 position;
        private bool game_over = false;
        private string game_over_msg;
        private Vector2 game_over_msg_position;

        public SoundEffect sheep_death_sound;
        public SoundEffect player_hit_sound;
        public SoundEffect explosion_sound;

        static public Game1 getGameInstance()
        {
            return _instance;
        }

        public Game1()
        {
            _instance = this;
            position = new Vector2((Defaults.window_size_x / 2) - 50, Defaults.stage_square_nb_y * Defaults.stage_square_size);
            game_over_msg_position = new Vector2(Defaults.window_size_x / 3, Defaults.window_size_y / 2);

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Defaults.window_size_x;
            graphics.PreferredBackBufferHeight = Defaults.window_size_y;
            Content.RootDirectory = "Content";

            _stage = Stage.getInstance();
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
            font = Content.Load<SpriteFont>("angrygrandma");

            textureRed = new Texture2D(GraphicsDevice, 1, 1);
            textureRed.SetData(new Color[] { Color.Red });
            textureGreen = new Texture2D(GraphicsDevice, 1, 1);
            textureGreen.SetData(new Color[] { Color.LightGreen });

            sheep_death_sound = Content.Load<SoundEffect>(Defaults.sheep_death_sound);
            player_hit_sound = Content.Load<SoundEffect>(Defaults.player_hit_sound);
            explosion_sound = Content.Load<SoundEffect>(Defaults.explosion_sound);

            _stage.init();
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
            KeyboardState kS = Keyboard.GetState();

            if (kS.IsKeyDown(Keys.I))
                this.Exit();

            if (game_over)
            {
                if (kS.IsKeyDown(Keys.Enter))
                {
                    game_over = false;
                    timer = new TimeSpan();
                    _stage.init();
                }
                else if (kS.IsKeyDown(Keys.Escape))
                    this.Exit();
            }
            else
            {
                _stage.update(gameTime);
                updateTimer(gameTime);
            }
            base.Update(gameTime);
        }

        private void updateTimer(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime;
            time_display = timer.Seconds + ":" + (timer.Milliseconds / 10);

            if (timer.Minutes > Defaults.timer_minutes_number ||
                (timer.Minutes == Defaults.timer_minutes_number && timer.Seconds > Defaults.timer_seconds_number))
            {
                game_over = true;
                game_over_msg = _stage.endOfGame();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!game_over)
                GraphicsDevice.Clear(Color.LightGray);
            else
                GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            if (game_over)
            {
                Vector2 v = new Vector2(Defaults.window_size_x / 6, Defaults.stage_square_nb_y * Defaults.stage_square_size);
                spriteBatch.DrawString(font, game_over_msg, game_over_msg_position, Color.Yellow);
                spriteBatch.DrawString(font, "Press Enter to restart or escape to exit", v, Color.Orange);
            }
            else
            {
                _stage.draw(spriteBatch);
                spriteBatch.DrawString(font, time_display, position, Color.Orange);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
