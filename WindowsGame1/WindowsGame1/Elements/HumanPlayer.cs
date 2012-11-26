using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class HumanPlayer : APlayer
    {
        SpriteSheet sprite;

        public HumanPlayer(float posx, float posy) :
            base(Defaults.human_texture_path, posx, posy, Defaults.player_speed_x, Defaults.player_speed_y, Defaults.player_health)
        {
            Width = Defaults.player_width;
            Height = Defaults.player_height;
            sprite = new SpriteSheet(0, Width, Height);
        }

        public override bool update(GameTime gameTime)
        {
            sprite.update(gameTime);
            this.updateUsingKeyboard(gameTime);

            return true;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            sprite.calculate();
            spriteBatch.Draw(_texture, getPosition(), sprite.SourceRect, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        private bool canMove(int x, int y)
        {
            LinkedList<AElement> elements = new LinkedList<AElement>();
            Stage.getInstance().getIntersections(new Rectangle(x, y, Width, Height), ref elements);
            foreach (AElement elem in elements)
            {
                if (elem != this)
                    return false;
            }
            return true;
        }

        public void updateUsingKeyboard(GameTime gameTime)
        {
            Vector2 pos = getPosition();

            float newX = pos.X;
            float newY = pos.Y;

            KeyboardState kS = Keyboard.GetState();

            if (kS.IsKeyDown(Keys.A))
            {
                newX -= _speed_x;
                sprite.animateLeft();
            }
            else if (kS.IsKeyDown(Keys.D))
            {
                newX += _speed_x;
                sprite.animateRight();
            }

            if (kS.IsKeyDown(Keys.W))
            {
                newY -= _speed_y;
                sprite.animateUp();
            }
            else if (kS.IsKeyDown(Keys.S))
            {
                newY += _speed_y;
                sprite.animateDown();
            }

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
