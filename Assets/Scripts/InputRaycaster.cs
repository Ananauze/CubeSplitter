using UnityEngine;
using System;

public class InputRaycaster : MonoBehaviour
{
    public event Action<Cube> CubeHit;
    [SerializeField] private Camera mainCamera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked"); 

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Raycast hit: " + hit.collider.name); 

                if (hit.collider.TryGetComponent(out Cube cube))
                {
                    Debug.Log("Hit cube: " + cube.name);
                    CubeHit?.Invoke(cube);
                }
            }
            else
            {
                Debug.Log("Raycast hit nothing"); 
            }
        }
    }
}
