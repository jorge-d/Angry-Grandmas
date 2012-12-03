using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    public abstract class AAnimate : AElement
    {
        protected float _move_timer = 0f;
        protected float _move_interval = Defaults.entity_movement_interval;
        protected SpriteSheet sprite;
        protected float _scale = 1f;

        public AAnimate(EntityType type, string texture_path, int width, int height, float posx, float posy, float speed) :
            base(type, texture_path, posx, posy, speed)
        {
            Width = width;
            Height = height;
            sprite = new SpriteSheet(Defaults.MOUVEMENT_PHASE_MIDDLE, Defaults.MOUVEMENT_DIRECTION_DOWN, Width, Height);
        }

        public AAnimate(SpriteSheet sp, EntityType type, string texture_path, float posx, float posy, float speed) :
            base(type, texture_path, posx, posy, speed)
        {
            sprite = sp;
            Width = sp.getWidth();
            Height = sp.getHeight();
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, getPosition(), sprite.SourceRect, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0);
        }

        public override bool update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _move_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            sprite.update(gameTime);
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
    }
}
