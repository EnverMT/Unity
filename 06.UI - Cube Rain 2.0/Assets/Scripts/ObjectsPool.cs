using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsPool : MonoBehaviour
{
    private Dictionary<string, object> pools = new();

    public T GetObject<T>(T prefab, int initialSize = 10, int maxSize = 100) where T : BaseFieldObject
    {
        if (pools.ContainsKey(typeof(T).Name) == false)
        {
            ObjectPool<T> newPool = new ObjectPool<T>(() => Instantiate(prefab),
                                                      obj => obj.gameObject.SetActive(true),
                                                      obj => obj.gameObject.SetActive(false),
                                                      obj => Destroy(obj.gameObject),
                                                      true, initialSize, maxSize);
            pools.Add(typeof(T).Name, newPool);
        }

        return ((ObjectPool<T>)pools[typeof(T).Name]).Get();
    }

    public void ReturnObject<T>(T obj) where T : BaseFieldObject
    {
        if (pools.ContainsKey(typeof(T).Name) == true)
            ((ObjectPool<T>)pools[typeof(T).Name]).Release(obj);
        else
            Debug.LogError("Pool with key " + typeof(T).Name + " does not exist.");
    }

    public int GetCount<T>() where T : BaseFieldObject
    {
        if (pools.ContainsKey(typeof(T).Name) == true)
            return ((ObjectPool<T>)pools[typeof(T).Name]).CountActive;
        else
            return 0;
    }
}