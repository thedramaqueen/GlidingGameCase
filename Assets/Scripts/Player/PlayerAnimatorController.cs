using UnityEngine;

namespace Player
{
    public class PlayerAnimatorController : ControllerBase
    {
        private const string FLY = "FLY";
        private const string ROTATE = "ROTATE";
        private const string THROW = "THROW";
        public Animator PlayerAnimator;
        
        private void OnPlayerStateChanged(PlayerStates state)
        {
            switch (state)
            {
                case PlayerStates.Throw:
                    PlayAnimation(THROW);
                    break;
                case PlayerStates.Fly:
                PlayAnimation(FLY);  
                    break;
                case PlayerStates.Rotate:
                    PlayAnimation(ROTATE);
                    break;
                case PlayerStates.Dead:
                    PlayerAnimator.enabled = false;
                    break;
            }
        }

        private void PlayAnimation(string AnimtaionID)
        {
            PlayerAnimator.SetTrigger(AnimtaionID);
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
}