using DG.Tweening;
using UnityEngine;

public class PlayerMovementController : ControllerBase
{
    
    #region Fields

    [Range(0, 200)] [SerializeField] private float throwPower;
    private float JumpTimer = 2;
    [SerializeField] private float JumpPower = 10;
    [SerializeField] private float LeftRightFlyPower = 40;

    #endregion

    #region Components

    [Header("Components")] private Rigidbody rb;
    [SerializeField] private Transform SlingPoint;

    #endregion
    
    #region Unity Functions

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(PlayerController.Instance is null)
            return;
        
        if (PlayerController.Instance.PlayerState == PlayerStates.Sling)
        {
            FollowSling();
        }
        else if (PlayerController.Instance.PlayerState == PlayerStates.Fly)
        {
            MoveForwardWithFly();
            RotateWithFly();
        }
        else if (PlayerController.Instance.PlayerState == PlayerStates.Rotate ||
                 PlayerController.Instance.PlayerState == PlayerStates.Throw)
        {
            RotationMovement();
            rb.velocity -= Time.deltaTime * Vector3.up * rb.mass;
        }
        else if (PlayerController.Instance.PlayerState == PlayerStates.Jump)
        {
            JumpMovement();
            rb.velocity -= Time.deltaTime * Vector3.up * rb.mass;
        }
    }

    #endregion

    #region Events

    private void OnPlayerStateChanged(PlayerStates state)
    {
        if (state == PlayerStates.Rotate)
        {
            RotateZero();
        }

        if (state == PlayerStates.Dead)
        {
            rb.useGravity = true;
        }
    }

    private void OnThrowPlayerWithSLing(Vector3 forceVector)
    {
        rb.isKinematic = false;
        rb.velocity = forceVector * throwPower;
    }

    private void OnPlayerCollided(CollisionObject.CollisionType obj)
    {
        if (obj == CollisionObject.CollisionType.Ground)
            return;

        JumpStart((int)obj);
    }

    private void ResetMovement()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    #endregion

    #region ResetRotation

    private void RotateZero()
    {
        transform.DORotate(Vector3.zero, 1);
    }

    #endregion

    #region Rotation Movement

    private void RotationMovement()
    {
        rb.angularVelocity = Vector3.zero;
    }

    #endregion

    #region Sling

    private void FollowSling()
    {
        transform.position = SlingPoint.position;
        transform.rotation = SlingPoint.rotation;
    }

    #endregion

    #region Fly

    private void MoveForwardWithFly()
    {
        rb.velocity = Vector3.Lerp(rb.velocity,
            (Vector3.forward * 50) + (Vector3.right * SwipeController.swipeValue * -LeftRightFlyPower) +
            (Vector3.up * -3),
            Time.deltaTime * 10);
        rb.angularVelocity = Vector3.zero;
    }

    private void RotateWithFly()
    {
        //var direction = Vector3.Lerp(transform.localEulerAngles, Vector3.right* SwipeController.swipeValue * 90, Time.deltaTime);
        transform.localEulerAngles = new Vector3(
            Remap(SwipeController.swipeValue * -1, -1, 1, 60, 120),
            90,
            Remap(SwipeController.swipeValue, -1, 1, 30, 150));
    }

    #endregion

    #region Jump

    private void JumpStart(int power)
    {
        if (PlayerController.Instance.PlayerState == PlayerStates.Jump)
            return;

        JumpTimer = power / 10.0f;
        var zeroGravity = rb.velocity;
        zeroGravity.y = 0;

        PlayerController.Instance.ChangePlayerState(PlayerStates.Jump);
    }

    private void JumpMovement()
    {
        if (JumpTimer <= 0.0f)
        {
            PlayerController.Instance.ChangePlayerState(PlayerStates.Rotate);
            return;
        }

        JumpTimer -= Time.deltaTime;
        rb.velocity += Vector3.up * JumpPower + (Vector3.forward * JumpPower / 5);
    }

    #endregion
    
    #region Remap

    private float Remap(float value, float oldMin, float oldMax, float newMin, float newMax)
    {
        return newMin + (value - oldMin) * (newMax - newMin) / (oldMax - oldMin);
    }

    #endregion
    
    #region Listeners

    protected override void AddListeners()
    {
        PlayerController.OnPlayerStateChanged += OnPlayerStateChanged;
        SlingController.OnSlingThrowed += OnThrowPlayerWithSLing;
        PlayerColisionController.OnPlayerCollided += OnPlayerCollided;
        GameManager.OnRestartGame += ResetMovement;
    }


    protected override void RemoveListeners()
    {
        PlayerController.OnPlayerStateChanged -= OnPlayerStateChanged;
        SlingController.OnSlingThrowed -= OnThrowPlayerWithSLing;
        PlayerColisionController.OnPlayerCollided -= OnPlayerCollided;
        GameManager.OnRestartGame -= ResetMovement;
    }
    

    #endregion

}