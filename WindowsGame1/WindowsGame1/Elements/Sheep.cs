using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    public class Sheep : AEntity
    {
         public Sheep(float posx, float posy) :
            base(Defaults.sheep_texture_path, posx, posy, Defaults.sheep_speed, Defaults.sheep_health)
        {
        }

        public override bool update(GameTime gameTime)
        {
            KeyboardState kS = Keyboard.GetState();

            if (kS.IsKeyDown(Keys.Q))
                return false;
            return true;
        }
    }
}
