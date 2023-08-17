
using UnityEngine;

public class CollisionObject : MonoBehaviour
{
    public enum CollisionType
    {
        Ground = 0,
        Cube = 1,
        Cylinder = 2
    }

    public CollisionType myType;


    private void OnValidate()
    {
        
        foreach (var child in transform)
        {
            var obj = child as Transform;

            if (obj.name.Contains(myType.ToString()))
            {
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj.gameObject.SetActive(false);
            }
        }
        
    }
}
