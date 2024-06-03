using System;
using UnityEngine;
using UnityEngine.UI;

public class PausedStateMenu : MonoBehaviour 
{

    public GameObject PausedTab;
    public GameObject InventoryTab;
    public GameObject JournalTab;

    #region PauseMenuBehaviour
    // paused Buttons
    [SerializeField] private Button _resume;
    //[SerializeField] private Button _reloadCheckpoint;
    [SerializeField] private Button _options;
    [SerializeField] private Button _credits;
    [SerializeField] private Button _quitToMainmenu;
    [SerializeField] private Button _quittoDesktop;
    [SerializeField] private Button _backButton;

    [SerializeField] private GameObject _optionsTab;
    [SerializeField] private GameObject _creditsTab;
    [SerializeField] private GameObject _MainPausedMenu;

    [SerializeField] private GameObject _mainMenuTab;


    private void Start()
    {
        _resume.onClick.AddListener(ResumeButtonClicked);
        _options.onClick.AddListener(OptionsButtonClicked);
        _credits.onClick.AddListener(CreditsTabClicked);
        _quitToMainmenu.onClick.AddListener(QuitToMainMenu);
        _quittoDesktop.onClick.AddListener(QuitToDesktop);

        //back behaviour
        _backButton.onClick.AddListener(OnBackButtonClicked);

    }

    private void ResumeButtonClicked()
    {
        _MainPausedMenu.SetActive(false);
        UIManager.Instance.OnResumeStateChange.Invoke(true);
       
    }
    private void OptionsButtonClicked()
    {
       _optionsTab.SetActive(true);
    }

    private void CreditsTabClicked()
    {
        _creditsTab.SetActive(true);
    }
    private void QuitToMainMenu()
    {
        _mainMenuTab.SetActive(true);
        UIManager.Instance.OnClickToMainMenu.Invoke(true);
    }

    private void QuitToDesktop()
    {
        Application.Quit();
    }


    private void OnBackButtonClicked()
    {
       _creditsTab.SetActive(false);
    }



    // paused Behavior

    #endregion


    private void Update()
    {
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PREGAME) { return; }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePausedState();
            UpdatePausedStateCanvas();
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            HandleInventoryState();
            UpdatePausedStateCanvas();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            HandleJournalState();
            UpdatePausedStateCanvas();
        }

        
    }

    private void HandleJournalState()
    {
        JournalTab.SetActive(!JournalTab.activeSelf);
        if (JournalTab.activeSelf)
        {
            InventoryTab.SetActive(false);
            PausedTab.SetActive(false);
        }
    }

    private void HandleInventoryState()
    {
        InventoryTab.SetActive(!InventoryTab.activeSelf);
        if (InventoryTab.activeSelf)
        {
            JournalTab.SetActive(false);
            PausedTab.SetActive(false);
        }
    }

    private void HandlePausedState()
    {
        PausedTab.SetActive(!PausedTab.activeSelf);
        if (PausedTab.activeSelf)
        {
            InventoryTab.SetActive(false);
            JournalTab.SetActive(false);
        }
    }

    private void UpdatePausedStateCanvas()
    {
        bool isAnyActiveTab = JournalTab.activeSelf || InventoryTab.activeSelf || PausedTab.activeSelf;
        // UIManager.Instance.PauseMenu.gameObject.SetActive(isAnyActiveTab);
       //  UIManager.Instance.PausedGameState.SetActive(isAnyActiveTab);
       UIManager.Instance.PausedState(isAnyActiveTab);
    }


}