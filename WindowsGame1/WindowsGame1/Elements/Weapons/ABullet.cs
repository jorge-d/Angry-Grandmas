using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    abstract class ABullet : AElement
    {
        protected HumanPlayer _player;
        protected Direction _direction;
        protected int _damages;

        public ABullet(HumanPlayer shooter, Direction dir, int damages, string path, float posx, float posy, float speed) :
            base(EntityType.BULLET, path, posx, posy, speed)
        {
            _speed = speed;
            _damages = damages;
            _direction = dir;
            _player = shooter;
        }

        protected override bool move(float x, float y)
        {
            if (!canMove((int)x, (int)y))
                return false;
            setPosition(x, y);
            return true;
        }

        protected bool hitsPlayer()
        {
            foreach (AElement elem in getOverlapingElements())
                if (elem.GetElementType() == EntityType.PLAYER && elem != _player)
                {
                    ((AEntity)elem).hurt(_damages);
                    return true;
                }
            return false;
        }
    }
}
