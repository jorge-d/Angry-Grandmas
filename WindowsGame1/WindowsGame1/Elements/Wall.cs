using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    public class Wall : AElement
    {
        public Wall(float posx, float posy) :
            base(@"Images/brick", posx, posy, 0, 0)
        {
        }

        public override bool update(GameTime gameTime)
        {
            return true;
        }
    }
}
