using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private BaseCollectable _item;
    public void Spawn()
    {
        BaseCollectable coin = Instantiate(_item, gameObject.transform);
        coin.transform.position = gameObject.transform.position;
    }
}
