using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    private Button button;

    [SerializeField]
    private bool _isInteractable;

    private void Awake() 
    {
        button = GetComponent<Button>();

       
    }

    private void Start() 
    {
         button.interactable = _isInteractable;    
    }
}
