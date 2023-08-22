using System;
using UnityEngine;

public class PlayerColisionController : ControllerBase
{

    public static Action<CollisionObject.CollisionType> OnPlayerCollided;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Triggered");
        if (collision.transform.TryGetComponent(out CollisionObject obj))
        {
            //Debug.Log($"{obj.myType}");
            OnPlayerCollided?.Invoke(obj.myType);
        }
    }

    #region Listeners

    protected override void AddListeners()
    {
        
    }

    protected override void RemoveListeners()
    {
        
    }

    #endregion
}
