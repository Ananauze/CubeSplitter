using UnityEngine;

public class Cube : MonoBehaviour
{
    public event System.Action<Cube> OnClicked;

    public void Click()
    {
        OnClicked?.Invoke(this);
        Destroy(gameObject);
    }
}
