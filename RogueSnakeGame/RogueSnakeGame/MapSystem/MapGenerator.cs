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
            FillMap(map, Tile.WALL);
            PlaceRooms(map, new Point(3, 3), new Point(10, 10), 200);
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

        private void PlaceRooms(Map map, Point minRoomSize, Point maxRoomSize, int attempts, int padding = 1, int avoidEdges = 1, Tile tile = Tile.FLOOR)
        {
            List<Rectangle> rooms = new List<Rectangle>();
            for (int i = 0; i < attempts; i++)
            {
                Point roomSize = new Point(Util.Range(minRoomSize.X, maxRoomSize.X + 1), Util.Range(minRoomSize.Y, maxRoomSize.Y + 1));
                Rectangle room = new Rectangle(new Point(Util.Range(avoidEdges, size.X - roomSize.X - avoidEdges), Util.Range(avoidEdges, size.Y - roomSize.Y - avoidEdges)), roomSize);
                bool collides = false;

                Rectangle roomWithPadding = new Rectangle(new Point(room.X - padding, room.Y - padding), new Point(room.Width + padding * 2, room.Height + padding * 2));
                foreach (Rectangle other in rooms)
                {
                    if (roomWithPadding.Intersects(other))
                    {
                        collides = true;
                        break;
                    }
                }

                if (!collides)
                    rooms.Add(room);
            }

            foreach(Rectangle room in rooms)
            {
                for (int y = room.Y; y < room.Y + room.Height; y++)
                {
                    for (int x = room.X; x < room.X + room.Width; x++)
                    {
                        map.SetTile(new Point(x, y), tile);
                    }
                }
            }
        }
    }
}
