using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;

public class CameraController : ControllerBase
{
    #region Components

    private PositionConstraint positionConstraint;

    #endregion

    #region Fields

    private Vector3 StartPosition;
    private Vector3 StartOffset;
    private Vector3 StartRotation;
    public Vector3 FollowRotation;

    #endregion
    
    private void Start()
    {
        positionConstraint = GetComponent<PositionConstraint>();
        StartPosition = transform.position;
        StartOffset = positionConstraint.translationOffset;
        StartRotation = transform.eulerAngles;
    }

    #region Reset

    private void ResetCamera()
    {
        positionConstraint.translationOffset = StartOffset;
        transform.position = StartPosition;
        transform.eulerAngles = StartRotation;
    }

    #endregion

    private void OnPlayerStateChanged(PlayerStates state)
    {
        if (state == PlayerStates.Throw)
        {
            transform.DORotate(FollowRotation, 1f).SetEase(Ease.Linear);
            Vector3 value = positionConstraint.translationOffset;
            positionConstraint.enabled = true;
            DOTween.To(() => value, x => value = x, new Vector3(0, 20, -70), 1.5f).SetEase(Ease.Linear).OnUpdate(() =>
            {
                positionConstraint.translationOffset = value;
            });
           
        }

        if (state == PlayerStates.Dead)
        {
            positionConstraint.enabled = false;
        }
    }

    #region Listeners

    protected override void AddListeners()
    {
        PlayerController.OnPlayerStateChanged += OnPlayerStateChanged;
        GameManager.OnRestartGame += ResetCamera;
    }


    protected override void RemoveListeners()
    {
        PlayerController.OnPlayerStateChanged -= OnPlayerStateChanged;
        GameManager.OnRestartGame -= ResetCamera;
    }

    #endregion
    
}