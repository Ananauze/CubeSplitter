using UnityEngine;

public class CubeGameplay : MonoBehaviour
{
    [SerializeField] private CubeSpawner spawner;
    [SerializeField] private CubeExplosion explosionEffect;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private RaycastProcessor raycastProcessor;

    private float splitChance = 1f;

    private const float SplitChanceDecay = 0.5f;
    private const int MinSplitCount = 2;
    private const int MaxSplitCount = 5;
    private const float SpawnOffsetRadius = 0.5f;
    private const float ScaleReductionFactor = 2f;

    private void OnEnable()
    {
        inputHandler.OnClickPosition += raycastProcessor.ProcessRaycast;
        raycastProcessor.CubeHit += OnCubeHit;
        spawner.CubeClicked += OnCubeClicked;
    }

    private void OnDisable()
    {
        inputHandler.OnClickPosition -= raycastProcessor.ProcessRaycast;
        raycastProcessor.CubeHit -= OnCubeHit;
        spawner.CubeClicked -= OnCubeClicked;
    }

    private void OnCubeHit(Cube cube)
    {
        cube.Click();
    }

    private void OnCubeClicked(Cube cube)
    {
        if (Random.value < splitChance)
        {
            int count = Random.Range(MinSplitCount, MaxSplitCount + 1);
            var cubes = new Cube[count];

            for (int i = 0; i < count; i++)
            {
                Vector3 offset = Random.insideUnitSphere * SpawnOffsetRadius;
                cubes[i] = spawner.Spawn(cube.transform.position + offset, cube.transform.localScale / ScaleReductionFactor);
            }

            explosionEffect.Explode(cube.transform.position, cubes);

            splitChance *= SplitChanceDecay;
        }

        spawner.DestroyCube(cube);
    }
}
