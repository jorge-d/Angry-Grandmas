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
        public Wall(float posx, float posy, Direction dir) :
            base(EntityType.WALL, posx, posy)
        {
            _direction = dir;
        }

        public override void draw(SpriteBatch batch)
        {
            switch (_direction)
            {
                case Direction.NONE:
                    _stage.drawElement(batch, getPosition(), 22, 3);
                    break;
                case Direction.UP:
                    _stage.drawElement(batch, getPosition(), 22, 5);
                    break;
                case Direction.DOWN:
                    _stage.drawElement(batch, getPosition(), 22, 2);
                    break;
                case Direction.LEFT:
                    _stage.drawElement(batch, getPosition(), 23, 3);
                    break;
                case Direction.RIGHT:
                    _stage.drawElement(batch, getPosition(), 21, 3);
                    break;
            }
        }

        public override bool update(GameTime gameTime)
        {
            return true;
        }
    }
}
