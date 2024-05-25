using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using static GameManager;

public class GameManager : Singleton<GameManager> 
{
     public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    public enum GameType
    {
        SURVIVAL,
        ADVENTURE
    }

    public GameObject[] SystemPrefabs;
    public EventGameState OnGameStateChanged;

    List<GameObject> _instancedSystemPrefabs;
    GameState _currentGameState = GameState.PREGAME; 

    string _currentLevelName;

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        private set { _currentGameState = value; }
    }


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        _instancedSystemPrefabs = new List<GameObject>();

        //UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);

        OnGameStateChanged.Invoke(GameState.PREGAME, _currentGameState);
    }

    void Update()
    {
        if (_currentGameState == GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
           // TogglePause();
        }
    }

     void UpdateState(GameState state)
    {
        GameState previousGameState = _currentGameState;
        _currentGameState = state;

        switch (CurrentGameState)
        {
            case GameState.PREGAME:
                // Initialize any systems that need to be reset
                Time.timeScale = 1.0f;
                break;

            case GameState.RUNNING:
                //  Unlock player, enemies and input in other systems, update tick if you are managing time
                Time.timeScale = 1.0f;
                break;

            case GameState.PAUSED:
                // Pause player, enemies etc, Lock other input in other systems
                Time.timeScale = 0.0f;
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    public void ChangeValueToState(int value)
    {
        switch (value)
        {
            case 0:
                UpdateState(GameState.PREGAME);
                break;
            case 1:
                UpdateState(GameState.RUNNING);
                break;
            case 2:
                UpdateState(GameState.PAUSED);
                break;
            default:
                break;
        }
    }

    
}

[System.Serializable] public class EventGameState : UnityEvent<GameState, GameState> { }
