using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    class ElementGenerator
    {
        private float _sheep_generation_interval = 3000f;
        private float _sheep_generation_timer = 0f;

        private void resetSheepTimer()
        {
            _sheep_generation_timer = 0f;
        }

        public void update(GameTime gameTime)
        {
            generateSheeps(gameTime);
        }

        private void findRandomSpot(out int posx, out int posy)
        {
            int[,] level = Stage.getInstance().level;
            Random r = new Random();

            int nb = (r.Next() % (level.GetLength(0) * level.GetLength(1))) + 1;
            int tmp = 0;

            while (true)
                for (int y = 0; y < level.GetLength(0); y++)
                    for (int x = 0; x < level.GetLength(1); x++)
                        if (level[y, x] != (int)MapElements.WALL && tmp++ >= nb)
                        {
                            posx = x;
                            posy = y;
                            return;
                        }
        }

        private void generateSheeps(GameTime gameTime)
        {
            _sheep_generation_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_sheep_generation_timer > _sheep_generation_interval)
            {
                int x;
                int y;

                findRandomSpot(out x, out y);
                Stage.getInstance().addElement(new Sheep(Defaults.stage_square_size * x, Defaults.stage_square_size * y));
                resetSheepTimer();
            }
        }

        public int[,] generateRandomWorld(int player_nb)
        {
            int[,] level = Stage.getInstance().level;
            int posx;
            int posy;
            for (int y = 0; y < Defaults.stage_square_nb_y; y++)
                for (int x = 0; x < Defaults.stage_square_nb_x; x++)
                {
                    if (y == 0 || (y + 1) == Defaults.stage_square_nb_y ||
                        x == 0 || (x + 1) == Defaults.stage_square_nb_x)
                        level[y, x] = (int)MapElements.WALL;
                }

            findRandomSpot(out posx, out posy);
            level[posy, posx] = (int)MapElements.SPAWN;

            return level;
        }
    }
}
