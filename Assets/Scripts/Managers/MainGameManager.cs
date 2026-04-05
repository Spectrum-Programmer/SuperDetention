using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TriInspector;

namespace Managers
{
    [HideMonoScript]
    public class MainGameManager : MonoBehaviour
    {

        [Scene] [SerializeField] private string startScene;

        [SerializeField] private GameObject assignment;
        
        [Scene] private const string frontScene = "Assets/Scenes/MainGame/FrontView.unity";
        [Scene] private const string windowScene = "Assets/Scenes/MainGame/WindowView.unity";
        [Scene] private const string bookScene = "Assets/Scenes/MainGame/BookView.unity";
        [Scene] private const string backScene = "Assets/Scenes/MainGame/BackView.unity";

        private string currentScene = "";

        private void Start() { ChangeScene(startScene); }

        private async void ChangeScene(string scene)
        {
            try
            {
                if (currentScene.Equals(""))
                {
                    await SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                    SceneManager.SetActiveScene(SceneManager.GetSceneByPath(scene));
                }
                else
                {
                    await SceneManager.UnloadSceneAsync(currentScene);
                    await SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                }
                currentScene = scene;
            }
            catch (Exception _) { print("Error Loading Scene " + scene); }
        }

        // ------------------------------
        //          Game Set-Up
        // ------------------------------
        
        
        
        // ------------------------------
        //      Navigation Controls
        // ------------------------------
        
        public void LeftScene()
        {
            switch (currentScene)
            {
                case frontScene:
                    ChangeScene(windowScene); break;
                case windowScene:
                    ChangeScene(backScene); break;
                case backScene:
                    ChangeScene(bookScene); break;
                case bookScene:
                    ChangeScene(frontScene); break;
            }
        }
        
        public void RightScene()
        {
            switch (currentScene)
            {
                case frontScene:
                    ChangeScene(bookScene); break;
                case bookScene:
                    ChangeScene(backScene); break;
                case backScene:
                    ChangeScene(windowScene); break;
                case windowScene:
                    ChangeScene(frontScene); break;
            }
        }

        public void ToggleAssignment() { assignment.SetActive(!assignment.activeSelf); }
    }
}
