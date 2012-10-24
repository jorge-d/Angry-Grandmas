using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;


namespace WindowsGame1
{
    class Element
    {
        Texture2D texture;
        Vector2 position;

        float speed_x;
        float speed_y;

        Boolean reverse_x;
        Boolean reverse_y;
        GameWindow Window;

        public static List<Rectangle> rectangles = new List<Rectangle>();

        Color color;

        //Point point;
        //BoundingBox bounding_box;

        bool automatic_update;
        int instance_count;

        public Element(GameWindow window, ContentManager content, string path, float x, float y, float gap_x, float gap_y, bool auto = true)
        {
            Window = window;

            instance_count = rectangles.Count;

            rectangles.Add(new Rectangle());

            texture = content.Load<Texture2D>(path);
            position.X = x;
            position.Y = y;
            this.speed_x = gap_x;
            this.speed_y = gap_y;
            reverse_x = false;
            reverse_y = false;
            automatic_update = auto;
            color = Color.White;
        }

        private void updateAutomatically()
        {
            if ((int)(position.X + texture.Width) >= Window.ClientBounds.Width || position.X < 0)
                reverse_x = !reverse_x;
            if ((int)(position.Y + texture.Height) >= Window.ClientBounds.Height || position.Y < 0)
                reverse_y = !reverse_y;

            if (reverse_x)
                position.X -= speed_x;
            else
                position.X += speed_x;

            if (reverse_y)
                position.Y -= speed_y;
            else
                position.Y += speed_y;
        }

        public bool has_intersected(Rectangle rectangle)
        {
            int loop_count = 0;
            foreach (Rectangle rec in rectangles)
            {
                if (loop_count != instance_count && rectangle.Intersects(rec))
                    return true;
                loop_count++;
            }
            return false;
        }

        public void check_intersections()
        {
            Rectangle rectangle = rectangles[instance_count];

            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
            rectangle.Width = texture.Width;
            rectangle.Height = texture.Height;

            // Doesn't seem to update the reference in the list, need to check why...
            rectangles[instance_count] = rectangle;

            if (has_intersected(rectangle))
                color = Color.Red;
            else
                color = Color.White;

        }

        public void update()
        {
            if (automatic_update)
               this.updateAutomatically();
            else
               this.updateUsingKeyboard();

            check_intersections();
        }

        public void updateUsingKeyboard()
        {
            float newX = position.X;
            float newY = position.Y;

            KeyboardState kS = Keyboard.GetState();

            if (kS.IsKeyDown(Keys.A))
	            newX -= speed_x;
            if (kS.IsKeyDown(Keys.D))
                newX += speed_x;

            if (kS.IsKeyDown(Keys.W))
                newY -= speed_y;
            if (kS.IsKeyDown(Keys.S))
                newY += speed_y;


            position.X = this.checkBoudaries((int)newX, 0, Window.ClientBounds.Width - texture.Width);
            position.Y = this.checkBoudaries((int)newY, 0, Window.ClientBounds.Height - texture.Height);
        }

        private int checkBoudaries(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, null, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
