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
        private int width;
        private int height;
        private int tileWidth;
        private int tileHeight;
        private Tile[,] tiles;
        private Texture2D floorTexture;
        private Texture2D wallTexture;

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.tileWidth = 32;
            this.tileHeight = 32;

            tiles = new Tile[width, height];
            FillMap(Tile.FLOOR);
            FillEdges(Tile.WALL);
        }

        public void LoadContent(ContentManager content)
        {
            floorTexture = content.Load<Texture2D>("floor");
            wallTexture = content.Load<Texture2D>("wall");
        }

        public void Draw(SpriteBatch batch)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
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
                    batch.Draw(texture, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), Color.White);
                }
            }
        }

        public bool PlaceFree(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height) // Any tile which is out of bounds is considered impassable
                return false;

            return tiles[x, y] == Tile.FLOOR;
        }

        private void FillMap(Tile tile)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tiles[x, y] = tile;
                }
            }
        }

        private void FillEdges(Tile tile, int borderWidth = 1)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x < borderWidth || x >= width - borderWidth || y < borderWidth || y >= height - borderWidth)
                        tiles[x, y] = tile;
                }
            }
        }

        public enum Tile
        {
            FLOOR,
            WALL
        }
    }
}
