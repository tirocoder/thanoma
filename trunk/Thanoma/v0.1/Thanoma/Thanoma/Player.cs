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
        double _speed;
        char _direction;
        int _x;
        int _y;
        int _brick_x;
        int _brick_y;

        public Texture2D _texture = null;
        public Rectangle _rect;

        public Player(ContentManager cm, int type)
        {
            _type = type;
            _speed = 1.0;
            _direction = 'r';

            _x = VaC.WINDOW_WIDTH / 2;
            _y = VaC.WINDOW_HEIGHT - VaC.BRICK_HEIGHT * 6;

            _rect.Width = VaC.PLAYER_WIDTH;
            _rect.Height = VaC.PLAYER_HEIGHT;
            _rect.X = VaC.WINDOW_WIDTH / 2;
            _rect.Y = VaC.WINDOW_HEIGHT - VaC.BRICK_HEIGHT * 6;

            switch (type)
            {
                case 0:
                    // test guy
                    _texture = cm.Load<Texture2D>("player_right1");
                    break;
                default:
                    // test guy
                    _texture = cm.Load<Texture2D>("player_right1");
                    break;
            }
        }

        public void MovePlayer(Level level, char direction, double speed)
        {
            _speed = speed;
            _direction = direction;

            switch(direction)
            {
                case 'l':
                    // move left
                    if (level.IsBrickToMyLeft(_brick_x, _brick_y) == false) _x -= Convert.ToInt32(Math.Round(2 * speed, MidpointRounding.AwayFromZero));
                    break;
                case 'r':
                    // move right
                    if (level.IsBrickToMyRight(_brick_x, _brick_y) == false) _x += Convert.ToInt32(Math.Round(2 * speed, MidpointRounding.AwayFromZero));
                    break;
            }
        }

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
                        _texture = cm.Load<Texture2D>("player_right1");
                        change = false;
                    }
                    else
                    {
                        _texture = cm.Load<Texture2D>("player_right2");
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
                        _texture = cm.Load<Texture2D>("player_left1");
                        change = false;
                    }
                    else
                    {
                        _texture = cm.Load<Texture2D>("player_left2");
                        change = true;
                    }
                    ticks = gt.TotalGameTime + new TimeSpan(0, 0, 0, 0, Convert.ToInt32(200 / _speed));
                }
            }
        }

        int start_y;
        bool is_jumping = false;
        public void JumpPlayer(Level level)
        {
            if (level.IsBrickAboveMe(_brick_x, _brick_y) == false)
            {
                if (_y >= start_y - 56)
                {
                    is_jumping = true;
                    _y -= 10;
                }
                else is_jumping = false;
            }
        }

        //DateTime dt = new DateTime();
        public void FallDown()
        {
            //if (gt.TotalGameTime > ticks)
            {   
                // let player fall down
               // ticks_falldown = gt.TotalGameTime + new TimeSpan(0, 0, 0, 0, Convert.ToInt32(200 / _speed));
            }
        }

        double val = 0.0;
        public void FollowGravity(Level level)
        {
            if (level.IsBrickUnderMe(_brick_x, _brick_y) == false)
            {
                // let player fall down (if not on ground)
                _y += 1 + (int)val;
                val += 0.3;
            }
            else val = 0.0;
        }


        public void DeclareRectangle()
        {
            _rect.Width = VaC.PLAYER_WIDTH;
            _rect.Height = VaC.PLAYER_HEIGHT;
            _rect.X = _x;
            _rect.Y = _y;
        }

        public void Update(ContentManager cm, GameTime gametime, Level level, SpriteBatch sb)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A) == true)
            {
                MovePlayer(level, 'l', 1.0);
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.A) == true) && (Keyboard.GetState().IsKeyDown(Keys.Space) == true))
            {
                MovePlayer(level, 'l', 1.3);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) == true)
            {
                MovePlayer(level, 'r', 1.0);
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.D) == true) && (Keyboard.GetState().IsKeyDown(Keys.Space) == true))
            {
                MovePlayer(level, 'r', 1.3);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.J) == true)
            {
                // gerade nach oben springen
                if (is_jumping == false)
                {
                    start_y = _y;
                }
                JumpPlayer(level);
            }

            PlayMoveAnimation(cm, gametime);
            _brick_x = (int)((double)_x / VaC.PLAYER_WIDTH);
            _brick_y = (int)((double)_y / VaC.PLAYER_HEIGHT);
            FollowGravity(level);
            DeclareRectangle();
        }

    }
}
