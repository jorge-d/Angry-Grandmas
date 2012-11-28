using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;

namespace WindowsGame1
{
    class SpriteSheet
    {
        protected float m_timer = 0f;
        protected float m_interval = Defaults.animation_movement_interval;
        protected int x;
        protected int y;
        private int m_spriteWidth;
        private int m_spriteHeight;
        private Rectangle m_sourceRect;

        public SpriteSheet(int frame_x, int frame_y, int spriteWidth, int spriteHeight)
        {
            m_spriteWidth = spriteWidth;
            m_spriteHeight = spriteHeight;
            x = frame_x;
            y = frame_y;
        }

        private void updateSourceRec()
        {
            m_sourceRect = new Rectangle(x * m_spriteWidth, y * m_spriteHeight, m_spriteWidth, m_spriteHeight);
        }

        protected int getX() { return x; }
        protected int getY() { return y; }
        protected void setX(int frame_x)
        {
            x = frame_x;
        }
        protected void incrementX()
        {
            x++;
        }
        protected void decrementX()
        {
            x--;
        }
        protected void setY(int frame_y)
        {
             y = frame_y;
        }
        protected void incrementY()
        {
            y++;
        }
        protected void decrementY()
        {
            y--;
        }

        protected void resetTimer() { m_timer = 0f; }
        protected bool isTimerElapsed() { return (m_timer > m_interval); }
        
        public void update(GameTime gameTime) { m_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; }
        public Rectangle SourceRect
        {
            get {
                updateSourceRec();
                return m_sourceRect;
            }
            set { m_sourceRect = value; }
        }
    }
}
