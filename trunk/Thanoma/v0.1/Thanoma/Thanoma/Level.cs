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
    public class Level
    {
        /* START: properties */

        int[,] _tilemap = new int[16, VaC.LEVEL_WIDTH / VaC.BRICK_WIDTH];

        /* END: properties */

        // constructor
        public Level()
        {
            Random rd = new Random();
            for (int i2 = 0; i2 < VaC.LEVEL_WIDTH / VaC.BRICK_WIDTH; i2++)
            {
                _tilemap[0, i2] = 0;
                _tilemap[1, i2] = 0;
                _tilemap[2, i2] = 0;
                _tilemap[3, i2] = 0;
                _tilemap[4, i2] = 0;
                _tilemap[5, i2] = 0;
                _tilemap[6, i2] = 0;
                _tilemap[7, i2] = 0;
                _tilemap[8, i2] = 0;
                _tilemap[9, i2] = 0;
                _tilemap[10, i2] = 0;
                _tilemap[11, i2] = 101;
                _tilemap[12, i2] = 100;
                _tilemap[13, i2] = (rd.Next(200, 202) % 2 == 0) ? 100 : 102;
                _tilemap[14, i2] = rd.Next(200, 202);
                _tilemap[15, i2] = 200;
            }
            _tilemap[11, 19] = 0;
            _tilemap[11, 20] = 0; 
            _tilemap[11, 78] = 0;
            _tilemap[11, 79] = 0; 
        }

        /* START: methods */

        public void BuildTestLevel(ContentManager cm, SpriteBatch sb, int start_x)
        {
            for (int i = 0; i < VaC.LEVEL_WIDTH / VaC.BRICK_WIDTH; i++)
            {
                Brick brick = new Brick(cm, 11 * VaC.BRICK_HEIGHT, start_x + i * VaC.BRICK_WIDTH, _tilemap[11,i]);
                sb.Draw(brick._texture, brick._rect, Color.White);
                Brick brick2 = new Brick(cm, 12 * VaC.BRICK_HEIGHT, start_x + i * VaC.BRICK_WIDTH, _tilemap[12, i]);
                sb.Draw(brick2._texture, brick2._rect, Color.White);
                Brick brick3 = new Brick(cm, 13 * VaC.BRICK_HEIGHT, start_x + i * VaC.BRICK_WIDTH, _tilemap[13, i]);
                sb.Draw(brick3._texture, brick3._rect, Color.White);
                Brick brick4 = new Brick(cm, 14 * VaC.BRICK_HEIGHT, start_x + i * VaC.BRICK_WIDTH, _tilemap[14, i]);
                sb.Draw(brick4._texture, brick4._rect, Color.White);
                Brick brick5 = new Brick(cm, 15 * VaC.BRICK_HEIGHT, start_x + i * VaC.BRICK_WIDTH, _tilemap[15, i]);
                sb.Draw(brick5._texture, brick5._rect, Color.White);
            }
        }

        public bool IsBrickThere(int brick_x, int brick_y)
        {
            try
            {
                if (_tilemap[brick_y, brick_x] != 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        
        /* END: methods */
    }

    public class Brick
    {
        int _type;
        public Texture2D _texture;
        public Rectangle _rect;
        
        
        public Brick(ContentManager cm,int top, int left, int type)
        {
            //_width = VaC.BRICK_HEIGHT;
            //_height = VaC.BRICK_WIDTH;

            _type = type;

            _rect.Width = VaC.BRICK_WIDTH;
            _rect.Height = VaC.BRICK_HEIGHT;
            _rect.X = left;
            _rect.Y = top;

            switch(type)
            {
                case 0:
                    // air
                    _texture = cm.Load<Texture2D>("000");
                    break;
                case 100:
                    // soil
                    _texture = cm.Load<Texture2D>("100");
                    break;
                case 101:
                    // ground
                    _texture = cm.Load<Texture2D>("101");
                    break;
                case 102:
                    // ground
                    _texture = cm.Load<Texture2D>("102");
                    break;
                case 200:
                    // stone
                    _texture = cm.Load<Texture2D>("200");
                    break;
                case 201:
                    // stone2
                    _texture = cm.Load<Texture2D>("201");
                    break;
                default:
                    // air
                    _texture = cm.Load<Texture2D>("000");
                    break;
            }
        }
    }

    public class Camera2D
    {
        // Position der Kamera.
        private Vector2 Position = Vector2.Zero;

        public void Update(GameTime gameTime, Vector2 position)
        {
            // Neue Position der Kamera berechnen.
            this.Position = new Vector2(-1 * position.X, -1 * position.Y);
        }

        public Matrix getMatrix()
        {
            // Position der Kamera in eine Matrix umrechnen.
            return Matrix.CreateTranslation(new Vector3(this.Position, 0));
        }
    }
}
