using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour 
{
    private List<AsyncOperation> _loadOperations;
    private string _currentLevelName;

    protected int callbackvalue = 0;

    private void Start() 
    {
        _loadOperations = new List<AsyncOperation>();
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);

            if (_loadOperations.Count == 0)
            {
                // GameManager.Instance.UpdateState(GameState.RUNNING);
                GameManager.Instance.ChangeValueToState(1);
            }
        }
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        // Clean up level is necessary, go back to main menu
    }

   

    void HandleMainMenuFadeComplete(bool fadeIn)
    {
        if (!fadeIn)
        {
            UnloadLevel(_currentLevelName);
        }
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);

        _currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        ao.completed += OnUnloadOperationComplete;
    }

}