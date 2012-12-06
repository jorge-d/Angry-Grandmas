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
        static private Random r = new Random();
        static public int sheep_instances = 0;

         public Sheep(float posx, float posy) :
            base(Defaults.getSheepRandomTexture(), 32, 31,  posx, posy, Defaults.sheep_speed, Defaults.sheep_health)
        {
            pickRandomDirection();
            sheep_instances++;
        }

         private void pickRandomDirection()
         {
             _direction = (Direction)r.Next(4) + 1;
         }

        public override bool update(GameTime gameTime)
        {
            base.update(gameTime);

            if (isMoveTimerElapsed())
                moveSheep();

            if (!isAlive())
            {
                _stage.addElement(new Blood(getPosition().X - 16, getPosition().Y - 16));
                sheep_instances--;
                return false;
            }
            return true;
        }

        private bool moveSheep()
        {
            sprite.animate(_direction);
            if (!move(_direction))
                pickRandomDirection();
            return true;
        }

    }
}
