﻿using System;
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
        protected float _explosion_scale = 0.5f;

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
                    createExplosion();
                    return true;
                }
            return false;
        }

        protected void createExplosion()
        {
            float x = getPosition().X;
            float y = getPosition().Y;
            switch (_direction)
            {
                case Direction.LEFT:
                    x -= Width / 2;
                    break;
                case Direction.RIGHT:
                    x += Width / 2;
                    break;
                case Direction.UP:
                    y -= Width / 2;
                    break;
                case Direction.DOWN:
                    y += Width / 2;
                    break;
            }
            Stage.getInstance().addElement(new Explosion(x, y, _explosion_scale));
        }

        protected class BulletAnimation : SpriteSheet
        {
            private bool _up = true;

            public BulletAnimation() :
                base(0, 0, 25, 25)
            {
                m_interval /= 4;
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
