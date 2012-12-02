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
            _stage = Stage.getInstance();
        }

        public override void draw(SpriteBatch batch)
        {
            _stage.drawElement(batch, getPosition(), 21, 3); 
        }

        public override bool update(GameTime gameTime)
        {
            return true;
        }
    }
}
