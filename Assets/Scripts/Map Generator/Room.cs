using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    
	public Room()
    {
        GetLayout();
    }

    public IEnumerable<Vector2> Tiles { get { return _tiles; } }
    public IEnumerable<Vector2> LocalTiles { get { return _baseTiles; } }

    public IEnumerable<Vector2> Junctions { get { return _junctions; } }
    public IEnumerable<Vector2> LocalJunctions { get { return _baseJunctions; } }

    public Vector2 Offset { get; private set; }
    public Bounds Bounds { get; private set; }


    private List<Vector2> _tiles;
    private List<Vector2> _junctions;

    private List<Vector2> _baseTiles;
    private List<Vector2> _baseJunctions;
    
    private void GetLayout()
    {
        IRoomLayout layout = GetRandomLayout();

        _tiles = layout.CreateTiles();
        _junctions = layout.CreateJunctions(_tiles);

        _baseTiles = new List<Vector2>(_tiles);
        _baseJunctions = new List<Vector2>(_junctions);

        ApplyOffset(Vector2.zero);
        RecalculateBounds();
    }
    private IRoomLayout GetRandomLayout()
    {
        return LayoutTypes[Random.Range(0, LayoutTypes.Count)];
    }
    public void ApplyOffset(Vector2 offset)
    {
        Offset = offset;

        _tiles = ApplyOffsetToList(_baseTiles, offset);
        _junctions = ApplyOffsetToList(_baseJunctions, offset);

        RecalculateBounds();
    }
    private void RecalculateBounds()
    {
        Vector2 size = new Vector2()
        {
            x = _tiles.Max(x => x.x) - _tiles.Min(x => x.x),
            y = _tiles.Max(x => x.y) - _tiles.Min(x => x.y),
        };

        Bounds = new Bounds(size / 2, size);
    }
    private List<Vector2> ApplyOffsetToList(List<Vector2> list, Vector2 offset)
    {
        List<Vector2> newList = new List<Vector2>();

        for (int i = 0; i < list.Count; i++)
        {
            newList.Add(list[i] + offset);
        }

        return newList;
    }

    #region Layouts
    private List<IRoomLayout> LayoutTypes = new List<IRoomLayout>()
    {
        new SquareLayout(),
        new HallwayLayout(),
    };

    private interface IRoomLayout
    {
        List<Vector2> CreateTiles();
        List<Vector2> CreateJunctions(List<Vector2> tiles);
    }

    private class HallwayLayout : IRoomLayout
    {
        private const byte MIN_SIZE = 3;
        private const byte MAX_SIZE = 10;

        private readonly static List<Vector2> _directions = new List<Vector2>()
        {
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(0, -1),
            new Vector2(-1, 0),
        };
        public List<Vector2> CreateJunctions(List<Vector2> tiles)
        {
            int amountOfJunctions = Random.Range(1, 3);
            List<Vector2> junctions = new List<Vector2>(amountOfJunctions);
            List<Vector2> availableTiles = new List<Vector2>(tiles);

            for (int i = 0; i <= amountOfJunctions; i++)
            {
                Vector2? junction = null;

                while (junction == null && availableTiles.Count > 0)
                {
                    Vector2 tile = availableTiles[Random.Range(0, availableTiles.Count - 1)];
                    availableTiles.Remove(tile);

                    junction = GetJunction(tile, tiles, junctions);
                }

                if(junction != null)
                    junctions.Add(junction.Value);
            }

            return junctions;
        }
        private Vector2? GetJunction(Vector2 anchor, List<Vector2> tiles, List<Vector2> junctions)
        {
            Vector2 selectedJunction = _directions[Random.Range(0, _directions.Count - 1)] + anchor;

            if(!tiles.Contains(selectedJunction) && !junctions.Contains(selectedJunction))
            {
                return selectedJunction;
            }

            return null;
        }

        public List<Vector2> CreateTiles()
        {
            int length = Random.Range(MIN_SIZE, MAX_SIZE);
            Vector2 direction = _directions[Random.Range(0, _directions.Count - 1)];

            List<Vector2> tiles = new List<Vector2>();

            for (int i = 0; i < length; i++)
            {
                tiles.Add(direction * i);
            }

            return tiles;
        }
    }
    private class SquareLayout : IRoomLayout
    {
        private const byte MIN_SIZE = 3;
        private const byte MAX_SIZE = 15;
                
        public List<Vector2> CreateTiles()
        {
            int width = Random.Range(MIN_SIZE, MAX_SIZE);
            int height = Random.Range(MIN_SIZE, MAX_SIZE);

            List<Vector2> toReturn = new List<Vector2>(width * height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    toReturn.Add(new Vector2(x, y));
                }
            }

            return toReturn;
        }
        public List<Vector2> CreateJunctions(List<Vector2> tiles)
        {
            int width = (int)tiles.Max(x => x.x);
            int height = (int)tiles.Max(x => x.y);

            return new List<Vector2>(4)
            {
                new Vector2(0, Random.Range(1, height)),            //Left
                new Vector2(Random.Range(1, width), 0),             //Top

                new Vector2(width, Random.Range(1, height)),    //Right
                new Vector2(Random.Range(1, width), height),    //Bottom
            };
        }
    }

    #endregion

    private const char CHAR_EMPTY    = '_';
    private const char CHAR_TILE     = 'O';
    private const char CHAR_JUNCTION = 'X';

    public override string ToString()
    {
        int widthStart = (int)Tiles.Union(Junctions).Min(x => x.x);
        int heightStart = (int)Tiles.Union(Junctions).Min(x => x.y);
        
        int width = (int)Tiles.Union(Junctions).Max(x => x.x) - widthStart;
        int height = (int)Tiles.Union(Junctions).Max(x => x.y) - heightStart;

        string output = "";

        for (int x = widthStart; x < width; x++)
        {
            for (int y = heightStart; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);

                if (_baseJunctions.Contains(pos))
                {
                    output += CHAR_JUNCTION;
                }
                else
                {
                    if (_baseTiles.Contains(pos))
                    {
                        output += CHAR_TILE;
                    }
                    else
                    {
                        output += CHAR_EMPTY;
                    }
                }
                
            }

            output += "\n";
        }

        return output;
    }
}
