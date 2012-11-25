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
        protected float _posx;
        protected float _posy;
        protected float _speedx;
        protected float _speedy;

        protected Texture2D _texture;
        protected Vector2 _position;

        protected Rectangle _rectangle;

        protected Color _color;

        static protected Game _game = Game1.getGameInstance();

        public AElement(string texture_path, float posx, float posy, float speed_x, float speed_y)
        {
            _posx = posx;
            _posy = posy;
            _speedx = speed_x;
            _speedy = speed_y;
            _texture = _game.Content.Load<Texture2D>(texture_path);

            _color = Color.White;
            _position = new Vector2(posx, posy);
        }

        // Should return false if the object is dead.
        // Returns true otherwise.
        public abstract bool update();

        public void draw(SpriteBatch batch)
        {
            batch.Draw(_texture, _position, null, _color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public Rectangle getObjectRectangle()
        {
            _rectangle.X = (int)_position.X;
            _rectangle.Y = (int)_position.Y;
            _rectangle.Width = _texture.Width;
            _rectangle.Height = _texture.Height;
            return _rectangle;
        }

        public Vector2 getPosition()
        {
            return _position;
        }
    }
}
