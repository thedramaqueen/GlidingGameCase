using System;
using UnityEngine;

public enum SlingStates
{
    Start,
    Update,
    Throw,
}

public class SlingController : ControllerBase
{
    #region Fields

    [SerializeField] private float slingPower;
    private Vector3 slingVector3 => (Vector3.up * 10) +(Vector3.forward * slingPower);

    public static Action<Vector3> OnSlingThrowed;

    private SlingStates _slingState;

    #endregion

    #region Componets

    [SerializeField] private Animator stickAnimator;

    #endregion


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_slingState != SlingStates.Start)
            {
                return;
            }

            _slingState = SlingStates.Update;
            
        }

        if (Input.GetMouseButton(0))
        {
            if (_slingState != SlingStates.Update)
                return;
            CalculateStickAnimValue();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_slingState != SlingStates.Update)
                return;
            _slingState = SlingStates.Throw;
            ThrowPlayer();
        }
    }

    private void CalculateStickAnimValue()
    {
        slingPower = Mathf.Clamp((SwipeController.swipeValue ), 0, 1);
        stickAnimator.Play("Bend", 0, slingPower);
    }

    public void Throw()
    {
        PlayerController.Instance.ChangePlayerState(PlayerStates.Throw);
        OnSlingThrowed?.Invoke(slingVector3);
        this.enabled = false;
    }

    private void ThrowPlayer()
    {
        stickAnimator.SetTrigger("Release");
    }


    protected override void AddListeners()
    {
    }

    protected override void RemoveListeners()
    {
    }
}