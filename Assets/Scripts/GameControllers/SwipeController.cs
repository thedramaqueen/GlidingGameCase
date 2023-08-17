using UnityEngine;

public class SwipeController : MonoBehaviour
{
    
    #region Fields

    private Vector3 mouseStartPosition;
    public static float swipeValue;

    public float maxSwipeDistance = Screen.width/2;
    

    #endregion
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPosition = Input.mousePosition;
            swipeValue = 0.0f;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 swipeDelta = mouseStartPosition - Input.mousePosition;
            swipeValue = Mathf.Clamp(swipeDelta.x / maxSwipeDistance, -1.0f, 1.0f);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            swipeValue = 0.0f;
        }
    }
    
}