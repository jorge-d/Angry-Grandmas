using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    class Stage
    {
        static protected Game _game = Game1.getGameInstance();
        private float _sheep_generation_interval = 3000f;
        private float _sheep_generation_timer = 0f;

        int[,] level = new int[,]
           {{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}};

        LinkedList<AElement> _elements;

        static Stage _instance = null;

        static public Stage getInstance()
        {
            if (_instance == null)
                _instance = new Stage();
            return _instance;
        }

        private Stage()
        {
            _elements = new LinkedList<AElement>();
        }

        public void addElement(AElement elem)
        {
            _elements.AddLast(elem);
        }

        public bool init(int player_nb)
        {
            for (int y = 0; y < level.GetLength(0); y++)
                for (int x = 0; x < level.GetLength(1); x++)
                    switch (level[y, x])
                    {
                        case 1:
                            this.addElement(new Wall(32 * x, 32 * y));
                            break;
                        case 2:
                            this.addElement(new HumanPlayer(32 * x, 32 * y));
                            break;
                        case 3:
                            this.addElement(new Sheep(32 * x, 32 * y));
                            break;
                    }
            return true;
        }

        public int update(GameTime gametime)
        {
            LinkedList<AElement> elements = new LinkedList<AElement>(_elements);

            foreach (AElement element in elements)
            {
                if (!element.update(gametime))
                    _elements.Remove(element);
            }
            generateSheeps(gametime);
            return 0;
        }

        private void resetSheepTimer()
        {
            _sheep_generation_timer = 0f;
        }

        private void findRandomSpot(out int posx, out int posy)
        {
            Random r = new Random();
            int nb = (r.Next() % (level.GetLength(0) * level.GetLength(1))) + 1;
            int tmp = 0;

            while (true)
                for (int y = 0; y < level.GetLength(0); y++)
                    for (int x = 0; x < level.GetLength(1); x++)
                        if (level[y, x] != 1 && tmp++ >= nb)
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
                addElement(new Sheep(32 * x, 32 * y));
                resetSheepTimer();
            }
        }

        public void getIntersections(Rectangle rec, ref LinkedList<AElement> ret)
        {
            foreach (AElement element in _elements)
            {
                if (element.getObjectRectangle().Intersects(rec))
                    ret.AddLast(element);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (AElement element in _elements)
                element.draw(spriteBatch);
        }

    }
}
