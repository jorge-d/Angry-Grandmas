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
        private int player_nb;

        public HumanPlayer(float posx, float posy, int nb) :
            base(Defaults.human_texture_path, Defaults.player_width, Defaults.player_height ,posx, posy, Defaults.player_speed, Defaults.player_health)
        {
            gun = new Gun(this, nb);
            player_nb = nb;
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

            if ((player_nb == 1 && kS.IsKeyDown(Keys.A)) || (player_nb == 2 && kS.IsKeyDown(Keys.Left)))
            {
                move(Direction.LEFT);
                return Direction.LEFT;
            }
            else if ((player_nb == 1 && kS.IsKeyDown(Keys.D)) || (player_nb == 2 && kS.IsKeyDown(Keys.Right)))
            {
                move(Direction.RIGHT);
                return Direction.RIGHT;
            }
            else if ((player_nb == 1 && kS.IsKeyDown(Keys.W)) || (player_nb == 2 && kS.IsKeyDown(Keys.Up)))
            {
                move(Direction.UP);
                return Direction.UP;
            }
            else if ((player_nb == 1 && kS.IsKeyDown(Keys.S)) || (player_nb == 2 && kS.IsKeyDown(Keys.Down)))
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
