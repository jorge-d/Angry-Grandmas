﻿using System;
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
        private int player_nb;

        public Gun(HumanPlayer p, int nb)
        {
            _player = p;
            player_nb = nb;
        }

        public string getWeaponName()
        {
            if (currentWeapon == weapon.CLOUD)
                return "Cloud";
            return "Fireball";
        }

        public void update(GameTime gameTime, Direction direction)
        {
            shoot_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            change_weapon_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (!_player.isBleeding())
                shoot(direction);
        }

        public void shoot(Direction dir)
        {
            KeyboardState kS = Keyboard.GetState();

            if (((player_nb == 1 && kS.IsKeyDown(Keys.Space)) ||
                (player_nb == 2 && kS.IsKeyDown(Keys.RightAlt))) && isShootTimerElapsed())
            {
                if (currentWeapon == weapon.CLOUD)
                    Stage.getInstance().addElement(new Cloud(_player, dir, _player.getPosition().X, _player.getPosition().Y));
                else if (currentWeapon == weapon.FIREBALL)
                    Stage.getInstance().addElement(new Fireball(_player, dir, _player.getPosition().X, _player.getPosition().Y));
                resetShootTimer();
            }
            if (((player_nb == 1 && kS.IsKeyDown(Keys.Q)) ||
                (player_nb == 2 && kS.IsKeyDown(Keys.RightShift))) && change_weapon_timer > 800f)
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
