using UnityEngine;

public class CubeSplitter : MonoBehaviour
{
    private static float splitChance = 1.0f; 

    [SerializeField] private int minCubeCount = 2;
    [SerializeField] private int maxCubeCount = 6;
    [SerializeField] private float splitChanceDecay = 0.5f;
    [SerializeField] private float minCubeScale = 0.4f;
    [SerializeField] private float scaleReductionFactor = 2f;
    [SerializeField] private float explosionForce = 300f;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float spawnOffset = 0.5f;
    [SerializeField] private GameObject cubePrefab;

    private bool hasSplit = false;

    public void Split()
    {
        if (hasSplit) return;
        hasSplit = true;

        Vector3 position = transform.position;
        Vector3 scale = transform.localScale;

        if (scale.x <= minCubeScale)
        {
            Destroy(gameObject);
            return;
        }

        bool shouldSplit = Random.value < splitChance;

        if (shouldSplit)
        {
            int count = Random.Range(minCubeCount, maxCubeCount + 1);

            for (int i = 0; i < count; i++)
            {
                Vector3 spawnPos = position + Random.insideUnitSphere * spawnOffset;

                GameObject newCube = Instantiate(cubePrefab, spawnPos, Random.rotation);
                newCube.transform.localScale = scale / scaleReductionFactor;

                if (newCube.TryGetComponent(out Renderer rend))
                    rend.material.color = new Color(Random.value, Random.value, Random.value);

                Rigidbody rb = newCube.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, position, explosionRadius);
                }
            }

            splitChance *= splitChanceDecay;
        }

        Destroy(gameObject);
    }
}
