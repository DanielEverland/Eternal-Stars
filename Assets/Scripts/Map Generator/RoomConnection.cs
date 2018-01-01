using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConnection {

    private RoomConnection() { }
    public RoomConnection(Room room, Vector2 connectionTile)
    {
        _targetRoom = room;
        _conncetionTile = connectionTile;
    }

    public Room Room { get { return _targetRoom; } }
    public Vector2 ConnectionTile { get { return _conncetionTile; } }

    private readonly Room _targetRoom;
    private readonly Vector2 _conncetionTile;
}
