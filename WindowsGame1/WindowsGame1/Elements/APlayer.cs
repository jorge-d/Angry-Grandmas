﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public abstract class APlayer : AElement
    {
        protected int _health;

        public APlayer(string texture_path, float posx, float posy, float speed, int health) :
            base(EntityType.PLAYER, texture_path, posx, posy, speed)
        {
            _health = health;
        }
    }
}
