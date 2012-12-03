using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class Fireball : ABullet 
    {
        public Fireball(HumanPlayer shooter, Direction dir, float posx, float posy) :
            base(shooter, dir, Defaults.cloud_damages, Defaults.cloud_texture_path, posx, posy, Defaults.cloud_speed)
        {
        }
        
        public override bool update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
