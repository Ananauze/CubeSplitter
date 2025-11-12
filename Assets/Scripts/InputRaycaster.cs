using UnityEngine;
using System;

public class InputRaycaster : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    public event Action<Cube> CubeHit;

    public void ProcessRaycast(Vector2 screenPosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<Cube>(out var cube))
            {
                CubeHit?.Invoke(cube);
            }
        }
    }
}
