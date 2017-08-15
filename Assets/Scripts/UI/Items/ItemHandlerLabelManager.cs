using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandlerLabelManager : MonoBehaviour {
    
    private static List<ItemHandlerLabel> labels = new List<ItemHandlerLabel>();

    private const float MOVEMENT_SPEED = 30;

    private int i = 0;
    
    private void LateUpdate()
    {
        AlignAllLabels();
    }
    private void AlignAllLabels()
    {
        Vector2 center = new Vector2()
        {
            x = Screen.width / 2,
            y = Screen.height / 2,
        };

        for (int i = 0; i < labels.Count; i++)
        {
            ItemHandlerLabel label = labels[i];

            Vector2 movementVector = label.Rect.center - center;

            if (movementVector == Vector2.zero)
                movementVector = Vector2.up;

            while (Intersects(label, labels))
            {
                label.rectTransform.position += (Vector3)(movementVector.normalized * MOVEMENT_SPEED);
            }
        }
    }
    private bool Intersects(ItemHandlerLabel label, List<ItemHandlerLabel> source)
    {
        for (int i = 0; i < source.Count; i++)
        {
            ItemHandlerLabel toCompare = source[i];

            if (toCompare == label)
                continue;

            if (toCompare.Rect.Overlaps(label.Rect))
            {
                return true;
            }
        }

        return false;
    }

	public static void AddLabel(ItemHandlerLabel label)
    {
        labels.Add(label);
    }
    public static void RemoveLabel(ItemHandlerLabel label)
    {
        labels.Remove(label);
    }
}
