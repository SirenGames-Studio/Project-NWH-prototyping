using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public MainMenu MainMenu;
    public PauseMenu PauseMenu;
    public OptionsMenu OptionsMenu;
    public GameObject CreditTab;

   // [HideInInspector] public OptionsMenu OptionsMenu;



    [HideInInspector] public GameObject NewGameType;

    [SerializeField] private Camera _dummyCamera;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

       // _mainMenu.OnFadeComplete.AddListener(HandleMainMenuFadeComplete);

        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.PREGAME)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space pressed");
            }
        }
    }

    private void HandleMainMenuFadeComplete(bool fadeIn)
    {
        // pass it on
        OnMainMenuFadeComplete.Invoke(fadeIn);
    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        switch (currentState)
        {
            case GameManager.GameState.PAUSED:
                PauseMenu.gameObject.SetActive(true);
                break;

            default:
                PauseMenu.gameObject.SetActive(false);
                break;
        }
    }

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }
}
