using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    class Blood : AAnimate
    {
        public Blood(float posx, float posy) :
            base(new BloodAnimation(), EntityType.BLOOD, Defaults.blood_texture_path, posx, posy, 0)
        {   
        }

        public override bool update(GameTime gameTime)
        {
            base.update(gameTime);

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

            public override bool isAnimationOver()
            {
                if (getY() > 2)
                    return true;
                return false;
            }
        }
    }
}
