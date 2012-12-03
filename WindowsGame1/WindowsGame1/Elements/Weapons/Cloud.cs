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
        BulletAnimation sprite;

        public Cloud(HumanPlayer shooter, Direction dir, float posx, float posy) :
            base(shooter, dir, Defaults.cloud_damages, Defaults.cloud_texture_path, posx, posy, Defaults.cloud_speed)
        {
            Width = 25;
            Height = 25;
            sprite = new BulletAnimation();
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, getPosition(), sprite.SourceRect, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        public override bool update(GameTime gameTime)
        {
            if (hitsPlayer())
                return false;
            sprite.update(gameTime);
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
