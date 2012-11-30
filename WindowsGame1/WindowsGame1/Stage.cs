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
        static private Game _game = Game1.getGameInstance();
        static private Stage _instance = null;

        private LinkedList<AElement> _elements;
        private ElementGenerator generator = new ElementGenerator();

        public int[,] level = new int[Defaults.stage_square_nb_y, Defaults.stage_square_nb_x];


        static public Stage getInstance()
        {
            if (_instance == null)
                _instance = new Stage();
            return _instance;
        }

        private Stage() {}

        public void addElement(AElement elem)
        {
            _elements.AddLast(elem);
        }

        public bool init(int player_nb)
        {
            level = generator.generateRandomWorld(player_nb);
            _elements = new LinkedList<AElement>();

            for (int y = 0; y < level.GetLength(0); y++)
                for (int x = 0; x < level.GetLength(1); x++)
                    switch (level[y, x])
                    {
                        case (int)MapElements.WALL:
                            this.addElement(new Wall(Defaults.stage_square_size * x, Defaults.stage_square_size * y));
                            break;
                        case (int)MapElements.SPAWN:
                            this.addElement(new HumanPlayer(Defaults.stage_square_size * x, Defaults.stage_square_size * y));
                            break;
                    }
            return true;
        }

        public int update(GameTime gametime)
        {
            LinkedList<AElement> elements = new LinkedList<AElement>(_elements);

            foreach (AElement element in elements)
                if (!element.update(gametime))
                    _elements.Remove(element);
            generator.update(gametime);
            return 0;
        }


        public void getIntersections(Rectangle rec, ref LinkedList<AElement> ret)
        {
            foreach (AElement element in _elements)
                if (element.getObjectRectangle().Intersects(rec))
                    ret.AddLast(element);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (AElement element in _elements)
                element.draw(spriteBatch);
        }

    }
}
