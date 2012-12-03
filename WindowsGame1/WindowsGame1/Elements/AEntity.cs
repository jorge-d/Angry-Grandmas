using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    public abstract class AEntity : AAnimate
    {
        protected int _health;

        public AEntity(string texture_path, int width, int height, float posx, float posy, float speed, int health) :
            base(EntityType.PLAYER, texture_path, width, height, posx, posy, speed)
        {
            _health = health;
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
