using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CubeSpawner spawner;
    [SerializeField] private ExplosionManager explosionManager;
    [SerializeField] private InputRaycaster raycaster;

    private float splitChance = 1f;

    private const float SplitChanceDecay = 0.5f;
    private const int MinSplitCount = 2;
    private const int MaxSplitCount = 6; 
    private const float SpawnOffsetRadius = 0.5f;
    private const float ScaleReductionFactor = 2f;

    private void Start()
    {
        raycaster.CubeHit += OnCubeHit;

        foreach (var cube in Object.FindObjectsByType<Cube>(FindObjectsSortMode.None))
        {
            cube.OnClicked += OnCubeClicked;
        }

        spawner.CubeClicked += OnCubeClicked;
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

            explosionManager.Explode(cube.transform.position, cubes);

            splitChance *= SplitChanceDecay;
        }

        spawner.DestroyCube(cube);
    }
}
