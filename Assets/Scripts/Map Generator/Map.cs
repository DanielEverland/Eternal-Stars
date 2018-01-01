using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

    public Map(int level)
    {
        _level = level;

        Initialize();
    }
    public Map()
    {
        Initialize();
    }

    public static Map CurrentMap { get; private set; }

    public int Level { get { return _level; } }

    private readonly int _level;

    private List<Vector2> _tiles;

    private const int MIN_ROOMS = 10;
    private const int MAX_ROOMS = 15;

    private int roomsCreated;

    private void Initialize()
    {
        _tiles = new List<Vector2>();

        CreateRooms();

        CurrentMap = this;

        for (int i = 0; i < _tiles.Count; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            obj.transform.position = _tiles[i];
        }
    }
    private void CreateRooms()
    {
        int amountOfRoomsToGenerate = UnityEngine.Random.Range(MIN_ROOMS, MAX_ROOMS);
        int roomCreationAttempts = 0;
        roomsCreated = 0;
        
        while (roomsCreated < amountOfRoomsToGenerate && roomCreationAttempts < 1000)
        {
            roomCreationAttempts++;

            CreateRoom();
        }
    }
    private void CreateRoom()
    {
        Room newRoom = new Room();
        Vector2 anchorPosition = new Vector2();

        if (_tiles.Count == 0)
        {
            AddRoomToMap(newRoom);

            return;
        }            

        for (int i = 0; i < _tiles.Count; i++)
        {
            if (HasAvailableNeighbor(_tiles[i], ref anchorPosition))
            {
                if(TryFitRoom(newRoom, anchorPosition))
                {
                    return;
                }
            }
        }
    }
    private bool TryFitRoom(Room room, Vector2 anchor)
    {
        int amountOfJunctions = room.Junctions.Count();

        for (int i = 0; i < amountOfJunctions; i++)
        {
            Vector2 currentJunction = room.LocalJunctions.ElementAt(i);

            room.ApplyOffset(anchor - currentJunction);

            bool canFit = true;

            foreach (Vector2 roomTile in room.Tiles)
            {
                if (_tiles.Contains(roomTile))
                {
                    canFit = false;

                    break;
                }
            }

            if (canFit)
            {
                if(!room.LocalTiles.Contains(currentJunction))
                {
                    _tiles.Add(currentJunction + room.Offset);
                }

                AddRoomToMap(room);

                return true;
            }
        }

        return false;
    }
    private void AddRoomToMap(Room room)
    {
        roomsCreated++;

        _tiles.AddRange(room.Tiles);
    }
    private bool HasAvailableNeighbor(Vector2 position, ref Vector2 neighborPosition)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (y != 0 && x != 0)
                    continue;

                if(!_tiles.Contains(position + new Vector2(x, y)))
                {
                    neighborPosition = position + new Vector2(x, y);

                    return true;
                }
            }
        }

        return false;
    }
}
