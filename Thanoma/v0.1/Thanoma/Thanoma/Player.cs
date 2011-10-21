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

            _rect.Width = VaC.BRICK_WIDTH;
            _rect.Height = VaC.BRICK_HEIGHT;
            _rect.X = VaC.WINDOW_WIDTH / 2;
            _rect.Y = VaC.WINDOW_HEIGHT - VaC.BRICK_HEIGHT * 6;

            switch (type)
            {
                case 0:
                    // test guy
                    _texture = cm.Load<Texture2D>("player1");
                    break;
                default:
                    // test guy
                    _texture = cm.Load<Texture2D>("player1");
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
                    if(level.IsBrickThere(_brick_x - 1, _brick_y) == false) _x -= Convert.ToInt32(Math.Round(2 * speed, MidpointRounding.AwayFromZero));
                    break;
                case 'r':
                    // move right
                    if (level.IsBrickThere(_brick_x + 1, _brick_y) == false) _x += Convert.ToInt32(Math.Round(2 * speed, MidpointRounding.AwayFromZero));
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
                        _texture = cm.Load<Texture2D>("player1");
                        change = false;
                    }
                    else
                    {
                        _texture = cm.Load<Texture2D>("player1b");
                        change = true;
                    }
                    ticks = gt.TotalGameTime + new TimeSpan(0, 0, 0, 0, Convert.ToInt32(200 / _speed));
                }
            }
        }

        DateTime dt = new DateTime();
        
        public void FallDown()
        {
            //if (gt.TotalGameTime > ticks)
            {   
                // let player fall down
               // ticks_falldown = gt.TotalGameTime + new TimeSpan(0, 0, 0, 0, Convert.ToInt32(200 / _speed));
            }
        }

        public void FollowGravity(Level level)
        {
            if(level.IsBrickThere(_brick_x, _brick_y + 1) == false)
            {
                // let player fall down (if not on ground)
                _y += 2;
            }
        }


        public void DeclareRectangle()
        {
            _rect.Width = VaC.BRICK_WIDTH;
            _rect.Height = VaC.BRICK_HEIGHT;
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
            PlayMoveAnimation(cm, gametime);
            _brick_x = (int)Math.Round((double)_x / VaC.BRICK_WIDTH, MidpointRounding.ToEven);
            _brick_y = (int)Math.Round((double)_y / VaC.BRICK_HEIGHT, MidpointRounding.ToEven);
            FollowGravity(level);
            DeclareRectangle();
        }

    }
}
