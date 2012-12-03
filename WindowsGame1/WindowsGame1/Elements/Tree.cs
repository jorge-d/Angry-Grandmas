using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Tree : Wall
    {
        public Tree(float posx, float posy) :
            base(posx, posy, Direction.NONE)
        {
            Width = 64;
            Height = 64;
        }

        public override void draw(SpriteBatch batch)
        {
            _stage.drawElement(batch, getPosition(), 18, 10, Defaults.stage_square_size * 2, Defaults.stage_square_size * 2);
        }
    }
}
