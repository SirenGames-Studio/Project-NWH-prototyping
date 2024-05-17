using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
   public static UI_Manager Instance { get; private set; }

 public bool _isSystemUIVisible;

// Tab Section
    public GameObject OptionsMenu;
    public GameObject CreditTab;

    public GameObject m_GameplayUICanvas;


    [Header("All Options Menu")]
    public List<Button> um_allButtons = new List<Button>();
    public List<GameObject> um_allOptionTabs = new List<GameObject>();

   private void Awake()
   {
       Instance = this;
   }


    private void Update() {
        ToggleUIMenu();
    }

    private void ToggleUIMenu()
    {
        if ((OptionsMenu.activeSelf || CreditTab.activeSelf) == true)
        {
            m_GameplayUICanvas.SetActive(true);
        } else
        {
            m_GameplayUICanvas.SetActive(false);
        }
    }

    public void CursorVisibility(bool isVisible)
   {
       _isSystemUIVisible = isVisible;
       Cursor.visible = isVisible;
       Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
   }



}
