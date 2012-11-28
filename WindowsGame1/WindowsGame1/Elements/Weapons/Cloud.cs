using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Cloud : AElement
    {
        private int _damages = Defaults.cloud_damages;
        private const float CLOUD_SPEED = 3;
        private Direction _direction;
        CloudSpriteAnimation sprite;

        public Cloud(HumanPlayer shooter, Direction dir, float posx, float posy) :
            base(EntityType.BULLET, Defaults.cloud_texture, posx, posy, CLOUD_SPEED, CLOUD_SPEED)
        {
            Width = 32;
            Height = 32;
            _direction = dir;
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
                    y -= CLOUD_SPEED;
                    break;
                case Direction.DOWN:
                    y += CLOUD_SPEED;
                    break;
                case Direction.LEFT:
                    x -= CLOUD_SPEED;
                    break;
                case Direction.RIGHT:
                    x += CLOUD_SPEED;
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
            sprite.animate();
            return move();
        }

        private class CloudSpriteAnimation : SpriteSheet
        {
            private bool up = true;

            public CloudSpriteAnimation() :
                base(0, 0, 32, 32)
            {
                m_interval /= 2;
            }

            public void animate()
            {
                if (isTimerElapsed())
                {
                    if (up)
                        incrementY();
                    else
                        decrementY();

                    if (getY() >= 19)
                        up = false;
                    else if (getY() <= 0)
                        up = true;

                    resetTimer();
                }
            }
        }
    }
}
