using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
     private List<Button> _allButtons = new List<Button>();
    [SerializeField]
    private List<GameObject> _allOptionTabs = new List<GameObject>();

    private void Start() 
    {
        for (int i = 0; i < _allButtons.Count; i++)
        {
            int index = i;
            _allButtons[index].onClick.AddListener(() => ShowOptionPanels(index));
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
                    Debug.Log(_allOptionTabs[i].name);
                }
                break;
        }
    }

}
