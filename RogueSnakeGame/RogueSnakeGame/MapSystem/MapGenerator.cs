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
            SplitIntoRooms(map, new Point(4, 4));
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

        private void SplitIntoRooms(Map map, Point minRoomSize, int wallThickness = 1)
        {
            Random r = new Random();

            List<Rectangle> finishedAreas = new List<Rectangle>();
            Stack<Rectangle> areas = new Stack<Rectangle>();

            areas.Push(new Rectangle(Point.Zero, size));

            while (areas.Count() > 0)
            {
                Rectangle area = areas.Pop();

                if (area.Height > area.Width)
                {
                    if (area.Height < (minRoomSize.Y + wallThickness * 2) * 2)
                    {
                        finishedAreas.Add(area);
                    }
                    else
                    {
                        int split = r.Next((area.Height - minRoomSize.Y) - minRoomSize.Y) + minRoomSize.Y;
                        Rectangle top = new Rectangle(area.Location, new Point(area.Size.X, split));
                        Rectangle bottom = new Rectangle(new Point(area.Location.X, area.Location.Y + split), new Point(area.Width, area.Height - split));
                        areas.Push(top);
                        areas.Push(bottom);
                    }
                }
                else
                {
                    if (area.Width < (minRoomSize.X + wallThickness * 2) * 2)
                    {
                        finishedAreas.Add(area);
                    }
                    else
                    {
                        int split = r.Next((area.Width - minRoomSize.X) - minRoomSize.X) + minRoomSize.X;
                        Rectangle left = new Rectangle(area.Location, new Point(split, area.Height));
                        Rectangle right = new Rectangle(new Point(area.Location.X + split, area.Location.Y), new Point(area.Width - split, area.Height));
                        areas.Push(left);
                        areas.Push(right);
                    }
                }
            }

            foreach (Rectangle area in finishedAreas)
            {
                for (int y = area.Y; y < area.Y + area.Height; y++)
                {
                    for (int x = area.X; x < area.X + area.Width; x++)
                    {
                        if (x < area.X + wallThickness || x >= area.X + area.Width - wallThickness || y < area.Y + wallThickness || y >= area.Y + area.Height - wallThickness)
                            map.SetTile(new Point(x, y), Tile.WALL);
                        else
                            map.SetTile(new Point(x, y), Tile.FLOOR);
                    }
                }
            }
        }
    }
}
