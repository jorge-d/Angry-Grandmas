using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    class ElementGenerator
    {
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
            int nb = r.Next(Defaults.stage_square_nb_x * Defaults.stage_square_nb_y) + 1;
            int tmp = 0;

            while (true)
                for (int y = 0; y < Defaults.stage_square_nb_y; y++)
                    for (int x = 0; x < Defaults.stage_square_nb_x; x++)
                        if (_stage.level[y, x] == (int)MapElements.GRASS && tmp++ >= nb)
                        {
                            posx = x;
                            posy = y;
                            return;
                        }
        }

        private void findRandomSpotBig(out int posx, out int posy)
        {
            int nb = r.Next(Defaults.stage_square_nb_x * Defaults.stage_square_nb_y) + 5;
            int tmp = 0;

            while (true)
                for (int y = 2; y < (Defaults.stage_square_nb_y - 3); y++)
                    for (int x = 2; x < (Defaults.stage_square_nb_x - 3); x++)
                        if (tmp++ >= nb)
                            if (_stage.level[y, x] == (int)MapElements.GRASS &&
                                _stage.level[y + 1, x + 1] == (int)MapElements.GRASS &&
                                _stage.level[y, x + 1] == (int)MapElements.GRASS &&
                                _stage.level[y + 1, x] == (int)MapElements.GRASS)
                            {
                                posx = x;
                                posy = y;
                                return;
                            }
        }

        private void generateSheeps(GameTime gameTime)
        {
            _sheep_generation_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_sheep_generation_timer > Defaults.sheep_generation_interval)
            {
                int x;
                int y;

                findRandomSpot(out x, out y);
                _stage.addElement(new Sheep(Defaults.stage_square_size * x, Defaults.stage_square_size * y));
                resetSheepTimer();
            }
        }

        public void generateRandomWorld(int player_nb)
        {
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
                    _stage.level[y, x] = (int)MapElements.WALL;
                }

            findRandomSpot(out posx, out posy);
            _stage.level[posy, posx] = (int)MapElements.SPAWN;
            _stage.addElement(new HumanPlayer(posx * Defaults.stage_square_size, posy * Defaults.stage_square_size));

            generateTrees();

        }

        private void generateTrees()
        {
            int posx;
            int posy;

            for (int i = 0; i < Defaults.tree_numbers; i++)
            {
                findRandomSpot(out posx, out posy);
                _stage.level[posy, posx] = (int)MapElements.TREE;
            }

            for (int i = 0; i < (r.Next(10) + 10); i++)
            {
                findRandomSpotBig(out posx, out posy);
                _stage.level[posy, posx] = (int)MapElements.WALL;
                _stage.level[posy + 1, posx] = (int)MapElements.WALL;
                _stage.level[posy, posx + 1] = (int)MapElements.WALL;
                _stage.level[posy + 1, posx + 1] = (int)MapElements.WALL;
                _stage.addElement(new Tree(posx * Defaults.stage_square_size, posy * Defaults.stage_square_size));
            }
        }
    }
}
