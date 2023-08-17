
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

}
