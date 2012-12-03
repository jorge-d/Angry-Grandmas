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
        SpriteSheet sprite;

        public Fireball(HumanPlayer shooter, Direction dir, float posx, float posy) :
            base(shooter, dir, Defaults.fireball_damages, Defaults.fireball_texture_path, posx, posy, Defaults.fireball_speed)
        {
            Width = 32;
            Height = 32;
            sprite = new SpriteSheet(Defaults.MOUVEMENT_PHASE_MIDDLE, Defaults.MOUVEMENT_DIRECTION_DOWN, Width, Height);
        }
        
        public override bool update(GameTime gameTime)
        {
            hitsPlayer();
            sprite.update(gameTime);
            return move();
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, getPosition(), sprite.SourceRect, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        private bool move()
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
