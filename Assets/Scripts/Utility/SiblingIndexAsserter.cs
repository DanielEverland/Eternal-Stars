using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiblingIndexAsserter : MonoBehaviour {

    [SerializeField]
    private IndexAnchor Anchor;
    [Range(0, 10)]
    [SerializeField]
    private int Offset;

    private void LateUpdate()
    {
        int index = GetIndex();

        transform.SetSiblingIndex(index);
    }
    private int GetIndex()
    {
        int index = 0;

        if (Anchor == IndexAnchor.Front)
        {
            index = (transform.parent.childCount - 1) - Offset;
        }
        else
        {
            index = Offset;
        }

        return Mathf.Clamp(index, 0, transform.parent.childCount);
    }

    private enum IndexAnchor
    {
        Back,
        Front,
    }
}
