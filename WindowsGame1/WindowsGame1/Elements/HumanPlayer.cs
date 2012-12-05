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
        private Gun gun;

        public HumanPlayer(float posx, float posy) :
            base(Defaults.human_texture_path, Defaults.player_width, Defaults.player_height ,posx, posy, Defaults.player_speed, Defaults.player_health)
        {
            gun = new Gun(this);
        }

        public override bool update(GameTime gameTime)
        {
            base.update(gameTime);

            updateUsingKeyboard(gameTime);
            gun.update(gameTime, sprite.getLookingDirection());
            return true;
        }

        private Direction moveUsingKeyboard()
        {
            KeyboardState kS = Keyboard.GetState();

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
            if (isMoveTimerElapsed())
            {
                Direction dir = moveUsingKeyboard();
                sprite.animate(dir);
            }
        }
    }
}
