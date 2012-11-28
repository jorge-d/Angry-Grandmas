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
        protected float m_interval = 100f;
        protected int m_currentFrame;
        private int m_spriteWidth;
        private int m_spriteHeight;
        private Rectangle m_sourceRect;

        protected KeyboardState m_currentState;
        protected KeyboardState m_previousState;

        public SpriteSheet(int currentFrame, int spriteWidth, int spriteHeight)
        {
            m_spriteWidth = spriteWidth;
            m_spriteHeight = spriteHeight;
            m_currentFrame = currentFrame;
        }

        public void calculate()
        {
            //Set the rectangle for drawing
            m_sourceRect = new Rectangle(m_currentFrame * m_spriteWidth, 0, m_spriteWidth, m_spriteHeight);
        }

            ////Keyboard States
            //m_previousState = m_currentState;
            //m_currentState = Keyboard.GetState();


            ////Check if no keys are pressed, if they aren't reset the frame to 
            ////the standing frame of each direction
            //if (m_currentState.GetPressedKeys().Length == 0)
            //{
            //    if (m_currentFrame >= 0 && m_currentFrame <= 2)
            //    {
            //        m_currentFrame = 1;
            //    }
            //    if (m_currentFrame >= 3 && m_currentFrame <= 5)
            //    {
            //        m_currentFrame = 4;
            //    }
            //    if (m_currentFrame >= 6 && m_currentFrame <= 8)
            //    {
            //        m_currentFrame = 7;
            //    }
            //    if (m_currentFrame >= 9 && m_currentFrame <= 11)
            //    {
            //        m_currentFrame = 10;
            //    }
            //}

        public void update(GameTime gameTime)
        {
            m_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public Rectangle SourceRect
        {
            get { return m_sourceRect; }
            set { m_sourceRect = value; }
        }
    }
}
