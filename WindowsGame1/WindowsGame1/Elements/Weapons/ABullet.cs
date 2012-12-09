using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    abstract class ABullet : AAnimate
    {
        protected HumanPlayer _player;
        protected int _damages;
        protected float _explosion_scale = 0.5f;

        public ABullet(HumanPlayer shooter, Direction dir, int width, int height, int damages, string path, float posx, float posy, float speed) :
            base(EntityType.BULLET, path, width, height, posx, posy, speed)
        {
            _damages = damages;
            _direction = dir;
            _player = shooter;
        }

        public ABullet(SpriteSheet sp, HumanPlayer shooter, Direction dir, int damages, string path, float posx, float posy, float speed) :
            base(sp, EntityType.BULLET, path, posx, posy, speed)
        {
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

        protected bool hitsPlayer(bool explode)
        {
            // PLAYER can be a sheep OR a player
            foreach (AElement elem in getOverlapingElements())
                if (elem.GetElementType() == EntityType.PLAYER && elem != _player)
                {
                    AEntity e = (AEntity)elem;

                    // The player is bleeding and cannot be attacked for the moment
                    if (!e.hurt(_damages))
                        return false;

                    if (e.isAlive()) // A player or a sheep has been hit
                        _player.incrementScore(Defaults.SCORE_ON_HIT);
                    else // The target was killed - It was a sheep
                        _player.incrementScore(Defaults.SCORE_ON_DEATH);
                    if (explode)
                        createExplosion();
                    else if (e is HumanPlayer)
                        _game.player_hit_sound.Play();
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
    }
}
