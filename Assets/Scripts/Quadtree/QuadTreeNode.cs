using System.Security.Cryptography.X509Certificates;
using System;
using System.Security.AccessControl;
using System.Linq;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeNode<T> {

    private QuadTreeNode() { }
    public QuadTreeNode(int level, Rect rectangle, QuadTreeNode<T> parent)
    {
        _level = level;
        _rectangle = rectangle;
        _parent = parent;

        _entries = new List<NodeEntry<T>>(QuadTree<T>.MAX_OBJECTS);
        childNodes = new List<QuadTreeNode<T>>(4);

#if DEBUG
        _debugEntries = new List<QuadtreeDebugEntry>();
#endif
    }

    public int GlobalObjectCount
    {
        get
        {
            int count = _entries.Count;

            for (int i = 0; i < childNodes.Count; i++)
            {
                count += childNodes[i].GlobalObjectCount;
            }

            return count;
        }
    }

    private readonly QuadTreeNode<T> _parent;
    private readonly int _level;
    private readonly Rect _rectangle;
    private readonly List<NodeEntry<T>> _entries;
    private readonly List<QuadtreeDebugEntry> _debugEntries;

    private List<QuadTreeNode<T>> childNodes;

    public void Query(Rect rect, ref List<T> list)
    {
#if DEBUG
        _debugEntries.Add(new RectDebugEntry(rect, Color.cyan, 0));
        _debugEntries.Add(new RectDebugEntry(_rectangle, Color.white, 0));
#endif

        if(rect.Encompasses(_rectangle))
        {
            GetObjectsRecursive(ref list);
        }
        else if(rect.Overlaps(_rectangle))
        {
            GetObjects(ref list);
            
            for (int i = 0; i < childNodes.Count; i++)
            {
                childNodes[i].Query(rect, ref list);
            }
        }
    }
    public void GetObjects(ref List<T> list)
    {
        for (int i = 0; i < _entries.Count; i++)
        {
#if DEBUG
            _debugEntries.Add(new RectDebugEntry(_entries[i].rect, Color.magenta, 0));
#endif

            list.Add(_entries[i].obj);
        }
    }
    public void GetObjectsRecursive(ref List<T> list)
    {
        GetObjects(ref list);

        for (int i = 0; i < childNodes.Count; i++)
        {
            childNodes[i].GetObjectsRecursive(ref list);
        }
    }
    public bool Fits(Rect objRect)
    {
        return _rectangle.Encompasses(objRect);
    }
    private void PollDestroy()
    {        
        for (int i = 0; i < childNodes.Count; i++)
        {
            if (childNodes[i].GlobalObjectCount > 0)
            {
                return;
            }
        }  
        
        childNodes.Clear();

        if (_parent != null)
            _parent.PollDestroy();
    }
    private NodeEntry<T> GetEntry(T obj)
    {
        for (int i = 0; i < _entries.Count; i++)
        {
            NodeEntry<T> currentEntry = _entries[i];

            if (currentEntry.obj.Equals(obj))
                return currentEntry;
        }

        throw new NullReferenceException();
    }
    public void Add(T obj, Rect objRect)
    {
        for (int i = 0; i < childNodes.Count; i++)
        {
            QuadTreeNode<T> currentNode = childNodes[i];

            if (currentNode.Fits(objRect))
            {
                currentNode.Add(obj, objRect);
                return;
            }
        }

        if(!Fits(objRect) && _parent != null)
        {
            _parent.Add(obj, objRect);
            return;
        }

        TryAddToThisNode(obj, objRect);      
    }
    public bool ContainsRecursive(T obj)
    {
        if (Contains(obj))
        {
            return true;
        }
        else
        {
            for (int i = 0; i < childNodes.Count; i++)
            {
                if (childNodes[i].ContainsRecursive(obj))
                {
                    return true;
                }
            }
        }

        return false;
    }
    public bool Contains(T obj)
    {
        for (int i = 0; i < _entries.Count; i++)
        {
            if (_entries[i].obj.Equals(obj))
                return true;
        }

        return false;
    }
    public void Remove(T obj)
    {
        if(Contains(obj))
        {
            int index = _entries.FindIndex(x => x.obj.Equals(obj));
            _entries.RemoveAt(index);
        }
        else
        {
            childNodes.ForEach(x => x.Remove(obj));
        }        
    }
    public void ClearObjects()
    {
        _entries.Clear();
    }
    private void TryAddToThisNode(T obj, Rect objRect)
    {
        if(IsFull() && _level < QuadTree<T>.MAX_LEVELS && childNodes.Count == 0)
        {
            Subdivide();
            Add(obj, objRect);
        }
        else
        {
            AddToThisNode(obj, objRect);
        }
    }
    private bool IsFull()
    {
        return _entries.Count >= QuadTree<T>.MAX_OBJECTS;
    }
    private void AddToThisNode(T obj, Rect objRect)
    {
        NodeEntry<T> entry = new NodeEntry<T>()
        {
            obj = obj,
            rect = objRect,
        };

        _entries.Add(entry);
    }
    private void Subdivide()
    {
        float size = _rectangle.width / 2;

        childNodes = new List<QuadTreeNode<T>>(4)
        {
            new QuadTreeNode<T>(_level + 1, new Rect(_rectangle.x, _rectangle.y, size, size), this),
            new QuadTreeNode<T>(_level + 1, new Rect(_rectangle.x + size, _rectangle.y, size, size), this),
            new QuadTreeNode<T>(_level + 1, new Rect(_rectangle.x, _rectangle.y + size, size, size), this),
            new QuadTreeNode<T>(_level + 1, new Rect(_rectangle.x + size, _rectangle.y + size, size, size), this),
        };

        List<NodeEntry<T>> objectsToAdd = new List<NodeEntry<T>>(_entries);

        ClearObjects();

        for (int i = 0; i < objectsToAdd.Count; i++)
        {
            NodeEntry<T> entry = objectsToAdd[i];

            Add(entry.obj, entry.rect);
        }
    }
    public void DrawDebug()
    {
        EG_Debug.DrawRect(_rectangle, Color.red, 0, false);

        for (int i = 0; i < _entries.Count; i++)
        {
            NodeEntry<T> entry = _entries[i];

            EG_Debug.DrawRect(entry.rect, Color.white, 0, false);
        }
        for (int i = 0; i < _debugEntries.Count; i++)
        {
            QuadtreeDebugEntry entry = _debugEntries[i];

            if (!entry.IsObsolete())
            {
                entry.Draw();
            }
        }
        _debugEntries.Clear();

        for (int i = 0; i < childNodes.Count; i++)
        {
            childNodes[i].DrawDebug();
        }
    }
    private struct NodeEntry<W>
    {
        public W obj;
        public Rect rect;
    }
    private class RectDebugEntry : QuadtreeDebugEntry
    {
        public RectDebugEntry(Rect rect, Color color, float time) : base()
        {
            this.rect = rect;
            this.color = color;
            this.time = time;
        }

        private readonly Rect rect;
        private readonly Color color;
        private readonly float time;

        public override void Draw()
        {
            EG_Debug.DrawRect(rect, color, time);
        }
    }
    private abstract class QuadtreeDebugEntry
    {
        public QuadtreeDebugEntry()
        {
            frameCount = Time.frameCount;
        }

        private readonly int frameCount;

        public bool IsObsolete()
        {
            return Time.frameCount - frameCount > 3;
        }
        public abstract void Draw();
    }
}
