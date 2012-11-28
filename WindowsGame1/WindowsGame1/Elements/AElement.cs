using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    public abstract class AElement
    {
        protected float _speed_x;
        protected float _speed_y;

        protected Texture2D _texture;
        private Vector2 _position;

        protected Rectangle _rectangle;

        protected Color _color;

        protected int Width { set; get; }
        protected int Height { set; get; }

        EntityType _type;

        static protected Game _game = Game1.getGameInstance();

        public AElement(EntityType type, string texture_path, float posx, float posy, float speed_x, float speed_y)
        {
            _type = type;
            _speed_x = speed_x;
            _speed_y = speed_y;
            _texture = _game.Content.Load<Texture2D>(texture_path);

            Width = _texture.Width;
            Height = _texture.Height;
            _color = Color.White;
            _position = new Vector2(posx, posy);
        }

        public EntityType GetElementType()
        {
            return _type;
        }

        // Should return false if the object is dead.
        // Returns true otherwise.
        public abstract bool update(GameTime gameTime);

        public virtual void draw(SpriteBatch batch)
        {
            batch.Draw(_texture, _position, null, _color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public Rectangle getObjectRectangle()
        {
            _rectangle.X = (int)_position.X;
            _rectangle.Y = (int)_position.Y;
            _rectangle.Width = Width;
            _rectangle.Height = Height;
            return _rectangle;
        }

        public Vector2 getPosition()
        {
            return _position;
        }

        public void setPosition(float x, float y)
        {
            _position.X = checkBoudaries((int)x, 0, Defaults.window_size_x - Width);
            _position.Y = checkBoudaries((int)y, 0, Defaults.window_size_y - Height);
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
