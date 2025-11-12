using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    public event System.Action<Cube> CubeClicked;

    private readonly List<Cube> _activeCubes = new();

    public Cube Spawn(Vector3 position, Vector3 scale)
    {
        var cube = Instantiate(_cubePrefab, position, Random.rotation);
        cube.transform.localScale = scale;

        _activeCubes.Add(cube);
        cube.OnClicked += HandleCubeClicked;

        return cube;
    }

    private void HandleCubeClicked(Cube cube)
    {
        CubeClicked?.Invoke(cube);
    }

    public void DestroyCube(Cube cube)
    {
        _activeCubes.Remove(cube);
        Destroy(cube.gameObject);
    }
}
