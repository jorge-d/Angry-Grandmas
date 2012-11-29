using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Cloud : ABullet
    {
        CloudSpriteAnimation sprite;

        public Cloud(HumanPlayer shooter, Direction dir, float posx, float posy) :
            base(shooter, dir, Defaults.cloud_damages, Defaults.cloud_texture, posx, posy, Defaults.cloud_speed)
        {
            Width = 32;
            Height = 32;
            sprite = new CloudSpriteAnimation();
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, getPosition(), sprite.SourceRect, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        private bool move()
        {
            Vector2 pos = getPosition();
            float x = pos.X;
            float y = pos.Y;
            switch (_direction)
            {
                case Direction.UP:
                    y -= _speed;
                    break;
                case Direction.DOWN:
                    y += _speed;
                    break;
                case Direction.LEFT:
                    x -= _speed;
                    break;
                case Direction.RIGHT:
                    x += _speed;
                    break;
            }
            setPosition(x, y);

            LinkedList<AElement> elements = new LinkedList<AElement>();
            Stage.getInstance().getIntersections(new Rectangle((int)x, (int)y, Width, Height), ref elements);
            foreach (AElement elem in elements)
                if (elem != this)
                {
                    EntityType type = elem.GetElementType();
                    if (type == EntityType.WALL)
                        return false;
                }
            return true;
        }

        public override bool update(GameTime gameTime)
        {
            sprite.update(gameTime);
            sprite.animate(_direction);
            return move();
        }

        private class CloudSpriteAnimation : SpriteSheet
        {
            private bool _up = true;

            public CloudSpriteAnimation() :
                base(0, 0, 32, 32)
            {
                m_interval /= 4;
            }

            public override void animate(Direction Dir)
            {
                if (isTimerElapsed())
                {
                    if (_up)
                        incrementY();
                    else
                        decrementY();

                    if (getY() >= 19)
                        _up = false;
                    else if (getY() <= 0)
                        _up = true;

                    resetTimer();
                }
            }
        }
    }
}
