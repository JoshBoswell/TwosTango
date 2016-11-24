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

            this.tiles = new Tile[width, height];
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

        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }

        public void SetTile(int x, int y, Tile tile)
        {
            tiles[x, y] = tile;
        }

        public enum Tile
        {
            FLOOR,
            WALL
        }
    }
}
