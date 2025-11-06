using UnityEngine;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                CubeSplitter splitter = hit.collider.GetComponent<CubeSplitter>();
                if (splitter != null)
                {
                    splitter.Split();
                }
            }
        }
    }
}
