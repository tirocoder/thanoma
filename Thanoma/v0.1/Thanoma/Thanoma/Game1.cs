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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Thanoma
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch spriteBatch2;

        //Texture2D bg;
        //public Rectangle rect_bg;

        int start_x = 0;
        int start_x2 = 0;

        Player test_player;
        Level test_level;

        AI ai;

        private Camera2D camera = new Camera2D();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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

            ContentManager cm = Content;
            test_player = new Player(cm, 0);
            test_level = new Level();

            ai = new AI(cm);


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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
                this.Exit();

            // TODO: Add your update logic here
            ContentManager cm = Content;
            test_player.Update(cm, gameTime, test_level, spriteBatch);
            
            this.camera.Update(gameTime, new Vector2(test_player._rect.X - VaC.WINDOW_WIDTH / 2, 0));
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.PreferredBackBufferWidth = VaC.WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = VaC.WINDOW_HEIGHT;
            graphics.ApplyChanges();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, this.camera.getMatrix());
            
            ContentManager cm = Content;

            // TEST LEVEL
            test_level.DrawLevel1(cm, spriteBatch);

            // TEST AI
            ai.DrawNPCs(cm, spriteBatch);

            // TEST PLAYER
            spriteBatch.Draw(test_player._texture, test_player._rect, Color.White);
           

            spriteBatch.End();
            

            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
        }
        
    }
}
