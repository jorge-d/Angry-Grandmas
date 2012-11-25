using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace WindowsGame1
{
    class HumanPlayer : APlayer
    {
        public HumanPlayer(float posx, float posy) :
            base(Defaults.human_texture_path, posx, posy, Defaults.player_speed_x, Defaults.player_speed_y, Defaults.player_health)
        {
        }

        public override bool update()
        {
            this.updateUsingKeyboard();

            //check_intersections();
            return true;
        }

        //public void check_intersections()
        //{
        //    Rectangle rectangle = rectangles[instance_count];

        //    rectangle.X = (int)position.X;
        //    rectangle.Y = (int)position.Y;
        //    rectangle.Width = texture.Width;
        //    rectangle.Height = texture.Height;

        //    // Doesn't seem to update the reference in the list, need to check why...
        //    rectangles[instance_count] = rectangle;

        //    if (has_intersected(rectangle))
        //        color = Color.Red;
        //    else
        //        color = Color.White;
        //}

        public void updateUsingKeyboard()
        {
            Vector2 pos = getPosition();

            float newX = pos.X;
            float newY = pos.Y;

            KeyboardState kS = Keyboard.GetState();

            if (kS.IsKeyDown(Keys.A))
                newX -= _speed_x;
            if (kS.IsKeyDown(Keys.D))
                newX += _speed_x;

            if (kS.IsKeyDown(Keys.W))
                newY -= _speed_y;
            if (kS.IsKeyDown(Keys.S))
                newY += _speed_y;

            if (kS.IsKeyDown(Keys.Space))
                ((Game1)_game).addElement(new Sheep(newX - 50f, newY - 50f));

            this.setPosition(newX, newY);
        }

        private int checkBoudaries(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
    }
}
