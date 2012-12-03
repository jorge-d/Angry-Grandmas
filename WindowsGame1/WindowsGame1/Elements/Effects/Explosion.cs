using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Explosion : AAnimate
    {
        public Explosion(float posx, float posy, float scale) :
            base(new ExplosionAnimation(), EntityType.EXPLOSION, Defaults.explosion_texture_path, posx, posy, 0)
        {
            _scale = scale;
        }

        public override bool update(GameTime gameTime)
        {
            base.update(gameTime);

            sprite.animate(Direction.NONE);

            if (sprite.isAnimationOver())
                return false;
            return true;
        }

        private class ExplosionAnimation : SpriteSheet
        {
            public ExplosionAnimation() :
                base(-1, 0, 64, 64)
            {
                m_interval = Defaults.explosion_animation_interval;
            }

            public override void animate(Direction Dir)
            {
                if (isTimerElapsed())
                {
                    incrementX();

                    if (getX() > 4)
                    {
                        setX(0);
                        incrementY();
                    }
                    resetTimer();
                }
            }

            public override bool isAnimationOver()
            {
                if (getY() > 4)
                    return true;
                return false;
            }
        }
    }
}
