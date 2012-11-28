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
        HumanSpriteAnimation sprite;
        protected float shoot_timer = 0f;
        protected const float shoot_interval = 600f;

        public HumanPlayer(float posx, float posy) :
            base(Defaults.human_texture_path, posx, posy, Defaults.player_speed_x, Defaults.player_speed_y, Defaults.player_health)
        {
            Width = Defaults.player_width;
            Height = Defaults.player_height;
            sprite = new HumanSpriteAnimation(Width, Height);
        }

        public override bool update(GameTime gameTime)
        {
            sprite.update(gameTime);
            updateUsingKeyboard(gameTime);
            return true;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, getPosition(), sprite.SourceRect, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        private bool canMove(int x, int y)
        {
            LinkedList<AElement> elements = new LinkedList<AElement>();
            Stage.getInstance().getIntersections(new Rectangle(x, y, Width, Height), ref elements);
            foreach (AElement elem in elements)
                if (elem != this)
                {
                    EntityType type = elem.GetElementType();
                    if (type == EntityType.WALL)
                        return false;
                }
            return true;
        }

        private void shoot(GameTime gameTime, KeyboardState kS)
        {
            shoot_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Direction looking_at = sprite.getLookingDirection();

            if (kS.IsKeyDown(Keys.Space) && shoot_timer > shoot_interval)
            {
                shoot_timer = 0f;
                Stage.getInstance().addElement(new Cloud(this, looking_at, getPosition().X, getPosition().Y));
            }
        }

        private Direction moveUsingKeyboard(KeyboardState kS, ref float newX, ref float newY)
        {
            if (kS.IsKeyDown(Keys.P))
                _game.Exit();

            if (kS.IsKeyDown(Keys.A))
            {
                newX -= _speed_x;
                return Direction.LEFT;
            }
            else if (kS.IsKeyDown(Keys.D))
            {
                newX += _speed_x;
                return Direction.RIGHT;
            }
            else if (kS.IsKeyDown(Keys.W))
            {
                newY -= _speed_y;
                return Direction.UP;
            }
            else if (kS.IsKeyDown(Keys.S))
            {
                newY += _speed_y;
                return Direction.DOWN;
            }
            return Direction.NONE;
        }

        private void updateUsingKeyboard(GameTime gameTime)
        {
            KeyboardState kS = Keyboard.GetState();
            Vector2 pos = getPosition();

            float newX = pos.X;
            float newY = pos.Y;

            Direction dir = moveUsingKeyboard(kS, ref newX, ref newY);
            sprite.animate(dir);
            shoot(gameTime, kS);

            if (canMove((int)newX, (int)newY))
                this.setPosition(newX, newY);
        }

        private class HumanSpriteAnimation : SpriteSheet
        {
            Direction _previous_state;
            Direction _current_state = Direction.DOWN;

            public HumanSpriteAnimation(int spriteWidth, int spriteHeight)
                : base(0, 0, spriteWidth, spriteHeight)
            {
            }

            public Direction getLookingDirection()
            {
                return _current_state;
            }

            public void animate(Direction dir)
            {
                _previous_state = _current_state;

                if (dir != Direction.NONE)
                    _current_state = dir;

                switch (dir)
                {
                    case Direction.LEFT:
                        animateLeft();
                        break;
                    case Direction.RIGHT:
                        animateRight();
                        break;
                    case Direction.UP:
                        animateUp();
                        break;
                    case Direction.DOWN:
                        animateDown();
                        break;
                    default:
                        finishCurrentMovment();
                        break;
                }
            }

            private void finishCurrentMovment()
            {
                if (getX() >= 0 && getX() <= 2)
                    setX(1);
                if (getX() >= 3 && getX() <= 5)
                    setX(4);
                if (getX() >= 6 && getX() <= 8)
                    setX(7);
                if (getX() >= 9 && getX() <= 11)
                    setX(10);
            }

            private void animateRight()
            {
                if (_current_state != _previous_state)
                    setX(4);

                if (isTimerElapsed())
                {
                    incrementX();
                    resetTimer();
                    if (getX() > 5)
                        setX(3);
                }
            }

            private void animateLeft()
            {
                if (_current_state != _previous_state)
                    setX(7);

                if (isTimerElapsed())
                {
                    incrementX();
                    resetTimer();
                    if (getX() > 8)
                        setX(6);
                }
            }

            private void animateDown()
            {
                if (_current_state != _previous_state)
                    setX(1);

                if (isTimerElapsed())
                {
                    incrementX();
                    resetTimer();
                    if (getX() > 2)
                        setX(0);
                }
            }

            private void animateUp()
            {
                if (_current_state != _previous_state)
                    setX(10);

                if (isTimerElapsed())
                {
                    incrementX();
                    resetTimer();
                    if (getX() > 11)
                        setX(9);
                }
            }

        }
    }
}
