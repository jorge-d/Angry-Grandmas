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
        public HumanPlayer(string texture_path, float posx, float posy) :
            base(texture_path, posx, posy, Defaults.player_speed_x, Defaults.player_speed_y, Defaults.player_health)
        {
        }

        public override bool update()
        {
            Console.Out.WriteLine("update player");
            this.updateUsingKeyboard();

            //check_intersections();
            return true;
        }

        public void updateUsingKeyboard()
        {
            Vector2 pos = getPosition();
            Console.Out.WriteLine("BEFORE : " + pos);
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

            //pos.X = newX;
            //pos.Y = newY;
            this.setPosition(newX, newY);
            //pos.X = this.checkBoudaries((int)newX, 0, Window.ClientBounds.Width - texture.Width);
            //pos.Y = this.checkBoudaries((int)newY, 0, Window.ClientBounds.Height - texture.Height);
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
