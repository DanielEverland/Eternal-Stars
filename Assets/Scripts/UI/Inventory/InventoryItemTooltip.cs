using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemTooltip : MonoBehaviour {

    private int lastFrameTicked;

    public void Initialize(ItemBase item)
    {

    }
    public void Tick()
    {
        lastFrameTicked = Time.frameCount;
    }
    private void Update()
    {
        PollDestroy();
    }
    private void PollDestroy()
    {
        if(Time.frameCount - lastFrameTicked > 1)
        {
            InventoryItemTooltipManager.DeleteItemTooltip();
        }
    }
}
