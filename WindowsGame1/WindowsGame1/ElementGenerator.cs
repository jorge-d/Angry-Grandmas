﻿using System;
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
        Random r = new Random();
        private Stage _stage = Stage.getInstance();

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
            int[,] level = _stage.level;
            int nb = (r.Next() % (Defaults.stage_square_nb_x * Defaults.stage_square_nb_y)) + 1;
            int tmp = 0;

            while (true)
                for (int y = 0; y < Defaults.stage_square_nb_y; y++)
                    for (int x = 0; x < Defaults.stage_square_nb_x; x++)
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
            int[,] level = _stage.level;
            int posx;
            int posy;
            Direction dir = Direction.NONE;

            for (int y = 0; y < Defaults.stage_square_nb_y; y++)
                for (int x = 0; x < Defaults.stage_square_nb_x; x++)
                {
                    if ((x == 0 || ((x + 1) == Defaults.stage_square_nb_x)) &&
                        (y == 0 || ((y + 1) == Defaults.stage_square_nb_y)))
                        dir = Direction.NONE;
                    else if (x == 0)
                        dir = Direction.LEFT;
                    else if ((x + 1) == Defaults.stage_square_nb_x)
                        dir = Direction.RIGHT;
                    else if (y == 0)
                        dir = Direction.UP;
                    else if ((y + 1) == Defaults.stage_square_nb_y)
                        dir = Direction.DOWN;
                    else
                        continue;
                    _stage.addElement(new Wall(Defaults.stage_square_size * x, Defaults.stage_square_size * y, dir));
                    level[y, x] = (int)MapElements.WALL;
                }

            findRandomSpot(out posx, out posy);
            level[posy, posx] = (int)MapElements.SPAWN;
            _stage.addElement(new HumanPlayer(posx * Defaults.stage_square_size, posy * Defaults.stage_square_size));

            for (int i = 0; i < Defaults.tree_numbers; i++)
            {
                findRandomSpot(out posx, out posy);
                level[posy, posx] = (int)MapElements.TREE;
            }

            return level;
        }
    }
}
