using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class HumanPlayer : APlayer
    {
        public HumanPlayer(string texture_path, float posx, float posy) :
            base(texture_path, posx, posy, Defaults.player_speed_x, Defaults.player_speed_y, Defaults.player_health)
        {
        }

        public override bool update()
        {
            Console.Out.WriteLine("update player");
            return true;
        }
    }
}
