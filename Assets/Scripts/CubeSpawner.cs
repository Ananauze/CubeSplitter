using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;

    private readonly List<Cube> activeCubes = new();

    public Cube Spawn(Vector3 position, Vector3 scale)
    {
        var go = Instantiate(cubePrefab, position, Random.rotation);
        go.transform.localScale = scale;

        var cube = go.GetComponent<Cube>();
        activeCubes.Add(cube);
        cube.OnClicked += HandleCubeClicked;

        return cube;
    }

    private void HandleCubeClicked(Cube cube)
    {
        CubeClicked?.Invoke(cube);
    }

    public event System.Action<Cube> CubeClicked;

    public void DestroyCube(Cube cube)
    {
        if (activeCubes.Remove(cube))
        {
            Destroy(cube.gameObject);
        }
    }
}
