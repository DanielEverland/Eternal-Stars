using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct IntVector2 {

    public IntVector2(int x, int y)
    {
        this._x = x;
        this._y = y;
    }

    public int x { get { return _x; } set { _x = value; } }
    public int y { get { return _y; } set { _y = value; } }

    [SerializeField]
    private int _x;
    [SerializeField]
    private int _y;

    public static IntVector2 down { get { return new IntVector2(0, -1); } }
    public static IntVector2 up { get { return new IntVector2(0, 1); } }
    public static IntVector2 one { get { return new IntVector2(1, 1); } }
    public static IntVector2 zero { get { return new IntVector2(0, 0); } }
    public static IntVector2 left { get { return new IntVector2(-1, 0); } }
    public static IntVector2 right { get { return new IntVector2(1, 0); } }

    /// <summary>
    /// Returns the length of the vector
    /// </summary>
    public float magnitude
    {
        get
        {
            return Mathf.Sqrt(x * x + y * y);
        }
    }
    /// <summary>
    /// Faster computation of length (x * x + y * y)
    /// </summary>
    public float sqrMagnitude
    {
        get
        {
            return x * x + y * y;
        }
    }
    public IntVector2 normalized
    {
        get
        {
            IntVector2 toReturn = new IntVector2();
            float sqrLength = sqrMagnitude;

            if (sqrLength != 0)
            {
                toReturn.x = Mathf.RoundToInt(x / sqrLength);
                toReturn.y = Mathf.RoundToInt(y / sqrLength);
            }

            return toReturn;
        }
    }


    /// <summary>
    /// Changes the length of the vector to 1, while retaining the direction
    /// </summary>
    public void Normalize()
    {
        IntVector2 normal = normalized;

        x = normal.x;
        y = normal.y;
    }


    public override bool Equals(object obj)
    {
        if (obj is IntVector2)
        {
            IntVector2 other = (IntVector2)obj;

            return other.x.Equals(x) && other.y.Equals(y);
        }

        return false;
    }
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 13;

            hash *= 17 + x.GetHashCode();
            hash *= 17 + y.GetHashCode();

            return hash;
        }
    }
    public override string ToString()
    {
        return string.Format("{0}, {1}", x.ToString(), y.ToString());
    }

    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
    {
        return new IntVector2(a.x + b.x, a.y + b.y);
    }
    public static IntVector2 operator -(IntVector2 a)
    {
        return new IntVector2(-a.x, -a.y);
    }
    public static IntVector2 operator -(IntVector2 a, IntVector2 b)
    {
        return new IntVector2(a.x - b.x, a.y - b.y);
    }
    public static IntVector2 operator *(float d, IntVector2 a)
    {
        return new IntVector2(Mathf.RoundToInt(a.x * d), Mathf.RoundToInt(a.y * d));
    }
    public static IntVector2 operator *(IntVector2 a, float d)
    {
        return new IntVector2(Mathf.RoundToInt(a.x * d), Mathf.RoundToInt(a.y * d));
    }
    public static IntVector2 operator /(IntVector2 a, float d)
    {
        return new IntVector2(Mathf.RoundToInt(a.x / d), Mathf.RoundToInt(a.y / d));
    }
    public static bool operator ==(IntVector2 lhs, IntVector2 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }
    public static bool operator !=(IntVector2 lhs, IntVector2 rhs)
    {
        return lhs.x != rhs.x && lhs.y != rhs.y;
    }

    public static implicit operator Vector3(IntVector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }
    public static explicit operator IntVector2(Vector3 v)
    {
        return new IntVector2(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
    }
    public static implicit operator Vector2(IntVector2 v)
    {
        return new Vector2(v.x, v.y);
    }
    public static explicit operator IntVector2(Vector2 v)
    {
        return new IntVector2(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
    }
}
