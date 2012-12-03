using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Fireball : ABullet 
    {
        public Fireball(HumanPlayer shooter, Direction dir, float posx, float posy) :
            base(shooter, dir, 32, 32, Defaults.fireball_damages, Defaults.fireball_texture_path, posx, posy, Defaults.fireball_speed)
        {
        }
        
        public override bool update(GameTime gameTime)
        {
            base.update(gameTime);

            if (isMoveTimerElapsed())
            {
                hitsPlayer();
                return move();
            }
            return true;
        }

        protected bool move()
        {
            sprite.animate(_direction);
            if (!move(_direction))
            {
                createExplosion();
                return false;
            }
            return true;
        }
    }
}
