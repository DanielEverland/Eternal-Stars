using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolBehaviour : MonoBehaviour
{
    [SerializeField]
    private ObjectPool PoolEntries;

    private List<ObjectPool.PoolEntry> _entries;
    private Dictionary<string, List<GameObject>> Instances;
    private Dictionary<int, string> UsedObjects = new Dictionary<int, string>();
    
    protected virtual void Awake()
    {
        _entries = new List<ObjectPool.PoolEntry>(PoolEntries.Entries);

        CreateInstances();
    }
    private void CreateInstances()
    {
        Instances = new Dictionary<string, List<GameObject>>();

        for (int i = 0; i < _entries.Count; i++)
        {
            ObjectPool.PoolEntry entry = _entries[i];

            Instances.Add(entry.Key, new List<GameObject>());

            for (int j = 0; j < entry.Amount; j++)
            {
                GameObject instance = GetInstance(entry.Prefab);

                AddToList(instance, entry.Key);
            }
        }
    }
    public void ReturnObject(GameObject obj)
    {
        if (UsedObjects.ContainsKey(obj.GetInstanceID()))
        {
            ReturnObjectToList(obj);
        }
        else
        {
            Destroy(obj);
        }
    }
    private void ReturnObjectToList(GameObject obj)
    {
        int ID = obj.GetInstanceID();
        string key = UsedObjects[ID];

        UsedObjects.Remove(ID);

        AddToList(obj, key);
    }
    private void AddToList(GameObject obj, string key)
    {
        Instances[key].Add(obj);

        obj.SetActive(false);
        obj.transform.SetParent(transform);
    }
    public GameObject GetObject(string key)
    {
        if (!Instances.ContainsKey(key))
            throw new KeyNotFoundException("Object pool doesn't contain key " + key);

        if (Instances[key].Count > 0)
        {
            return GetObjectFromInstanceList(key);
        }
        else
        {
            return CreateNewObject(key);
        }
    }
    private GameObject GetObjectFromInstanceList(string key)
    {
        GameObject toReturn = Instances[key][0];
        toReturn.transform.SetParent(null);
        toReturn.SetActive(true);

        Instances[key].Remove(toReturn);
        UsedObjects.Add(toReturn.GetInstanceID(), key);

        return toReturn;
    }
    private GameObject CreateNewObject(string key)
    {
        for (int i = 0; i < _entries.Count; i++)
        {
            ObjectPool.PoolEntry entry = _entries[i];

            if (entry.Key == key)
            {
                GameObject obj = GetInstance(entry.Prefab);

                UsedObjects.Add(obj.GetInstanceID(), key);

                return obj;
            }
        }

        throw new NullReferenceException();
    }
    private static GameObject GetInstance(GameObject prefab)
    {
        GameObject obj = GameObject.Instantiate(prefab);
        obj.name = string.Format("{0} ({1})", prefab.name, obj.GetInstanceID());

        return obj;
    }
}
