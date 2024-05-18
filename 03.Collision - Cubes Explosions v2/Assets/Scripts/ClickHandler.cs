using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private Explosion _explosion;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CubeClicked();
    }

    private void CubeClicked()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.TryGetComponent(out Cube cube))
            {
                float explodeMult = 1 / cube.ScaleMultiplier;
                Vector3 explosionCenter = cube.transform.position;

                if (cube.CanSplit)
                {
                    Cube[] splitedCubes = _cubeSpawner.Split(cube);

                    _explosion.Explode(explosionCenter, explodeMult, splitedCubes);
                }
                else
                {
                    _explosion.Explode(explosionCenter, explodeMult);
                }

                Destroy(cube.gameObject);
            }
        }
    }
}
