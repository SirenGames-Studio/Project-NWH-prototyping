using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
     public static SceneLoader Instance { get; private set; }

   List<AsyncOperation> sceneToload = new List<AsyncOperation>();

   private void Awake()
   {
       if (Instance != null)
       {
           Destroy(gameObject);
       } else {
           Instance = this;
           DontDestroyOnLoad(gameObject);
       }
   }

   public void GameStart()
   {
        
   }

   public void LoadNewGame()
   {
        sceneToload.Add(SceneManager.LoadSceneAsync("GamePlayInstance"));
        sceneToload.Add(SceneManager.LoadSceneAsync("Level_1"));
   }
}


