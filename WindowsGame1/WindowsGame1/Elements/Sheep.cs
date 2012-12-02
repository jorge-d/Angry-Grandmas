using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    public class Sheep : AEntity
    {
        Direction _dir;

         public Sheep(float posx, float posy) :
            base(Defaults.getSheepRandomTexture(), posx, posy, Defaults.sheep_speed, Defaults.sheep_health)
        {
            Width = 32;
            Height = 31;
            sprite = new SpriteSheet(Defaults.MOUVEMENT_PHASE_MIDDLE, Defaults.MOUVEMENT_DIRECTION_DOWN, Width, Height);
            pickRandomDirection();
        }

         private void pickRandomDirection()
         {
             Random r = new Random();
             _dir = (Direction)r.Next(4) + 1;
         }

        public override bool update(GameTime gameTime)
        {
            base.update(gameTime);

            if (isMoveTimerElapsed())
                moveSheep();

            if (!isAlive())
            {
                _stage.addElement(new Blood(getPosition().X, getPosition().Y));
                return false;
            }
            return true;
        }

        private bool moveSheep()
        {
            sprite.animate(_dir);
            if (!move(_dir))
                pickRandomDirection();
            return true;
        }

    }
}
