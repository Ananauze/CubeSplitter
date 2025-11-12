using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private float explosionForce = 300f;
    [SerializeField] private float explosionRadius = 2f;

    public void Explode(Vector3 center, Cube[] cubes)
    {
        foreach (var cube in cubes)
        {
            if (cube.TryGetComponent(out Rigidbody rb))
                rb.AddExplosionForce(explosionForce, center, explosionRadius);
        }
    }
}
