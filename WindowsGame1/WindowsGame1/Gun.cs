using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class Gun
    {
        HumanPlayer _player;

        private enum weapon { CLOUD = 0, FIREBALL }
        private float shoot_timer = 0f;
        private float change_weapon_timer = 0f;
        private weapon currentWeapon = weapon.CLOUD;

        public Gun(HumanPlayer p)
        {
            _player = p;
        }

        public void update(GameTime gameTime, Direction direction)
        {
            shoot_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            change_weapon_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            shoot(direction);
        }

        public void shoot(Direction dir)
        {
            KeyboardState kS = Keyboard.GetState();

            if (kS.IsKeyDown(Keys.Space) && isShootTimerElapsed())
            {
                if (currentWeapon == weapon.CLOUD)
                    Stage.getInstance().addElement(new Cloud(_player, dir, _player.getPosition().X, _player.getPosition().Y));
                else if (currentWeapon == weapon.FIREBALL)
                    Stage.getInstance().addElement(new Fireball(_player, dir, _player.getPosition().X, _player.getPosition().Y));
                resetShootTimer();
            }
            if (kS.IsKeyDown(Keys.Q) && change_weapon_timer > 1000f)
            {
                change_weapon_timer = 0f;
                currentWeapon++;
                if (currentWeapon > weapon.FIREBALL)
                    currentWeapon = weapon.CLOUD;
            }
        }

        private void resetShootTimer()
        {
            shoot_timer = 0f;
        }

        private bool isShootTimerElapsed()
        {
            float shoot_interval;

            if (currentWeapon == weapon.CLOUD)
                shoot_interval = Defaults.cloud_shoot_interval;
            else
                shoot_interval = Defaults.fireball_shoot_interval;
            return (shoot_timer > shoot_interval);
        }


    }
}
