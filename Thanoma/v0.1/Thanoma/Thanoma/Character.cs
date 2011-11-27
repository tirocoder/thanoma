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

        protected int _type;
        protected Direction _direction;
        protected double _speed;

        protected int _x;
        protected int _y;
        protected int _brick_x;
        protected int _brick_y;

        protected string[] _textures = new string[4];
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
                    _textures[0] = VaC.TEXTURE_PLAYER_LEFT1;
                    _textures[1] = VaC.TEXTURE_PLAYER_LEFT2;
                    _textures[2] = VaC.TEXTURE_PLAYER_RIGHT1;
                    _textures[3] = VaC.TEXTURE_PLAYER_RIGHT2;
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

        /// <summary>
        /// animates the character while walking right/left
        /// </summary>
        bool change = false;
        TimeSpan ticks = new TimeSpan(0);
        public void PlayMoveAnimation(ContentManager cm, GameTime gt)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D) == true)
            {
                if (gt.TotalGameTime > ticks)
                {
                    if (change == true)
                    {
                        _texture = cm.Load<Texture2D>(_textures[2]);
                        change = false;
                    }
                    else
                    {
                        _texture = cm.Load<Texture2D>(_textures[3]);
                        change = true;
                    }
                    ticks = gt.TotalGameTime + new TimeSpan(0, 0, 0, 0, Convert.ToInt32(200 / _speed));
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) == true)
            {
                if (gt.TotalGameTime > ticks)
                {
                    if (change == true)
                    {
                        _texture = cm.Load<Texture2D>(_textures[0]);
                        change = false;
                    }
                    else
                    {
                        _texture = cm.Load<Texture2D>(_textures[1]);
                        change = true;
                    }
                    ticks = gt.TotalGameTime + new TimeSpan(0, 0, 0, 0, Convert.ToInt32(200 / _speed));
                }
            }
        }

        /// <summary>
        /// character jumps while user presses and holds jump key
        /// </summary>
        int start_y;
        bool is_jumping = false;
        public void Jump1(Level level)
        {
            int value = level.IsBrickAboveMe(_brick_x, _brick_y, _x, _y);
            if ((value != 0) && (10 <= value))
            {
                if (_y >= start_y - 56)
                {
                    //is_jumping = true;
                    _y -= 10;
                }
                //else is_jumping = false;
            }
            else //((value != 0) && (10 >= value))
            {
                _y += value;
            }
        }

        public void Jump2(Level level)
        {
            int value = level.IsBrickAboveMe(_brick_x, _brick_y, _x, _y);
            if ((value != 0) && (10 <= value))
            {
                if (_y >= start_y - 56)
                {
                    //is_jumping = true;
                    _y -= 7;
                }
                //else is_jumping = false;
            }
            else //((value != 0) && (10 >= value))
            {
                _y += value;
            }
        }

        /// <summary>
        /// makes character follow gravity
        /// </summary>
        double val = 0.0;
        public void FollowGravity(Level level)
        {
            int value = level.IsBrickUnderMe(_brick_x, _brick_y, _x, _y);
            if ((value != 0) && (2 + (int)val <= value))
            {
                // let player fall down (if not on ground)
                _y += 2 + (int)val;
                val += 0.3;
            }
            else if ((value != 0) && (2 + (int)val >= value))
            {
                _y += value;
            }
            else val = 0.0;
        }





        /// <summary>
        /// declares rectangle of character for drawing
        /// </summary>
        public void DeclareRectangle()
        {
            _rect.Width = VaC.PLAYER_WIDTH;
            _rect.Height = VaC.PLAYER_HEIGHT;
            _rect.X = _x;
            _rect.Y = _y;
        }

        /// <summary>
        /// updates the character
        /// </summary>
        /// <param name="cm"></param>
        /// <param name="gametime"></param>
        /// <param name="level"></param>
        /// <param name="sb"></param>
        /// <param name="is_player"></param>
        public void Update(ContentManager cm, GameTime gametime, Level level, SpriteBatch sb)
        {
            PlayMoveAnimation(cm, gametime);
            _brick_x = (int)((double)_x / VaC.BRICK_WIDTH);
            _brick_y = (int)((double)_y / VaC.BRICK_HEIGHT);
            FollowGravity(level);
            DeclareRectangle();
        }


        /* END: methods */
    }
}
