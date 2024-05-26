using UnityEngine;
using TMPro;

public class DebugWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI text2;

    private void Update()
    {
        UpdateDebugWindow();
    }
    public void UpdateDebugWindow()
    {
        text.text = GameManager.Instance.CurrentGameState.ToString();
        text2.text = Time.timeScale.ToString();
        

    }

}
