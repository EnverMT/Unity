using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    private ObjectPool<Enemy> _pool;

    public void SetPool(ObjectPool<Enemy> pool)
    {
        _pool = pool;
    }
}
