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
    class HumanPlayer : AEntity
    {
        SpriteSheet sprite;
        protected float shoot_timer = 0f;

        public HumanPlayer(float posx, float posy) :
            base(Defaults.human_texture_path, posx, posy, Defaults.player_speed, Defaults.player_health)
        {
            Width = Defaults.player_width;
            Height = Defaults.player_height;
            sprite = new SpriteSheet(Defaults.MOUVEMENT_PHASE_MIDDLE, Defaults.MOUVEMENT_DIRECTION_DOWN, Width, Height);
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
    }
}
