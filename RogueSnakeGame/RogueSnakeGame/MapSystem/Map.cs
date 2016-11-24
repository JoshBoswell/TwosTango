using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueSnakeGame.MapSystem
{
    class Map
    {
        private Point size;
        private Point tileSize;
        private Tile[,] tiles;
        private Texture2D floorTexture;
        private Texture2D wallTexture;

        public Map(Point size)
        {
            this.size = size;
            this.tileSize = new Point(32, 32);

            this.tiles = new Tile[size.X, size.Y];
        }

        public void LoadContent(ContentManager content)
        {
            floorTexture = content.Load<Texture2D>("floor");
            wallTexture = content.Load<Texture2D>("wall");
        }

        public void Draw(SpriteBatch batch)
        {
            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    Texture2D texture = floorTexture;
                    switch (tiles[x, y])
                    {
                        case Tile.FLOOR:
                            texture = floorTexture;
                            break;
                        case Tile.WALL:
                            texture = wallTexture;
                            break;
                    }
                    batch.Draw(texture, new Rectangle(x * tileSize.X, y * tileSize.Y, tileSize.X, tileSize.Y), Color.White);
                }
            }
        }

        public bool PlaceFree(Point pos)
        {
            if (pos.X < 0 || pos.X >= size.X || pos.Y < 0 || pos.Y >= size.Y) // Any tile which is out of bounds is considered impassable
                return false;

            return tiles[pos.X, pos.Y] == Tile.FLOOR;
        }

        public Tile GetTile(Point pos)
        {
            return tiles[pos.X, pos.Y];
        }

        public void SetTile(Point pos, Tile tile)
        {
            tiles[pos.X, pos.Y] = tile;
        }

        public enum Tile
        {
            FLOOR,
            WALL
        }
    }
}
