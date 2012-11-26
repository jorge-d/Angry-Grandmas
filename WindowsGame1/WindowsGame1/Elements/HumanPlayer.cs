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

            return true;
        }

        private bool canMove(int x, int y)
        {
            LinkedList<AElement> elements = new LinkedList<AElement>();

            Stage.getInstance().getIntersections(new Rectangle(x, y, width, height), ref elements);
            foreach (AElement elem in elements)
            {
                if (elem != this)
                    return false;
            }
            return true;
        }

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
                Stage.getInstance().addElement(new Sheep(newX - 50f, newY - 50f));

            if (kS.IsKeyDown(Keys.P))
                _game.Exit();

            if (canMove((int)newX, (int)newY))
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
