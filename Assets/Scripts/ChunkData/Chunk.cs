using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk<T> {

	private Chunk() { }
    public Chunk(uint size)
    {
        _size = (int)size;

        _objects = new T[_size, _size];
    }

    private readonly int _size;

    private T[,] _objects;

    public T this[uint x, uint y]
    {
        get
        {
            if (IsOutOfBounds(x, y))
                throw new System.IndexOutOfRangeException();

            return _objects[(int)x, (int)y];
        }
        protected set
        {
            if (IsOutOfBounds(x, y))
                throw new System.IndexOutOfRangeException();

            _objects[(int)x, (int)y] = value;
        }
    }
    protected bool IsOutOfBounds(uint x, uint y)
    {
        return x >= _size || y >= _size;
    }
    protected bool IsOutOfBounds(Vector2 position)
    {
        return position.x < 0 || position.y < 0 || position.x >= _size || position.y >= _size;
    }
    public void Add(IntVector2 localPosition, T obj)
    {
        if (IsOutOfBounds(localPosition))
            throw new System.IndexOutOfRangeException();

        this[(uint)localPosition.x, (uint)localPosition.y] = obj;

        OnAdded(localPosition, obj);
        OnUpdate();
    }
    public void Remove(IntVector2 localPosition)
    {
        if (IsOutOfBounds(localPosition))
            throw new System.IndexOutOfRangeException();

        T obj = this[(uint)localPosition.x, (uint)localPosition.y];

        this[(uint)localPosition.x, (uint)localPosition.y] = default(T);

        OnRemoved(localPosition, obj);
        OnUpdate();
    }

    protected virtual void OnUpdate() { }
    protected virtual void OnAdded(IntVector2 localPosition, T obj) { }
    protected virtual void OnRemoved(IntVector2 localPosition, T obj) { }
}
