using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

    [SerializeField]
    private ItemBase toDrop;
   
	private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            SpawnItem();
        }
	}
    private void SpawnItem()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance;
        plane.Raycast(ray, out distance);

        Vector3 point = ray.origin + ray.direction * distance;

        ItemBase itemToDrop = Instantiate(toDrop);
        
        GameObject obj = PlayModeObjectPool.Pool.GetObject("ItemHandler");
        obj.transform.position = point;

        ItemHandler handler = obj.GetComponent<ItemHandler>();
        handler.Initialize(itemToDrop);
    }
}
