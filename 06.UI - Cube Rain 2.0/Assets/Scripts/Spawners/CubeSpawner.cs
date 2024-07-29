using UnityEngine;

public class CubeSpawner : BaseSpawner<Cube>
{
    [SerializeField, Range(0f, 9f)] private float _spawnRadius = 9f;

    public override Cube Spawn(Vector3 position, Color color)
    {
        position = GetStandardSpawnPosition();

        return base.Spawn(position, color);
    }

    private Vector3 GetStandardSpawnPosition()
    {
        Vector2 random2 = UnityEngine.Random.insideUnitCircle * _spawnRadius;
        Vector3 random3 = new Vector3(random2.x, 0, random2.y);

        return random3 + gameObject.transform.position;
    }
}
