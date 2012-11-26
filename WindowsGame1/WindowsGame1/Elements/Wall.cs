using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public class Wall : AElement
    {
        public Wall(float posx, float posy) :
            base(@"Images/brick", posx, posy, 0, 0)
        {
        }

        public override bool update()
        {
            return true;
        }
    }
}
