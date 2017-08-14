using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTree<T> {

    private QuadTree() { }
    public QuadTree(uint size)
    {
        if (!size.IsPowerOfTwo())
        {
            throw new ArgumentException("Quadtree size must be power of two");
        }

        this.size = (int)size;

        Clear();
    }
    
    public int ObjectCount { get { return _rootNode.GlobalObjectCount; } }

    public const int MAX_OBJECTS = 1;
    public const int MAX_LEVELS = 10;

    private readonly int size;

    private QuadTreeNode<T> _rootNode;

    public List<T> GetNearbyObjects(Vector3 position, Vector2 size)
    {
        Vector2 fixedPos = new Vector2()
        {
            x = position.x,
            y = position.z,
        };

        return GetNearbyObjects(fixedPos, size);
    }
    public List<T> GetNearbyObjects(Vector2 position, Vector2 size)
    {
        return GetNearbyObjects(new Rect(position - size / 2, size));
    }
    public List<T> GetNearbyObjects(Rect rect)
    {
        List<T> list = new List<T>();

        _rootNode.Query(rect, ref list);

        return list;
    }
    public void Add(T obj, Rect objRect)
    {
        _rootNode.Add(obj, objRect);
    }
    public void Remove(T obj)
    {
        _rootNode.Remove(obj);
    }
    public void DrawDebug()
    {
        _rootNode.DrawDebug();
    }
    public void Clear()
    {
        Rect rootRect = new Rect(-(size / 2), -(size / 2), size, size);

        _rootNode = new QuadTreeNode<T>(0, rootRect, null);
    }
}
