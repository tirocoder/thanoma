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
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public class Character
    {
        /* START: properties */

        int _type;
        Direction _direction;
        double _speed;

        protected int _x;
        protected int _y;
        protected int _brick_x;
        protected int _brick_y;

        public Texture2D _texture = null;
        public Rectangle _rect;
        

        /* END: properties */


        /* START: constructor(s) */
        public Character()
        {
            
        }

        public Character(ContentManager cm, int type)
        {
            _type = type;
            _speed = 1.0;
            _direction = Direction.Right;

            _x = VaC.WINDOW_WIDTH / 2;
            _y = VaC.WINDOW_HEIGHT - VaC.BRICK_HEIGHT * 10;

            _rect.Width = VaC.PLAYER_WIDTH;
            _rect.Height = VaC.PLAYER_HEIGHT;

            switch (type)
            {
                case 0:
                    // test guy
                    _texture = cm.Load<Texture2D>(VaC.TEXTURE_PLAYER_RIGHT1);
                    break;
                default:
                    // test guy
                    _texture = cm.Load<Texture2D>(VaC.TEXTURE_PLAYER_RIGHT1);
                    break;
            }
        }
        /* END: constructor(s) */

        /* START: methods */

        public void Move(Level level, Direction direction, double speed)
        {
            _speed = speed;
            _direction = direction;

            switch (direction)
            {
                case Direction.Left:
                    // move left
                    int value = level.IsBrickToMyLeft(_brick_x, _brick_y, _x, _y);
                    int delta_x = Convert.ToInt32(Math.Round(2 * speed, MidpointRounding.AwayFromZero));
                    if ((value != 0) && (delta_x <= value))
                    {
                        _x -= delta_x;
                    }
                    else
                    {
                        _x -= value;
                    }
                    break;
                case Direction.Right:
                    // move right
                    int value2 = level.IsBrickToMyRight(_brick_x, _brick_y, _x, _y);
                    int delta_x2 = Convert.ToInt32(Math.Round(2 * speed, MidpointRounding.AwayFromZero));
                    if ((value2 != 0) && (delta_x2 <= value2))
                    {
                        _x += delta_x2;
                    }
                    else
                    {
                        _x += value2;
                    }
                    break;
            }
        }

        public void Jump()
        {

        }

        /* END: methods */
    }
}
