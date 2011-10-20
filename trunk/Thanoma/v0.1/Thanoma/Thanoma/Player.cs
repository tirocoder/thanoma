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
    public class Player
    {
        int _type;
        public Texture2D _texture = null;
        public Rectangle _rect;

        public Player(ContentManager cm, int type)
        {
            _type = type;

            _rect.Width = VaC.BRICK_WIDTH;
            _rect.Height = VaC.BRICK_HEIGHT;
            _rect.X = VaC.WINDOW_WIDTH / 2;
            _rect.Y = VaC.WINDOW_HEIGHT - VaC.BRICK_HEIGHT * 6;

            switch (type)
            {
                case 0:
                    // test guy
                    //_texture = cm.Load<Texture2D>("player1");
                    break;
                default:
                    // test guy
                    //_texture = cm.Load<Texture2D>("player1");
                    break;
            }
        }

    }
}
