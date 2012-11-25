using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    public class Sheep : APlayer
    {
         public Sheep(float posx, float posy) :
            base(Defaults.sheep_texture_path, posx, posy, Defaults.sheep_speed_x, Defaults.sheep_speed_y, Defaults.sheep_health)
        {
        }

        public override bool update()
        {
            KeyboardState kS = Keyboard.GetState();

            if (kS.IsKeyDown(Keys.Q))
                return false;
            return true;
        }
    }
}
