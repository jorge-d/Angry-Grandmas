using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    public class Healthbar : DrawableGameComponent
    {
        private Texture2D textureGreen;
        private Texture2D textureRed;
        Rectangle redRectangle = new Rectangle(0, 0, 30, 5);
        Rectangle greenRectangle = new Rectangle(0, 0, 30, 5);

        public Healthbar(Game1 game)
            : base(game)
        {
            // Choose a high number, so we will draw on top of other components.
            DrawOrder = 1000;
            textureGreen = game.textureGreen;
            textureRed = game.textureRed;
        }

        public void update(Vector2 position, int healt)
        {
            redRectangle.X = (int)position.X;
            redRectangle.Y = (int)position.Y - 10;
            greenRectangle.X = (int)position.X;
            greenRectangle.Y = (int)position.Y - 10;

            greenRectangle.Width = healt * 3;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureRed, redRectangle, Color.White);
            spriteBatch.Draw(textureGreen, greenRectangle, Color.White);
        }
    }
}
