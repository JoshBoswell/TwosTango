using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueSnakeGame.MapSystem
{
    using Tile = Map.Tile;

    class MapGenerator
    {
        private int width;
        private int height;

        public MapGenerator(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public Map GenerateMap()
        {
            Map map = new Map(width, height);
            FillMap(map, Tile.FLOOR);
            FillEdges(map, Tile.WALL);
            return map;
        }

        private void FillMap(Map map, Tile tile)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map.SetTile(x, y, tile);
                }
            }
        }

        private void FillEdges(Map map, Tile tile, int borderWidth = 1)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x < borderWidth || x >= width - borderWidth || y < borderWidth || y >= height - borderWidth)
                        map.SetTile(x, y, tile);
                }
            }
        }
    }
}
