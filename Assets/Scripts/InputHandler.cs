using UnityEngine;
using System;

public class InputHandler : MonoBehaviour
{
    public event Action<Vector2> OnClickPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClickPosition?.Invoke(Input.mousePosition);
        }
    }
}
