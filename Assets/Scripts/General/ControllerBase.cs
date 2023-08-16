using UnityEngine;


public abstract class ControllerBase : MonoBehaviour
{
    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    protected abstract void AddListeners();
    protected abstract void RemoveListeners();
}