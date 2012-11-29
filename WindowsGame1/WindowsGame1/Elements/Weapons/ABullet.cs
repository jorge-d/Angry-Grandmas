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
        protected float _speed;

        public ABullet(HumanPlayer shooter, Direction dir, int damages, string path, float posx, float posy, float speed) :
            base(EntityType.BULLET, path, posx, posy, speed)
        {
            _speed = speed;
            _damages = damages;
            _direction = dir;
            _player = shooter;
        }
    }
}
