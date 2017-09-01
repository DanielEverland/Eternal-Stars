using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainerBase {

    bool Fits(object index, ItemBase item);
    void Remove(ItemStack stack);
    void Add(object index, ItemStack stack);
}
