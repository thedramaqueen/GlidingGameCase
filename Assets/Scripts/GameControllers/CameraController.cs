
    using System;
    using Cinemachine;
    using UnityEngine;

    public class CameraController : ControllerBase
    {
        private CinemachineVirtualCamera VirtualCamera;
        private CinemachineTransposer VirtualCameraTransposer;

        public Vector3 SlingPosition;
        public Vector3 FollowPosition;
        
        
        
        private void Start()
        {
            VirtualCamera = this.GetComponent<CinemachineVirtualCamera>();
            VirtualCameraTransposer = VirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }
        
        private void OnPlayerStateChanged(PlayerStates state)
        {

            if (state == PlayerStates.Throw)
            {
                VirtualCameraTransposer.m_FollowOffset = FollowPosition;
            }
        }

        protected override void AddListeners()
        {
            PlayerController.OnPlayerStateChanged += OnPlayerStateChanged;
        }

        

        protected override void RemoveListeners()
        {
            PlayerController.OnPlayerStateChanged -= OnPlayerStateChanged;
        }
    }
