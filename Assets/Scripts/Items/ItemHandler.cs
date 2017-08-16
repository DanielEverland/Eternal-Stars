using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour {

    public ItemBase Item { get { return item; } }

    [SerializeField]
    private List<EffectEntry> effectEntries;
    
    private ItemBase item;
    private ItemHandlerLabel label;

	public void Initialize(ItemBase item)
    {
        this.item = item;
        
        PollDisplayEffect();
        CreateLabel();
    }
    private void CreateLabel()
    {
        label = PlayModeObjectPool.Pool.GetObject("ItemHandlerLabel").GetComponent<ItemHandlerLabel>();
        label.Initialize(this);

        label.transform.SetParent(Canvas2D.Instance.transform);
    }
    private void OnReturned()
    {
        label.Return();
    }
    private void PollDisplayEffect()
    {
        for (int i = 0; i < effectEntries.Count; i++)
        {
            EffectEntry entry = effectEntries[i];

            entry.parent.SetActive(false);

            if (entry.rarities.Contains(item.Rarity))
                EnableEffect(entry);
        }
    }
    private void EnableEffect(EffectEntry entry)
    {
        entry.parent.SetActive(true);

        Color color = item.Rarity.Color;

        for (int i = 0; i < entry.lights.Length; i++)
        {
            entry.lights[i].color = color;
        }
        for (int i = 0; i < entry.renderers.Length; i++)
        {
            Color colorToAssign = color;
            colorToAssign.a = entry.renderers[i].material.GetColor("_TintColor").a;

            entry.renderers[i].material.SetColor("_TintColor", colorToAssign);
        }
        for (int i = 0; i < entry.lineRenderers.Length; i++)
        {
            LineRenderer renderer = entry.lineRenderers[i];
            Gradient gradient = renderer.colorGradient;

            GradientColorKey[] keys = gradient.colorKeys;

            for (int j = 0; j < keys.Length; j++)
            {
                keys[j].color = color;
            }

            gradient.SetKeys(keys, gradient.alphaKeys);
            renderer.colorGradient = gradient;
        }
    }
    [System.Serializable]
    private struct EffectEntry
    {
        public GameObject parent;
        public List<Rarity> rarities;
        public Light[] lights;
        public Renderer[] renderers;
        public LineRenderer[] lineRenderers;
    }
}
