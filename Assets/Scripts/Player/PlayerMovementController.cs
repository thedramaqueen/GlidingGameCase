    using System;
    using DG.Tweening;
    using UnityEngine;

    public class PlayerMovementController: ControllerBase
    {
        #region Fields
        [Range(0,200)]
        [SerializeField] private float throwPower;

        #endregion
        #region Components
        [Header("Components")]
        private Rigidbody rb;
        [SerializeField] private Transform SlingPoint;
        
        #endregion


        #region Unity Functions

        private void Start()
        {
            rb = this.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (PlayerController.Instance.PlayerState == PlayerStates.Sling)
            {
                FollowSling();
            }
            else if (PlayerController.Instance.PlayerState == PlayerStates.Fly)
            {
                MoveForwardWithFly();
                RotatePlayer();
            }
            else if (PlayerController.Instance.PlayerState == PlayerStates.Rotate)
            {
                var velocity = rb.velocity;
                velocity -= Time.deltaTime * Vector3.up * 1000;
                velocity.y = Mathf.Clamp(velocity.y, -50, 200f);
                velocity.x = 0;


                rb.velocity = Vector3.Lerp(rb.velocity, velocity,Time.deltaTime * 50);
                rb.angularVelocity = Vector3.zero;
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
        }

        private void ThrowPlayer(Vector3 forceVector)
        {
            rb.isKinematic = false;
            rb.velocity = forceVector * throwPower;
        }

        #endregion

        private void RotateZero()
        {
            transform.DORotate(Vector3.zero, 1);
        }
        
        
        private void FollowSling()
        {
            transform.position = SlingPoint.position;
            transform.rotation = SlingPoint.rotation;
        }

        private void MoveForwardWithFly()
        {
            rb.velocity = Vector3.Lerp(rb.velocity,
                (Vector3.forward * 50) + (Vector3.right * SwipeController.swipeValue * -20) + (Vector3.up * -3),
                Time.deltaTime * 10);
            rb.angularVelocity = Vector3.zero;
        }
        private void RotatePlayer()
        {
            //var direction = Vector3.Lerp(transform.localEulerAngles, Vector3.right* SwipeController.swipeValue * 90, Time.deltaTime);
            transform.localEulerAngles = new Vector3(
                Remap(SwipeController.swipeValue*-1,-1,1,60,120),
                90,
                Remap(SwipeController.swipeValue,-1,1,60,120));
        }
        
        private float Remap(float value, float oldMin, float oldMax, float newMin, float newMax)
        {
            return newMin + (value - oldMin) * (newMax - newMin) / (oldMax - oldMin);
        }
        
        
        protected override void AddListeners()
        {
            PlayerController.OnPlayerStateChanged += OnPlayerStateChanged;
            SlingController.OnSlingThrowed += ThrowPlayer;
        }

       


        protected override void RemoveListeners()
        {
            PlayerController.OnPlayerStateChanged -= OnPlayerStateChanged;
            SlingController.OnSlingThrowed -= ThrowPlayer;

        }
    }
