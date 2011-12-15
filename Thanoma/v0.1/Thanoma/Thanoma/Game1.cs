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
        SpriteFont _statsFont;

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

            ai = new AI(cm, test_level, test_player);


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

            _statsFont = Content.Load<SpriteFont>("fonts/test1");
            
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

        bool restart = false;

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



            if (Keyboard.GetState().IsKeyDown(Keys.A) == true)
            {
                test_player.Move(test_level, Direction.Left, 1.0);
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.A) == true) && (Keyboard.GetState().IsKeyDown(Keys.Space) == true))
            {
                test_player.Move(test_level, Direction.Left, 1.15);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) == true)
            {
                test_player.Move(test_level, Direction.Right, 1.0);
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.D) == true) && (Keyboard.GetState().IsKeyDown(Keys.Space) == true))
            {
                test_player.Move(test_level, Direction.Right, 1.15);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) == true)
            {
                test_player.Jump1(test_level);
                //test_player.StartJump(test_level, DateTime.Now);
            }

            bool doit = true;

            ContentManager cm = Content;
            test_player.Update(cm, gameTime, test_level, spriteBatch);
            foreach (NPC npc in ai.npcs)
            {
                npc.Update(cm, gameTime, test_level, test_player);
                if (ai.CheckHitPlayer(npc, test_player))
                {
                    doit = false;
                }
            }

            if (doit) ai.LetNPCFollowPlayer(test_level, test_player);
            else
            {
                if (!restart)
                {
                    test_player._lives--;
                    restart = true;
                }
            }
            
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

            // TEST AI
            ai.DrawNPCs(cm, spriteBatch);

            // TEST PLAYER
            spriteBatch.Draw(test_player._texture, test_player._rect, Color.White);

            // TEST LEVEL
            test_level.DrawLevel1(cm, spriteBatch);
            
            // render player lives
            spriteBatch.DrawString(_statsFont, string.Format("Lives: {0}", test_player._lives), new Vector2(test_player._x - VaC.WINDOW_WIDTH / 2 + 30, 30), Color.Red);

            spriteBatch.End();
            

            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
        }
        
    }
}
