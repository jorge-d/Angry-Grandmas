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
        private bool bleeding = false;
        private float bleedingTimer;
        private float cleeping;
        private int score = 0;
        private Vector2 fontPos;
        private Vector2 fontPos2;

        public HumanPlayer(float posx, float posy, int nb) :
            base(Defaults.humanTexturePath(nb), Defaults.player_width, Defaults.player_height ,posx, posy, Defaults.player_speed, Defaults.player_health)
        {
            gun = new Gun(this, nb);
            player_nb = nb;

            createFontPos(nb);             
        }

        private void createFontPos(int nb)
        {
            int y = Defaults.stage_square_nb_y * Defaults.stage_square_size;
            int x = 120;

            if (nb == 2)
                x = ((Defaults.window_size_y / 4) * 3) + 80;
            fontPos = new Vector2(x, y);

            if (nb == 1)
                x = 15;
            else
                x = Defaults.window_size_x - 100;
            fontPos2 = new Vector2(x, y);
        }

        private void updateBleedingTimer(GameTime gameTime)
        {
            bleedingTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            cleeping += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (bleedingTimer < 0f)
            {
                bleeding = false;
                _color = Color.White;
            }
            else if (cleeping > 250)
            {
                if (_color == Color.Red)
                    _color = Color.White;
                else
                    _color = Color.Red;
                cleeping = 0;
            }
        }

        public bool isBleeding()
        {
            return bleeding;
        }

        public override bool update(GameTime gameTime)
        {
            base.update(gameTime);
            if (isBleeding())
                updateBleedingTimer(gameTime);
            updateUsingKeyboard(gameTime);
            gun.update(gameTime, sprite.getLookingDirection());
            return true;
        }

        protected override bool removeLife(int damages)
        {
            if (bleeding == false)
            {
                base.removeLife(damages);
                _color = Color.Red;
                bleeding = true;
                cleeping = 0f;
                bleedingTimer = Defaults.time_before_recovering_from_bleeding;
                return true;
            }
            return false;
        } 

        private void updateUsingKeyboard(GameTime gameTime)
        {
            if (!isMoveTimerElapsed())
                return;

            KeyboardState kS = Keyboard.GetState();
            Direction dir = Direction.NONE;

            if ((player_nb == 1 && kS.IsKeyDown(Keys.A)) || (player_nb == 2 && kS.IsKeyDown(Keys.Left)))
                dir = Direction.LEFT;
            else if ((player_nb == 1 && kS.IsKeyDown(Keys.D)) || (player_nb == 2 && kS.IsKeyDown(Keys.Right)))
                dir = Direction.RIGHT;
            else if ((player_nb == 1 && kS.IsKeyDown(Keys.W)) || (player_nb == 2 && kS.IsKeyDown(Keys.Up)))
                dir = Direction.UP;
            else if ((player_nb == 1 && kS.IsKeyDown(Keys.S)) || (player_nb == 2 && kS.IsKeyDown(Keys.Down)))
                dir = Direction.DOWN;

            if (dir != Direction.NONE)
                move(dir);
            sprite.animate(dir);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            string msg;

            if (bleeding)
                spriteBatch.DrawString(_game.font, "HURT", fontPos2, _color);

            if (player_nb == 1)
                msg = score + " - " + gun.getWeaponName();
            else
                msg = gun.getWeaponName() + " - " + score;
            spriteBatch.DrawString(_game.font, msg, fontPos, Color.Black);
        }
    }
}
