using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuUI : MonoBehaviour
{
    [Header("Main Menu")]
    //button
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _newGameBtn;
    [SerializeField] private Button _optionBtn;
    [SerializeField] private Button _creditBtn;
    [SerializeField] private Button _exitBtn;

    [SerializeField] private GameObject _gameType;
    public GameObject mainMenu;
    public GameObject optionsMenu;

    [SerializeField]
    private List<Button> _allButtons = new List<Button>();
    [SerializeField]
    private List<GameObject> _allOptionTabs = new List<GameObject>();


    private  void Start()
    {

        //Button button = _newGameBtn.GetComponent<Button>();
        ConfigureAllOptions();
        for (int i = 0; i < _allButtons.Count; i++)
        {
            int index = i;
            _allButtons[index].onClick.AddListener(() => ShowOptionPanels(index));
            
        }

        ButtonCLickState();
       
    }

    private void ConfigureAllOptions()
    {
       // _allButtons = UIManager.Instance.um_allButtons;
       // _allOptionTabs = UI_Manager.Instance.um_allOptionTabs;
    }

    private void ButtonCLickState()
    {
        _continueBtn.onClick.AddListener(ContinueGameState);
        _newGameBtn.onClick.AddListener(NewGame);
        _optionBtn.onClick.AddListener(OpenOptions);
        _creditBtn.onClick.AddListener(OpenCredit);
        _exitBtn.onClick.AddListener(ExitGame);
       
    }

    private void ContinueGameState()
    {
        //TODO: Continue Game
    }
    private void NewGame()
    {
        AudioManager.Instance.PlayClickSound();
        _gameType.SetActive(true);

    }

    private void OpenCredit()
    {
        AudioManager.Instance.PlayClickSound();
        mainMenu.SetActive(false);
       // UI_Manager.Instance.CreditTab.SetActive(true);
    }


    private void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int nextTabIndex = GetCurrentTabIndex() + 1;
            if(nextTabIndex >= 5)
            {
                nextTabIndex = 0;
                ShowOptionPanels(nextTabIndex);
            }
            ShowOptionPanels(nextTabIndex);

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int nextTabIndex = GetCurrentTabIndex() - 1;
            if (nextTabIndex < 0)
            {
                nextTabIndex = 4;
                ShowOptionPanels(nextTabIndex);
            }
            ShowOptionPanels(nextTabIndex);

        }


    }
    private void ShowOptionPanels(int index)
    {
        switch (index)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:

                for (int i = 0; i < _allOptionTabs.Count; i++)
                {
                    _allOptionTabs[i].SetActive(i == index);

                    AudioManager.Instance.PlayClickSound();
                }
                break;
        }
    }
    private void OpenOptions()
    {
        AudioManager.Instance.PlayClickSound();
        mainMenu.SetActive(false);
      //  UI_Manager.Instance.OptionsMenu.SetActive(true);

    }

    public int GetCurrentTabIndex()
    {
        // Find the index of the active panel
        for (int i = 0; i < _allOptionTabs.Count; i++)
        {
            if (_allOptionTabs[i].activeSelf)
            {
                return i;
            }
        }
        return -1; // No active panel found
    }

}
