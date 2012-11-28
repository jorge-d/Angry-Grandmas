using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class HumanPlayer : APlayer
    {
        HumanSpriteAnimation sprite;
        Direction _previous_state;
        Direction _current_state;

        public HumanPlayer(float posx, float posy) :
            base(Defaults.human_texture_path, posx, posy, Defaults.player_speed_x, Defaults.player_speed_y, Defaults.player_health)
        {
            Width = Defaults.player_width;
            Height = Defaults.player_height;
            sprite = new HumanSpriteAnimation(Width, Height);
        }

        public override bool update(GameTime gameTime)
        {
            sprite.update(gameTime);
            this.updateUsingKeyboard(gameTime);

            return true;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            sprite.calculate();
            spriteBatch.Draw(_texture, getPosition(), sprite.SourceRect, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        private bool canMove(int x, int y)
        {
            LinkedList<AElement> elements = new LinkedList<AElement>();
            Stage.getInstance().getIntersections(new Rectangle(x, y, Width, Height), ref elements);
            foreach (AElement elem in elements)
            {
                if (elem != this)
                    return false;
            }
            return true;
        }

        public void updateUsingKeyboard(GameTime gameTime)
        {
            Vector2 pos = getPosition();

            float newX = pos.X;
            float newY = pos.Y;

            KeyboardState kS = Keyboard.GetState();

            if (kS.IsKeyDown(Keys.A))
            {
                newX -= _speed_x;
                sprite.animateLeft();
            }
            else if (kS.IsKeyDown(Keys.D))
            {
                newX += _speed_x;
                sprite.animateRight();
            }

            if (kS.IsKeyDown(Keys.W))
            {
                newY -= _speed_y;
                sprite.animateUp();
            }
            else if (kS.IsKeyDown(Keys.S))
            {
                newY += _speed_y;
                sprite.animateDown();
            }

            if (kS.IsKeyDown(Keys.Space))
                Stage.getInstance().addElement(new Sheep(newX - 50f, newY - 50f));

            if (kS.IsKeyDown(Keys.P))
                _game.Exit();

            if (canMove((int)newX, (int)newY))
                this.setPosition(newX, newY);
        }

        private class HumanSpriteAnimation : SpriteSheet
        {
            public HumanSpriteAnimation(int spriteWidth, int spriteHeight)
                : base(0, spriteWidth, spriteHeight)
            {
            }

            public void animateRight()
            {
                //Check if the keyboard state is a new one, if it is snap straight to the standing 
                //frame for the direction. Allows quick turning
                if (m_currentState != m_previousState)
                {
                    m_currentFrame = 4;
                }

                //Check if timer is greater than interval
                if (m_timer > m_interval)
                {
                    //If is incrememnt current frame
                    m_currentFrame++;


                    //Check frame is within direction frames, if not set back to standing
                    if (m_currentFrame > 5)
                    {
                        m_currentFrame = 3;
                    }

                    //Reset timer
                    m_timer = 0f;
                }
            }

            public void animateLeft()
            {
                if (m_currentState != m_previousState)
                {
                    m_currentFrame = 7;
                }

                if (m_timer > m_interval)
                {
                    m_currentFrame++;

                    if (m_currentFrame > 8)
                    {
                        m_currentFrame = 6;
                    }
                    m_timer = 0f;
                }
            }

            public void animateDown()
            {
                if (m_currentState != m_previousState)
                {
                    m_currentFrame = 1;
                }

                if (m_timer > m_interval)
                {
                    m_currentFrame++;

                    if (m_currentFrame > 2)
                    {
                        m_currentFrame = 0;
                    }
                    m_timer = 0f;
                }
            }

            public void animateUp()
            {
                //if (m_currentState != m_previousState)
                if (m_currentFrame < 9)
                {
                    m_currentFrame = 10;
                }

                if (m_timer > m_interval)
                {
                    m_currentFrame++;

                    if (m_currentFrame > 11)
                    {
                        m_currentFrame = 9;
                    }
                    m_timer = 0f;
                }
            }

        }
    }
}
