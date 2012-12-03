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
    public class SpriteSheet
    {
        protected float m_timer = 0f;
        protected float m_interval = Defaults.animation_movement_interval;
        protected int x;
        protected int y;
        private int m_frameWidth;
        private int m_frameHeight;
        private int m_spriteWidth;
        private int m_spriteHeight;
        private Rectangle m_sourceRect;
        protected Direction _previous_state = Direction.NONE;
        protected Direction _current_state = Direction.DOWN;

        public SpriteSheet(int frame_x, int frame_y, int spriteWidth, int spriteHeight)
        {
            m_frameWidth = m_spriteWidth = spriteWidth;
            m_frameHeight = m_spriteHeight = spriteHeight;
            x = frame_x;
            y = frame_y;
        }

        public int getWidth() { return m_spriteWidth; }
        public int getHeight() { return m_spriteHeight; }

        public virtual void animate(Direction dir)
        {
            _previous_state = _current_state;

            // Avoid having a direction NONE because it could be harmfull
            // when we try to know where he's looking
            if (dir != Direction.NONE)
                _current_state = dir;

            // Avoid non-smooth transitions
            if (_current_state != _previous_state)
                resetMovement();

            switch (dir)
            {
                case Direction.LEFT:
                    move(Defaults.MOUVEMENT_DIRECTION_LEFT);
                    break;
                case Direction.RIGHT:
                    move(Defaults.MOUVEMENT_DIRECTION_RIGHT);
                    break;
                case Direction.UP:
                    move(Defaults.MOUVEMENT_DIRECTION_UP);
                    break;
                case Direction.DOWN:
                    move(Defaults.MOUVEMENT_DIRECTION_DOWN);
                    break;
                default:
                    resetMovement();
                    break;
            }
        }

        private void resetMovement()
        {
            setX(Defaults.MOUVEMENT_PHASE_MIDDLE);
        }

        private void move(int y)
        {
            setY(y);
            if (isTimerElapsed())
            {
                resetTimer();
                incrementX();
                if (getX() > Defaults.MOUVEMENT_PHASE_END)
                    setX(Defaults.MOUVEMENT_PHASE_BEGIN);
            }
        }

        public Direction getLookingDirection()
        {
            return _current_state;
        }

        private void updateSourceRec()
        {
            m_sourceRect = new Rectangle(x * m_frameWidth, y * m_frameHeight, m_spriteWidth, m_spriteHeight);
        }

        public void setSpriteDimensions(int width, int height)
        {
            m_spriteWidth = width;
            m_spriteHeight = height;
        }

        public int getX() { return x; }
        public int getY() { return y; }
        public void setX(int frame_x) { x = frame_x; }
        protected void incrementX() { x++; }
        protected void decrementX() { x--; }
        public void setY(int frame_y) { y = frame_y; }
        protected void incrementY() { y++; }
        protected void decrementY() { y--; }
        protected void resetTimer() { m_timer = 0f; }
        protected bool isTimerElapsed() { return (m_timer > m_interval); }
        public void update(GameTime gameTime) { m_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; }
        public virtual bool isAnimationOver() { return false; }

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
