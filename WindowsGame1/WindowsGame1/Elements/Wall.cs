using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    public class Wall : AElement
    {
        public Wall(float posx, float posy) :
            base(EntityType.WALL, posx, posy)
        {
        }

        public override void draw(SpriteBatch batch)
        {
            // DO NOTHING HERE
        }

        public override bool update(GameTime gameTime)
        {
            return true;
        }
    }
}
