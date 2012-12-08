using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    public class Stage
    {
        static private Game _game = Game1.getGameInstance();
        static private Stage _instance = null;
        static private Texture2D _world_texture = null;

        private LinkedList<AElement> _elements;
        private ElementGenerator generator;
        private SpriteSheet sprite = new SpriteSheet(0, 0, Defaults.stage_square_size, Defaults.stage_square_size);
        public int[,] level;

        static public Stage getInstance()
        {
            if (_instance == null)
                _instance = new Stage();
            return _instance;
        }

        private Stage() { }

        public void addElement(AElement elem)
        {
            _elements.AddLast(elem);
        }

        private void loadTexture()
        {
            if (_world_texture == null)
                _world_texture = _game.Content.Load<Texture2D>(Defaults.world_texture_path);
        }

        public bool init()
        {
            Sheep.sheep_instances = 0;
            level = new int[Defaults.stage_square_nb_y, Defaults.stage_square_nb_x]; 
            generator = new ElementGenerator();
            loadTexture();
            _elements = new LinkedList<AElement>();
            generator.generateRandomWorld(2);
            return true;
        }

        public void update(GameTime gametime)
        {
            LinkedList<AElement> elements = new LinkedList<AElement>(_elements);

            foreach (AElement element in elements)
                if (!element.update(gametime))
                    _elements.Remove(element);
            generator.update(gametime);
        }

        public string endOfGame()
        {
            HumanPlayer winner = null;
            foreach (AElement element in _elements)
                if (element is HumanPlayer)
                {
                    HumanPlayer h = (HumanPlayer)element;
                    if (winner == null)
                        winner = h;
                    else if (winner.getSore() < h.getSore())
                        winner = h;
                    else if (winner.getSore() == h.getSore())
                        winner = null;
                }
            if (winner != null)
                return "PLAYER " + winner.getPlayerNb() + " WON !";
            return "THE RESULT IS TIE!";
        }
        

        public void getIntersections(Rectangle rec, ref LinkedList<AElement> ret)
        {
            foreach (AElement element in _elements)
                if (element.getObjectRectangle().Intersects(rec))
                    ret.AddLast(element);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            renderMap(spriteBatch);
            foreach (AElement element in _elements)
                element.draw(spriteBatch);
        }

        private void renderMap(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < level.GetLength(0); y++)
                for (int x = 0; x < level.GetLength(1); x++)
                    drawElement(spriteBatch, (MapElements)level[y, x], x * Defaults.stage_square_size, y * Defaults.stage_square_size);
        }

        public void drawElement(SpriteBatch spriteBatch, Vector2 pos, int x, int y)
        {
            sprite.setX(x);
            sprite.setY(y);
            spriteBatch.Draw(_world_texture, pos, sprite.SourceRect, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        public void drawElement(SpriteBatch spriteBatch, Vector2 pos, int x, int y, int width, int height)
        {
            sprite.setSpriteDimensions(width, height);
            sprite.setX(x);
            sprite.setY(y);
            spriteBatch.Draw(_world_texture, pos, sprite.SourceRect, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            sprite.setSpriteDimensions(Defaults.stage_square_size, Defaults.stage_square_size);
        }

        public void drawElement(SpriteBatch spriteBatch, MapElements type, int x, int y)
        {
            Vector2 pos = new Vector2(x, y);
            switch (type)
            {
                case MapElements.WALL:
                case MapElements.SPAWN:
                case MapElements.GRASS:
                    sprite.setX(1);
                    sprite.setY(8);
                    break;
                case MapElements.TREE:
                    sprite.setX(0);
                    sprite.setY(12);
                    break;
                default:
                    return;
            }
            spriteBatch.Draw(_world_texture, pos, sprite.SourceRect, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }
    }
}
