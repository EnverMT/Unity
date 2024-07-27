using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsPool : MonoBehaviour
{
    private Dictionary<System.Type, ObjectPool<BaseFieldObject>> _typedPools = new();

    public T GetObject<T>(T prefab, int initialSize = 10, int maxSize = 100) where T : BaseFieldObject
    {
        System.Type type = typeof(T);

        if (_typedPools.ContainsKey(type) == false)
        {
            ObjectPool<BaseFieldObject> newPool = new ObjectPool<BaseFieldObject>(() => Instantiate(prefab),
                                                      obj => obj.gameObject.SetActive(true),
                                                      obj => obj.gameObject.SetActive(false),
                                                      obj => Destroy(obj.gameObject),
                                                      true, initialSize, maxSize);
            _typedPools.Add(type, newPool);
        }

        return (T)_typedPools[type].Get();
    }

    public void ReturnObject<T>(T obj) where T : BaseFieldObject
    {
        System.Type type = typeof(T);

        if (_typedPools.ContainsKey(type) == true)
            _typedPools[type].Release(obj);
        else
            Debug.LogError("Pool with key " + type + " does not exist.");
    }

    public int GetCount<T>() where T : BaseFieldObject
    {
        if (_typedPools.ContainsKey(typeof(T)) == true)
            return _typedPools[typeof(T)].CountActive;
        else
            return 0;
    }
}