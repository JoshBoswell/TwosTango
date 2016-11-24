using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueSnakeGame.MapSystem
{
    using Microsoft.Xna.Framework;
    using Tile = Map.Tile;

    class MapGenerator
    {
        private Point size;

        public MapGenerator(Point size)
        {
            this.size = size;
        }

        public Map GenerateMap()
        {
            Map map = new Map(size);
            FillMap(map, Tile.FLOOR);
            FillEdges(map, Tile.WALL);
            return map;
        }

        private void FillMap(Map map, Tile tile)
        {
            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    map.SetTile(new Point(x, y), tile);
                }
            }
        }

        private void FillEdges(Map map, Tile tile, int borderWidth = 1)
        {
            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    if (x < borderWidth || x >= size.X - borderWidth || y < borderWidth || y >= size.Y - borderWidth)
                        map.SetTile(new Point(x, y), tile);
                }
            }
        }
    }
}
