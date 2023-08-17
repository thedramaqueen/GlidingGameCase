using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIController : ControllerBase
{

    #region Components

    [SerializeField] private Transform endGameUI;
    [SerializeField] private Button restartButton;

    #endregion
    
    private void OnPlayerStateChanged(PlayerStates state)
    {
        if (state == PlayerStates.Dead)
        {
            endGameUI.DOScale(Vector3.one, 1).SetEase(Ease.InCubic);
        }
        
    }

    #region Reset

    private void ResetUI()
    {
        endGameUI.localScale = Vector3.zero;
    }
    
    private void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    #endregion
    
    #region Listeners

    protected override void AddListeners()
    {
        PlayerController.OnPlayerStateChanged += OnPlayerStateChanged;
        GameManager.OnRestartGame += ResetUI;
        restartButton.onClick.AddListener(RestartGame);
    }
    
    protected override void RemoveListeners()
    {
        PlayerController.OnPlayerStateChanged -= OnPlayerStateChanged;
        GameManager.OnRestartGame -= ResetUI;
        restartButton.onClick.RemoveListener(RestartGame);
    }

    #endregion
    
}
