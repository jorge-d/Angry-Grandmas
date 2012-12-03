using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    class Blood : AElement
    {
        BloodAnimation sprite;
        private float _scale;

        public Blood(float posx, float posy) :
            base(EntityType.BLOOD, Defaults.blood_texture_path, posx, posy, 0)
        {   
            _scale = 1f;
            sprite = new BloodAnimation();
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, getPosition(), sprite.SourceRect, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0);
        }

        public override bool update(GameTime gameTime)
        {
            sprite.update(gameTime);
            sprite.animate(Direction.NONE);

            if (sprite.isAnimationOver())
                return false;
            return true;
        }

        private class BloodAnimation : SpriteSheet
        {
            public BloodAnimation() :
                base(-1, 0, 60, 60)
            {
                m_interval = Defaults.blood_animation_interval;
            }

            public override void animate(Direction Dir)
            {
                if (isTimerElapsed())
                {
                    incrementX();

                    if (getX() > 3)
                    {
                        setX(0);
                        incrementY();
                    }
                    resetTimer();
                }
            }

            public bool isAnimationOver()
            {
                if (getY() > 2)
                    return true;
                return false;
            }
        }
    }
}
