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

        public bool hurt(int damages)
        {
            _health -= damages;
            return isAlive();
        }

        protected bool isAlive()
        {
            if (_health <= 0)
                return false;
            return true;
        }

        protected bool isMoveTimerElapsed()
        {
            return (_move_timer > _move_interval);
        }

        protected void resetMoveTimer()
        {
            _move_timer = 0f;
        }

        protected override bool move(float x, float y)
        {
            if (canMove((int)x, (int)y))
            {
                resetMoveTimer();
                setPosition(x, y);
                return true;
            }
            return false;
        }
    }
}
