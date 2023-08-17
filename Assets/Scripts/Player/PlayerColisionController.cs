using System;
using UnityEngine;

public class PlayerColisionController : ControllerBase
{

    public static Action<CollisionObject.CollisionType> OnPlayerCollided;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out CollisionObject obj))
        {
            OnPlayerCollided?.Invoke(obj.myType);
        }
    }

    protected override void AddListeners()
    {
        
    }

    protected override void RemoveListeners()
    {
        
    }
}
