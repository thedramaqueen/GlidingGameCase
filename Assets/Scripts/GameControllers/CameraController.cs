using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;

public class CameraController : ControllerBase
{
    private PositionConstraint positionConstraint;

    public Vector3 FollowRotation;

    private void Start()
    {
        positionConstraint = GetComponent<PositionConstraint>();
    }

    private void OnPlayerStateChanged(PlayerStates state)
    {
        if (state == PlayerStates.Throw)
        {
            transform.DORotate(FollowRotation, 1);
            Vector3 value = Vector3.zero;
            positionConstraint.enabled = true;
            DOTween.To(() => value, x => value = x, new Vector3(0, 20, -70), .5f).OnUpdate(() =>
            {
                positionConstraint.translationOffset = value;
            });
           
        }

        if (state == PlayerStates.Dead)
        {
            positionConstraint.enabled = false;
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