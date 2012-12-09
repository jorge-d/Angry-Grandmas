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
        public Cloud(HumanPlayer shooter, Direction dir, float posx, float posy) :
            base(new CloudAnimation(), shooter, dir, Defaults.cloud_damages, Defaults.cloud_texture_path, posx, posy, Defaults.cloud_speed)
        {
        }

        public override bool update(GameTime gameTime)
        {
            base.update(gameTime);
            sprite.animate(_direction);

            if (!move(_direction))
                createExplosion();
            else if (!hitsPlayer())
                return true;
            return false;
        }

        protected class CloudAnimation : SpriteSheet
        {
            private bool _up = true;

            public CloudAnimation() :
                base(0, 0, 25, 25)
            {
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
