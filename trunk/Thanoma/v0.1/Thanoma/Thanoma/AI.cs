using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Timers;

namespace Thanoma
{
    public class AI
    {
        /* START: properties */

        public IList<NPC> npcs = new List<NPC>();

        /* END: properties */


        // constructor
        public AI(ContentManager cm)
        {
            npcs.Add(new NPC(cm, 0));
        }

        /* START: methods */

        public void DrawNPCs(ContentManager cm, SpriteBatch sb)
        {
            sb.Draw(npcs[0]._texture, npcs[0]._rect, Color.White);
        }

        /* END: methods */
    }



    public class NPC : Character
    {
        /* START: properties */

        

        /* END: properties */


        // constructor
        public NPC(ContentManager cm, int type) : base()
        {
            _type = type;
            _texture = cm.Load<Texture2D>("img/npc/001");
            _rect.Width = _texture.Width;
            _rect.Height = _texture.Height;
            _x = VaC.BRICK_WIDTH * 15;
            _y = VaC.BRICK_HEIGHT * 4;

            // Timer

            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        /* START: methods */

        bool stop = false;

        Timer timer = new Timer(500);
        int i_elapsed = 0;
        public void Walk(Level level)
        {
            if (i_elapsed < 10)
            {
                Move(level, Direction.Left, 0.3);
            }
            if (i_elapsed >= 10 && i_elapsed < 15)
            {
                if(i_elapsed!=13) Jump1(level);
            }
            if (i_elapsed >= 15 && i_elapsed < 28)
            {
                Move(level, Direction.Right, 0.3);
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            i_elapsed++;
            timer.Stop();
            timer.Start();
        }

        DateTime dt = DateTime.Now + TimeSpan.FromSeconds(15);
        public void DoFor5Seconds()
        {
            if (DateTime.Now >= dt)
            {
                stop = true;
            }
        }

        public void Update(ContentManager cm, GameTime gametime, Level level)
        {
            _brick_x = (int)((double)_x / VaC.BRICK_WIDTH);
            _brick_y = (int)((double)_y / VaC.BRICK_HEIGHT);
            FollowGravity(level);
            DeclareRectangle();

            Walk(level);
        }

        /* END: methods */
    }
}
