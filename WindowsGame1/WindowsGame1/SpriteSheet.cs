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
        Texture2D m_texture;
        float m_timer = 0f;
        float m_interval = 100f;
        int m_currentFrame = 7;
        int m_spriteWidth = 25;
        int m_spriteHeight = 35;
        Rectangle m_sourceRect;

        KeyboardState m_currentState;
        KeyboardState m_previousState;
        Vector2 m_position;
        Vector2 m_origin;

        public SpriteSheet(Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight)
        {
            m_texture = texture;
            m_spriteWidth = spriteWidth;
            m_spriteHeight = spriteHeight;
            m_currentFrame = currentFrame;
        }

        public Vector2 Position
        {
            get { return m_position; }
            set { m_position = value; }
        }

        public Rectangle SourceRect
        {
            get { return m_sourceRect; }
            set { m_sourceRect = value; }
        }

        public Vector2 Origin
        {
            get { return m_origin; }
            set { m_origin = value; }
        }

        public Texture2D Texture
        {
            get { return m_texture; }
            set { m_texture = value; }
        }
    }
}
