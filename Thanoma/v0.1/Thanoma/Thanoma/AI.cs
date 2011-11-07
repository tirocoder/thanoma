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

        IList<NPC> npcs = new List<NPC>();

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



    public class NPC
    {
        /* START: properties */

        int _type;
        double _speed;
        char _direction; 
        int _x;
        int _y;
        int _brick_x;
        int _brick_y;

        public Texture2D _texture = null;
        public Rectangle _rect;

        /* END: properties */


        // constructor
        public NPC(ContentManager cm)
        {
            _texture = cm.Load<Texture2D>("img/npc/001");
            _rect.Width = _texture.Width;
            _rect.Height = _texture.Height;
            _rect.X = VaC.BRICK_WIDTH * 29;
            _rect.Y = VaC.BRICK_HEIGHT * 10;
        }

        /* START: methods */

        /* END: methods */
    }
}
