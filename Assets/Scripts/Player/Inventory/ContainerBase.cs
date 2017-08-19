using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerBase {

	public ContainerBase(int size)
    {
        _containerSize = size;
    }
    private ContainerBase() { }

    public int Size { get { return _containerSize; } }

    private readonly int _containerSize;
}
