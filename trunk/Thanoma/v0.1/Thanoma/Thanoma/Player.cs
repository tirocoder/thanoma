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
//using System.Timers;

namespace Thanoma
{
    public class Player : Character
    {
        public Player(ContentManager cm, int type) : base(cm, 0)
        {
            _type = type;
            _speed = 1.0;
            _direction = Direction.Right;
            _lives = 3;

            _x = VaC.WINDOW_WIDTH / 2;
            _y = VaC.WINDOW_HEIGHT - VaC.BRICK_HEIGHT * 10;

            _rect.Width = VaC.PLAYER_WIDTH;
            _rect.Height = VaC.PLAYER_HEIGHT;
        }


    }
}
