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

        int[,] _tilemap = new int[16, VaC.LEVEL_WIDTH / VaC.BRICK_WIDTH];

        /* END: properties */

        // constructor
        public Level()
        {
            LoadLevel1();
        }

        /* START: methods */

        //public void BuildTestLevel(ContentManager cm, SpriteBatch sb, int start_x)
        //{
        //    for (int i = 0; i < VaC.LEVEL_WIDTH / VaC.BRICK_WIDTH; i++)
        //    {
        //        Brick brick7 = new Brick(cm, 9 * VaC.BRICK_HEIGHT, start_x + i * VaC.BRICK_WIDTH, _tilemap[9, i]);
        //        sb.Draw(brick7._texture, brick7._rect, Color.White);
        //        Brick brick8 = new Brick(cm, 10 * VaC.BRICK_HEIGHT, start_x + i * VaC.BRICK_WIDTH, _tilemap[10, i]);
        //        sb.Draw(brick8._texture, brick8._rect, Color.White);
        //        Brick brick6 = new Brick(cm, 8 * VaC.BRICK_HEIGHT, start_x + i * VaC.BRICK_WIDTH, _tilemap[8, i]);
        //        sb.Draw(brick6._texture, brick6._rect, Color.White);
        //        Brick brick = new Brick(cm, 11 * VaC.BRICK_HEIGHT, start_x + i * VaC.BRICK_WIDTH, _tilemap[11,i]);
        //        sb.Draw(brick._texture, brick._rect, Color.White);
        //        Brick brick2 = new Brick(cm, 12 * VaC.BRICK_HEIGHT, start_x + i * VaC.BRICK_WIDTH, _tilemap[12, i]);
        //        sb.Draw(brick2._texture, brick2._rect, Color.White);
        //        Brick brick3 = new Brick(cm, 13 * VaC.BRICK_HEIGHT, start_x + i * VaC.BRICK_WIDTH, _tilemap[13, i]);
        //        sb.Draw(brick3._texture, brick3._rect, Color.White);
        //        Brick brick4 = new Brick(cm, 14 * VaC.BRICK_HEIGHT, start_x + i * VaC.BRICK_WIDTH, _tilemap[14, i]);
        //        sb.Draw(brick4._texture, brick4._rect, Color.White);
        //        Brick brick5 = new Brick(cm, 15 * VaC.BRICK_HEIGHT, start_x + i * VaC.BRICK_WIDTH, _tilemap[15, i]);
        //        sb.Draw(brick5._texture, brick5._rect, Color.White);
                
        //    }
        //}

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
                    if (_tilemap[brick_y + 1, brick_x] != 0) return 0;
                    else
                    {
                        if ((rect_player.Left % VaC.BRICK_WIDTH != 0) && (_tilemap[brick_y + 1, brick_x + 1] != 0))
                        {
                            return 0;
                        }
                        int value = 0;
                        for (int i = (int)(y / VaC.BRICK_HEIGHT) + 1; i < 15; i++)
                        {
                            if ((_tilemap[i, (int)(x / VaC.BRICK_WIDTH)] != 0) || (_tilemap[i, (int)((x + VaC.BRICK_WIDTH) / VaC.BRICK_WIDTH)] != 0))
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
                            if ((_tilemap[i, (int)(x / VaC.BRICK_WIDTH)] != 0) || (_tilemap[i, (int)((x + VaC.BRICK_WIDTH) / VaC.BRICK_WIDTH)] != 0))
                            {
                                value = i * VaC.BRICK_HEIGHT;
                                break;
                            }
                        }
                    }
                    return value - (y + VaC.PLAYER_HEIGHT);
                }




                //if ((_tilemap[brick_y + 1, (int)(x / VaC.BRICK_WIDTH)] != 0) || (_tilemap[brick_y + 1, (int)((x + VaC.BRICK_WIDTH) / VaC.BRICK_WIDTH)] != 0))
                //{
                //    return 0;
                //}
                //else
                //{
                //    int value = 0;
                //    for (int i = (int)(y / VaC.BRICK_HEIGHT) + 1; i<15 ; i++)
                //    {
                //        if ((_tilemap[i, (int)(x / VaC.BRICK_WIDTH)] != 0) || (_tilemap[i, (int)((x + VaC.BRICK_WIDTH) / VaC.BRICK_WIDTH)] != 0))
                //        {
                //            value = i * VaC.BRICK_HEIGHT;
                //            break;
                //        }
                //    }
                //    return value - (y + VaC.PLAYER_HEIGHT);
                //}
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
                    if (_tilemap[brick_y - 1, brick_x] != 0) return 0;
                    else
                    {
                        if ((rect_player.Left % VaC.BRICK_WIDTH != 0) && (_tilemap[brick_y - 1, brick_x + 1] != 0))
                        {
                            return 0;
                        }
                        int value = 0;
                        for (int i = (int)(y / VaC.BRICK_HEIGHT) - 1; i > 0; i--)
                        {
                            if ((_tilemap[i, (int)(x / VaC.BRICK_WIDTH)] != 0) || (_tilemap[i, (int)((x + VaC.BRICK_WIDTH) / VaC.BRICK_WIDTH)] != 0))
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
                            if ((_tilemap[i, (int)(x / VaC.BRICK_WIDTH)] != 0) || (_tilemap[i, (int)((x + VaC.BRICK_WIDTH) / VaC.BRICK_WIDTH)] != 0))
                            {
                                value = i * VaC.BRICK_HEIGHT;
                                break;
                            }
                        }
                    }
                    return y - (value + VaC.BRICK_HEIGHT);
                }




                //if ((_tilemap[brick_y, (int)(x / VaC.BRICK_WIDTH)] != 0) || (_tilemap[brick_y, (int)((x + VaC.BRICK_WIDTH) / VaC.BRICK_WIDTH)] != 0))
                //{
                //    return 0;
                //}
                //else
                //{
                //    int value = 0;
                //    for (int i = (int)(y / VaC.BRICK_HEIGHT) - 1; i > 0; i--)
                //    {
                //        if ((_tilemap[i, (int)(x / VaC.BRICK_WIDTH)] != 0) || (_tilemap[i, (int)((x + VaC.BRICK_WIDTH) / VaC.BRICK_WIDTH)] != 0))
                //        {
                //            value = i * VaC.BRICK_HEIGHT + VaC.BRICK_HEIGHT;
                //            break;
                //        }
                //    }
                //    return (y + VaC.PLAYER_HEIGHT) - value;
                //}
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        // NEXT STEP
        public int IsBrickToMyRight(int brick_x, int brick_y, int x, int y)
        {
            try
            {
                int pixels = (brick_x + 1) * VaC.BRICK_WIDTH - x - VaC.PLAYER_WIDTH;    // horizontal distance to next brick
                if((pixels<3)&&(_tilemap[brick_y, brick_x+1]!=0))   // if player is in front of unpassable block
                {
                    return 0;
                }
                else if ((pixels < 3) && (brick_x != 0) && (_tilemap[brick_y + 1, brick_x + 1] != 0) && (y % VaC.BRICK_HEIGHT != 0))
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
            try
            {
                int pixels = x - brick_x * VaC.BRICK_WIDTH;     // horizontal distance to next brick
                if ((pixels < 3) && (brick_x != 0) && (_tilemap[brick_y + 1, brick_x - 1] != 0) && (y % VaC.BRICK_HEIGHT != 0))
                {
                    return 0;
                }
                else if ((pixels < 3) && (brick_x != 0) && (_tilemap[brick_y, brick_x - 1] != 0))  // if player is in front of unpassable block
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
                case 103:
                    // ground
                    _texture = cm.Load<Texture2D>("103");
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
