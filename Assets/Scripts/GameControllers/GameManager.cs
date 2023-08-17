using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Action OnRestartGame;
    
    private void Awake()
    {
        if (Instance is not null)
        {
            Debug.LogError("There is an another GameManager in scene");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RestartGame()
    {
        OnRestartGame?.Invoke();
    }

}
