using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class HumanPlayer : AEntity
    {
        protected float shoot_timer = 0f;

        public HumanPlayer(float posx, float posy) :
            base(Defaults.human_texture_path, Defaults.player_width, Defaults.player_height ,posx, posy, Defaults.player_speed, Defaults.player_health)
        {
        }

        public override bool update(GameTime gameTime)
        {
            base.update(gameTime);
            updateUsingKeyboard(gameTime);
            return true;
        }

        private bool isShootTimerElapsed()
        {
            return (shoot_timer > Defaults.cloud_shoot_interval);
        }

        private void shoot(GameTime gameTime, KeyboardState kS)
        {
            shoot_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Direction looking_at = sprite.getLookingDirection();

            if (kS.IsKeyDown(Keys.Space) && isShootTimerElapsed())
            {
                shoot_timer = 0f;
                Stage.getInstance().addElement(new Fireball(this, looking_at, getPosition().X, getPosition().Y));
            }
            if (kS.IsKeyDown(Keys.O) && isShootTimerElapsed())
            {
                shoot_timer = 0f;
                Stage.getInstance().addElement(new Cloud(this, looking_at, getPosition().X, getPosition().Y));
            }
        }

        private Direction moveUsingKeyboard(KeyboardState kS)
        {
            if (kS.IsKeyDown(Keys.P))
                _game.Exit();

            if (kS.IsKeyDown(Keys.A))
            {
                move(Direction.LEFT);
                return Direction.LEFT;
            }
            else if (kS.IsKeyDown(Keys.D))
            {
                move(Direction.RIGHT);
                return Direction.RIGHT;
            }
            else if (kS.IsKeyDown(Keys.W))
            {
                move(Direction.UP);
                return Direction.UP;
            }
            else if (kS.IsKeyDown(Keys.S))
            {
                move(Direction.DOWN);
                return Direction.DOWN;
            }
            return Direction.NONE;
        }

        private void updateUsingKeyboard(GameTime gameTime)
        {
            KeyboardState kS = Keyboard.GetState();
            if (isMoveTimerElapsed())
            {
                Direction dir = moveUsingKeyboard(kS);
                sprite.animate(dir);
            }
            shoot(gameTime, kS);
        }
    }
}
