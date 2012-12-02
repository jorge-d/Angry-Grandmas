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
        protected float _speed;
        static protected Stage _stage = Stage.getInstance();

        protected Texture2D _texture;
        private Vector2 _position;

        protected Rectangle _rectangle;

        protected Color _color = Color.White;

        protected int Width { set; get; }
        protected int Height { set; get; }

        EntityType _type;

        static protected Game _game = Game1.getGameInstance();

        public AElement(EntityType type, string texture_path, float posx, float posy, float speed)
        {
            _type = type;
            _speed = speed;
            _texture = _game.Content.Load<Texture2D>(texture_path);

            Width = _texture.Width;
            Height = _texture.Height;
            _position = new Vector2(posx, posy);
        }

        public AElement(EntityType type, float posx, float posy)
        {
            Width = Defaults.stage_square_size;
            Height = Defaults.stage_square_size;
            _position = new Vector2(posx, posy);
            _type = type;
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

        protected bool canMove(Direction dir)
        {
            switch (dir)
            {
                case Direction.LEFT:
                    return canMove((int)(getPosition().X - _speed), (int)getPosition().Y);
                case Direction.RIGHT:
                    return canMove((int)(getPosition().X + _speed), (int)getPosition().Y);
                case Direction.UP:
                    return canMove((int)getPosition().X, (int)(getPosition().Y - _speed));
                case Direction.DOWN:
                    return canMove((int)getPosition().X, (int)(getPosition().Y - _speed));
                default:
                    throw new ArgumentException();
            }
        }

        protected virtual bool move(float x, float y) { throw new NotImplementedException(); } 

        protected bool move(Direction dir)
        {
            switch (dir)
            {
                case Direction.LEFT:
                    return move(getPosition().X - _speed, getPosition().Y);
                case Direction.RIGHT:
                    return move(getPosition().X + _speed, getPosition().Y);
                case Direction.UP:
                    return move(getPosition().X, getPosition().Y - _speed);
                case Direction.DOWN:
                    return move(getPosition().X, getPosition().Y + _speed);
                default:
                    throw new ArgumentException();
            }
        }

        protected LinkedList<AElement> getOverlapingElements()
        {
            return getOverlapingElements((int)getPosition().X, (int)getPosition().Y);
        }

        protected LinkedList<AElement> getOverlapingElements(int x, int y)
        {
            LinkedList<AElement> elements = new LinkedList<AElement>();
            Stage.getInstance().getIntersections(new Rectangle(x, y, Width, Height), ref elements);
            return elements;
        }

        protected bool canMove(int x, int y)
        {
            foreach (AElement elem in getOverlapingElements(x, y))
                if (elem != this)
                {
                    EntityType type = elem.GetElementType();
                    if (type == EntityType.WALL)
                        return false;
                }
            return true;
        }
    }
}
