using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomTooltipLoadout {

    protected ItemBase Item { get { return Tooltip.Item; } }
    protected ItemTooltip Tooltip { get; private set; }

	public void Initialize(ItemTooltip tooltip)
    {
        Tooltip = tooltip;

        CreateLayout();
    }
    protected abstract void CreateLayout();
    public virtual void OnReturned() { }
}
