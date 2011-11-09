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
            npcs.Add(new NPC(cm));
        }

        /* START: methods */

        public void DrawNPCs(ContentManager cm, SpriteBatch sb)
        {
            sb.Draw(npcs[0]._texture, npcs[0]._rect, Color.White);
        }

        /* END: methods */
    }



    public class NPC : Player
    {
        /* START: properties */

        

        /* END: properties */


        // constructor
        public NPC(ContentManager cm) : base(cm, 0)
        {
            _texture = cm.Load<Texture2D>("img/npc/001");
            _rect.Width = _texture.Width;
            _rect.Height = _texture.Height;
            _x = VaC.BRICK_WIDTH * 15;
            _y = VaC.BRICK_HEIGHT * 4;
        }

        /* START: methods */

        bool stop = false;
        public void Walk(Level level)
        {
            if (!stop)
            {
                MovePlayer(level, 'l', 0.3);
            }
            else MovePlayer(level, 'r', 0.3);
            DoFor5Seconds();
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
