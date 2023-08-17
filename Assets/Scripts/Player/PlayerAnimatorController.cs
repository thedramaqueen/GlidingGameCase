using UnityEngine;

namespace Player
{
    public class PlayerAnimatorController : ControllerBase
    {
        
        #region Fields

        private const string FLY = "FLY";
        private const string ROTATE = "ROTATE";
        private const string THROW = "THROW";
        private const string SLING = "SLING";

        #endregion

        #region Components

        public Animator PlayerAnimator;

        public TrailRenderer LTR, RTR;

        #endregion
        
        private void OnPlayerStateChanged(PlayerStates state)
        {
            switch (state)
            {
                case PlayerStates.Sling:
                    SetAnimEnable();
                    PlayAnimation(SLING);
                    
                    break;
                case PlayerStates.Throw:
                    PlayAnimation(THROW);
                    break;
                case PlayerStates.Fly:
                    PlayAnimation(FLY);
                    OpenTrails();
                    break;
                case PlayerStates.Rotate:
                    PlayAnimation(ROTATE);
                    CloseTrails();
                    break;
                case PlayerStates.Jump:
                    PlayAnimation(THROW);
                    break;
                case PlayerStates.Dead:
                    PlayAnimation(THROW);
                    Invoke(nameof(SetAnimDisable),1);
                    break;
            }
        }

        #region PlayAnimation

        private void PlayAnimation(string AnimtaionID)
        {
            PlayerAnimator.SetTrigger(AnimtaionID);
        }

        #endregion
        
        #region SetAnimEnable/Disable

        private void SetAnimEnable()
        {
            PlayerAnimator.enabled = true;
        }
        private void SetAnimDisable()
        {
            PlayerAnimator.enabled = false;
        }

        #endregion
        
        #region Trails

        private void OpenTrails()
        {
            LTR.time = RTR.time = 1;
        }

        private void CloseTrails()
        {
            LTR.time = RTR.time = 0;
        }

        #endregion

        #region Listeners

        protected override void AddListeners()
        {
            PlayerController.OnPlayerStateChanged += OnPlayerStateChanged;
        }

        protected override void RemoveListeners()
        {
            PlayerController.OnPlayerStateChanged -= OnPlayerStateChanged;
        }

        #endregion
    }
}