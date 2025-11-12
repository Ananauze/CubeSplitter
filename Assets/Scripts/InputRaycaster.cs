using UnityEngine;
using System;

public class RaycastProcessor : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public event Action<Cube> CubeHit;

    public void ProcessRaycast(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<Cube>(out var cube))
            {
                CubeHit?.Invoke(cube);
            }
        }
    }
}
