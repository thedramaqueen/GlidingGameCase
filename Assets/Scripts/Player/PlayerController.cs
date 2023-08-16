using System;

using UnityEngine;

public class PlayerController : ControllerBase
{
    public static PlayerController Instance;

    #region Fields

    [Header("Fields")] 
    [SerializeField] private PlayerStates playerState = PlayerStates.Sling;
    public PlayerStates PlayerState => playerState;

    #endregion

    #region Components

    private Rigidbody Rb;

    #endregion

    #region Actions

    public static Action<PlayerStates> OnPlayerStateChanged;

    #endregion

    #region Unity Functions

    private void Awake()
    {
        if (Instance is not null)
        {
            Debug.LogError("There is an another PlayerController in scene");
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        Rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (PlayerState != PlayerStates.Sling)
                ChangePlayerState(PlayerStates.Fly);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (PlayerState != PlayerStates.Sling)
                ChangePlayerState(PlayerStates.Rotate);
        }
    }

    #endregion


    #region Functions

    private void SlingThrowed()
    {
        if (playerState != PlayerStates.Sling)
            return;

        playerState = PlayerStates.Throw;
        OnPlayerStateChanged?.Invoke(playerState);
    }

    private void ChangeToFly()
    {
        if (playerState is PlayerStates.Rotate or PlayerStates.Throw)
        {
            playerState = PlayerStates.Fly;
            OnPlayerStateChanged?.Invoke(playerState);
        }

    }

    private void ChangeToRotate()
    {
        if (playerState != PlayerStates.Fly)
            return;

        playerState = PlayerStates.Rotate;
        OnPlayerStateChanged?.Invoke(playerState);
    }

    private void PlayerIsDead()
    {
        if (playerState == PlayerStates.Dead)
            return;

        playerState = PlayerStates.Dead;
        OnPlayerStateChanged?.Invoke(playerState);
    }

    public void ChangePlayerState(PlayerStates requestedState)
    {
        switch (requestedState)
        {
            case PlayerStates.Dead:
                PlayerIsDead();
                break;
            case PlayerStates.Fly:
                ChangeToFly();
                break;
            case PlayerStates.Rotate:
                ChangeToRotate();
                break;
            case PlayerStates.Throw:
                SlingThrowed();
                break;
        }
    }

    #endregion

    #region Add | Remove Listeners

    protected override void AddListeners()
    {
    }

    protected override void RemoveListeners()
    {
    }

    #endregion
}