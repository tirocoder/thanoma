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
    public class Level
    {
        /* START: properties */

        Texture2D _background;
        Rectangle _rect_background;
        public int[,] _tilemap = new int[16, VaC.LEVEL_WIDTH / VaC.BRICK_WIDTH];

        /* END: properties */

        // constructor
        public Level()
        {
            LoadLevel1();
        }

        /* START: methods */

        public void LoadLevel1()
        {
            // read level file
            StreamReader sr = new StreamReader(@"C:\thanoma\level1.txt");
            string level_data = "";
            for (int i = 0; !sr.EndOfStream; i++ )
            {
                level_data = sr.ReadLine();
                string[] line = level_data.Split(' ');
                for (int i2 = 0; i2 < line.Length; i2++)
                {
                    _tilemap[i, i2] = Convert.ToInt32(line[i2]);
                }
            }
            sr.Close();
        }

        public void DrawLevel1(ContentManager cm, SpriteBatch sb)
        {
            _background = cm.Load<Texture2D>("bg1");

            _rect_background.Width = _background.Width;
            _rect_background.Height = _background.Height;

            sb.Draw(_background, _rect_background, Color.White);

            for (int i = 0; i < _tilemap.GetLength(1); i++)
            {
                for (int i2 = 0; i2 < 16; i2++)
                {
                    if (_tilemap[i2, i] != 0)
                    {
                        Brick brick = new Brick(cm, i2 * VaC.BRICK_HEIGHT, i * VaC.BRICK_WIDTH, _tilemap[i2, i]);
                        sb.Draw(brick._texture, brick._rect, Color.White);
                    }
                }
            }
        }

        public int IsBrickUnderMe(int brick_x, int brick_y, int x, int y)
        {
            try
            {
                Rectangle rect_brick = new Rectangle(brick_x * VaC.BRICK_WIDTH, (brick_y + 1) * VaC.BRICK_HEIGHT - 2, VaC.BRICK_WIDTH, VaC.BRICK_HEIGHT);
                Rectangle rect_player = new Rectangle(x, y, VaC.PLAYER_WIDTH, VaC.PLAYER_HEIGHT);

                if (rect_player.Intersects(rect_brick) == true)
                {
                    if (_tilemap[brick_y + 1, brick_x] > 20) return 0;
                    else
                    {
                        if ((rect_player.Left % VaC.BRICK_WIDTH != 0) && (_tilemap[brick_y + 1, brick_x + 1] > 20))
                        {
                            return 0;
                        }
                        int value = 0;
                        for (int i = (int)(y / VaC.BRICK_HEIGHT) + 1; i < 15; i++)
                        {
                            if ((_tilemap[i, (int)(x / VaC.BRICK_WIDTH)] > 20) || (_tilemap[i, (int)((x + VaC.BRICK_WIDTH) / VaC.BRICK_WIDTH)] > 20))
                            {
                                value = i * VaC.BRICK_HEIGHT;
                                break;
                            }
                        }
                        return value - (y + VaC.PLAYER_HEIGHT);
                    }
                }
                else
                {
                    int value = 0;
                    for (int i = (int)(y / VaC.BRICK_HEIGHT) + 1; i < 15; i++)
                    {
                        if (rect_player.Left % VaC.BRICK_WIDTH != 0)
                        {
                            if ((_tilemap[i, (int)(x / VaC.BRICK_WIDTH)] > 20) || (_tilemap[i, (int)((x + VaC.BRICK_WIDTH) / VaC.BRICK_WIDTH)] > 20))
                            {
                                value = i * VaC.BRICK_HEIGHT;
                                break;
                            }
                        }
                    }
                    return value - (y + VaC.PLAYER_HEIGHT);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int IsBrickAboveMe(int brick_x, int brick_y, int x, int y)
        {
            try
            {
                Rectangle rect_brick = new Rectangle(brick_x * VaC.BRICK_WIDTH, (brick_y - 1) * VaC.BRICK_HEIGHT, VaC.BRICK_WIDTH, VaC.BRICK_HEIGHT);
                Rectangle rect_player = new Rectangle(x, y, VaC.PLAYER_WIDTH, VaC.PLAYER_HEIGHT);

                if (rect_player.Intersects(rect_brick) == true)
                {
                    if (_tilemap[brick_y - 1, brick_x] > 20) return 0;
                    else
                    {
                        if ((rect_player.Left % VaC.BRICK_WIDTH != 0) && (_tilemap[brick_y - 1, brick_x + 1] > 20))
                        {
                            return 0;
                        }
                        int value = 0;
                        for (int i = (int)(y / VaC.BRICK_HEIGHT) - 1; i > 0; i--)
                        {
                            if ((_tilemap[i, (int)(x / VaC.BRICK_WIDTH)] > 20) || (_tilemap[i, (int)((x + VaC.BRICK_WIDTH) / VaC.BRICK_WIDTH)] > 20))
                            {
                                value = i * VaC.BRICK_HEIGHT;
                                break;
                            }
                        }
                        return y - (value + VaC.BRICK_HEIGHT);
                    }
                }
                else
                {
                    int value = 0;
                    for (int i = (int)(y / VaC.BRICK_HEIGHT) - 1; i > 0; i--)
                    {
                        if (rect_player.Left % VaC.BRICK_WIDTH != 0)
                        {
                            if ((_tilemap[i, (int)(x / VaC.BRICK_WIDTH)] > 20) || (_tilemap[i, (int)((x + VaC.BRICK_WIDTH) / VaC.BRICK_WIDTH)] > 20))
                            {
                                value = i * VaC.BRICK_HEIGHT;
                                break;
                            }
                        }
                    }
                    return y - (value + VaC.BRICK_HEIGHT);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        
        public int IsBrickToMyRight(int brick_x, int brick_y, int x, int y)
        {
            brick_x = (int)((double)x / VaC.BRICK_WIDTH);
            brick_y = (int)((double)y / VaC.BRICK_HEIGHT);

            try
            {
                int pixels = (brick_x + 2) * VaC.BRICK_WIDTH - x - VaC.PLAYER_WIDTH;    // horizontal distance to next brick

                if (y % VaC.BRICK_HEIGHT != 0)
                {
                    if (_tilemap[brick_y + 1, brick_x + 2] > 20)
                    {
                        return 0;
                    }
                }

                
                if((pixels < 3)&&(_tilemap[brick_y, brick_x + 2] > 20))   // if player is in front of unpassable block
                {
                    return 0;
                }
                else if ((pixels < 3) && (brick_x != 0) && (_tilemap[brick_y + 1, brick_x + 2] > 20) && (y % VaC.BRICK_HEIGHT != 0))
                {
                    return 0;
                }
                else return pixels + VaC.BRICK_WIDTH;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int IsBrickToMyLeft(int brick_x, int brick_y, int x, int y)
        {
            brick_y = (int)((double)y / VaC.BRICK_HEIGHT);

            try
            {
                int pixels = x - brick_x * VaC.BRICK_WIDTH;     // horizontal distance to next brick
                if ((pixels < 3) && (brick_x != 0) && (_tilemap[brick_y + 1, brick_x - 1] > 20) && (y % VaC.BRICK_HEIGHT != 0))
                {
                    return 0;
                }
                else if ((pixels < 3) && (brick_x != 0) && (_tilemap[brick_y, brick_x - 1] > 20))  // if player is in front of unpassable block
                {
                    return 0;
                }
                else return pixels + VaC.BRICK_WIDTH;
            }
            catch (Exception ex)
            {
                return 0;
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

            try
            {
                string bricktype="";
                if (type.ToString().Length == 3) bricktype = type.ToString();
                else if (type.ToString().Length == 2) bricktype = "0" + type.ToString();
                else bricktype = "00" + type.ToString();
                _texture = cm.Load<Texture2D>("img/bricks/" + bricktype);
            }
            catch (Exception ex)
            {
                _texture = cm.Load<Texture2D>("img/bricks/000");
            }

            //switch(type)
            //{
            //    case 0:
            //        // air
            //        _texture = cm.Load<Texture2D>("000");
            //        break;
            //    case 100:
            //        // soil
            //        _texture = cm.Load<Texture2D>("100");
            //        break;
            //    case 101:
            //        // ground
            //        _texture = cm.Load<Texture2D>("101");
            //        break;
            //    case 102:
            //        // ground
            //        _texture = cm.Load<Texture2D>("102");
            //        break;
            //    case 103:
            //        // ground
            //        _texture = cm.Load<Texture2D>("103");
            //        break;
            //    case 200:
            //        // stone
            //        _texture = cm.Load<Texture2D>("200");
            //        break;
            //    case 201:
            //        // stone2
            //        _texture = cm.Load<Texture2D>("201");
            //        break;
            //    default:
            //        // air
            //        _texture = cm.Load<Texture2D>("000");
            //        break;
            //}
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
