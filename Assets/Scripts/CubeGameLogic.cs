using UnityEngine;

public class CubeGameLogic : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeExplosion _explosionEffect;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private InputRaycaster _raycastProcessor;

    private float splitChance = 1f;

    private const float SplitChanceDecay = 0.5f;
    private const int MinSplitCount = 2;
    private const int MaxSplitCount = 5;
    private const float SpawnOffsetRadius = 0.5f;
    private const float ScaleReductionFactor = 2f;

    private void OnEnable()
    {
        _inputHandler.OnClickPosition += _raycastProcessor.ProcessRaycast;
        _raycastProcessor.CubeHit += OnCubeHit;
        _spawner.CubeClicked += OnCubeClicked;

        foreach (var cube in Object.FindObjectsByType<Cube>(FindObjectsSortMode.None))
        {
            cube.OnClicked += OnCubeClicked;
        }
    }

    private void OnDisable()
    {
        _inputHandler.OnClickPosition -= _raycastProcessor.ProcessRaycast;
        _raycastProcessor.CubeHit -= OnCubeHit;
        _spawner.CubeClicked -= OnCubeClicked;

        foreach (var cube in Object.FindObjectsByType<Cube>(FindObjectsSortMode.None))
        {
            if (cube != null)
            {
                cube.OnClicked -= OnCubeClicked;
            }
        }
    }

    private void OnCubeHit(Cube cube)
    {
        OnCubeClicked(cube);
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
                cubes[i] = _spawner.Spawn(cube.transform.position + offset, cube.transform.localScale / ScaleReductionFactor);
            }

            _explosionEffect.Explode(cube.transform.position, cubes);

            splitChance *= SplitChanceDecay;
        }

        _spawner.DestroyCube(cube);
    }
}
