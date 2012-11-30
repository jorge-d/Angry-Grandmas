using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    public abstract class AEntity : AElement
    {
        protected SpriteSheet sprite;
        protected int _health;
        protected float _move_interval = Defaults.entity_movement_interval;
        protected float _move_timer = 0f;

        public AEntity(string texture_path, float posx, float posy, float speed, int health) :
            base(EntityType.PLAYER, texture_path, posx, posy, speed)
        {
            _health = health;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, getPosition(), sprite.SourceRect, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        public override bool update(GameTime gameTime)
        {
            _move_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            sprite.update(gameTime);
            return true;
        }

        protected bool isMoveTimerElapsed()
        {
            return (_move_timer > _move_interval);
        }

        protected bool canMove(Direction dir)
        {
            switch (dir)
            {
                case Direction.LEFT:
                    return canMove((int)(getPosition().X - _speed), (int)getPosition().Y);
                case Direction.RIGHT:
                    return canMove((int)(getPosition().X + _speed), (int)getPosition().Y);
                case Direction.UP:
                    return canMove((int)getPosition().X, (int)(getPosition().Y - _speed));
                case Direction.DOWN:
                    return canMove((int)getPosition().X, (int)(getPosition().Y - _speed));
                default:
                    throw new ArgumentException();
            }
        }

        protected void resetMoveTimer()
        {
            _move_timer = 0f;
        }

        protected bool move(float x, float y)
        {
            if (canMove((int)x, (int)y))
            {
                resetMoveTimer();
                setPosition(x, y);
                return true;
            }
            return false;
        }

        protected bool move(Direction dir)
        {
            switch (dir)
            {
                case Direction.LEFT:
                    return move(getPosition().X - _speed, getPosition().Y);
                case Direction.RIGHT:
                    return move(getPosition().X + _speed, getPosition().Y);
                case Direction.UP:
                    return move(getPosition().X, getPosition().Y - _speed);
                case Direction.DOWN:
                    return move(getPosition().X, getPosition().Y + _speed);
                default:
                    throw new ArgumentException();
            }
        }

        protected bool canMove(int x, int y)
        {
            LinkedList<AElement> elements = new LinkedList<AElement>();
            Stage.getInstance().getIntersections(new Rectangle(x, y, Width, Height), ref elements);
            foreach (AElement elem in elements)
                if (elem != this)
                {
                    EntityType type = elem.GetElementType();
                    if (type == EntityType.WALL)
                        return false;
                }
            return true;
        }
    }
}
