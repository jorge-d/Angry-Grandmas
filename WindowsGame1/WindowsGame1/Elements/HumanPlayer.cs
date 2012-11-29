﻿using System;
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

        public HumanPlayer(float posx, float posy) :
            base(Defaults.human_texture_path, posx, posy, Defaults.player_speed, Defaults.player_health)
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

            if (kS.IsKeyDown(Keys.Space) && shoot_timer > Defaults.player_shoot_interval)
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
                newX -= _speed;
                return Direction.LEFT;
            }
            else if (kS.IsKeyDown(Keys.D))
            {
                newX += _speed;
                return Direction.RIGHT;
            }
            else if (kS.IsKeyDown(Keys.W))
            {
                newY -= _speed;
                return Direction.UP;
            }
            else if (kS.IsKeyDown(Keys.S))
            {
                newY += _speed;
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

                // Avoid having a direction NONE because it could be harmfull
                // when we try to know where he's looking
                if (dir != Direction.NONE)
                    _current_state = dir;

                // Avoid non-smooth transitions
                if (_current_state != _previous_state)
                    resetMovement();

                switch (dir)
                {
                    case Direction.LEFT:
                        move(Defaults.MOUVEMENT_DIRECTION_LEFT);
                        break;
                    case Direction.RIGHT:
                        move(Defaults.MOUVEMENT_DIRECTION_RIGHT);
                        break;
                    case Direction.UP:
                        move(Defaults.MOUVEMENT_DIRECTION_UP);
                        break;
                    case Direction.DOWN:
                        move(Defaults.MOUVEMENT_DIRECTION_DOWN);
                        break;
                    default:
                        resetMovement();
                        break;
                }
            }

            private void resetMovement()
            {
                setX(Defaults.MOUVEMENT_PHASE_MIDDLE);
            }

            private void move(int y)
            {
                setY(y);
                if (isTimerElapsed())
                {
                    resetTimer();
                    incrementX();
                    if (getX() > Defaults.MOUVEMENT_PHASE_END)
                        setX(Defaults.MOUVEMENT_PHASE_BEGIN);
                }
            }
        }
    }
}
