using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TriInspector;
using UnityEngine.Events;
using UnityEngine.UI;
using static MathProblems;

namespace Managers
{
    [HideMonoScript]
    public class UIManager : MonoBehaviour
    {

        private enum Page
        {
            None = 0,
            Math = 1,
            Language = 2,
            Other = 3
        }
        
        [Scene] [SerializeField] private string startScene;
        
        [Header("Screen Darkener")]
        [SerializeField] private GameObject darkBG;
        
        [Header("Assignment Objects")]
        [SerializeField] private GameObject assignment_m;
        [SerializeField] private GameObject assignment_l;
        [SerializeField] private GameObject arrow_r;
        [SerializeField] private GameObject arrow_l;
        [SerializeField] private GameObject assignment_button;
        [SerializeField] private GameObject submit_button;
        
        // Scenes Initialization
        [Scene] private const string frontScene = "Assets/Scenes/MainGame/FrontView.unity";
        [Scene] private const string windowScene = "Assets/Scenes/MainGame/WindowView.unity";
        [Scene] private const string bookScene = "Assets/Scenes/MainGame/BookView.unity";
        [Scene] private const string backScene = "Assets/Scenes/MainGame/BackView.unity";

        // Runtime Variables
        private string currentScene = "";
        private Page currentPage;
        
        public UnityEvent closeUI;
        
        private void Start()
        {
            ChangeScene(startScene);
            currentPage = Page.None;
        }

        // ------------------------------
        //        Helper Methods
        // ------------------------------
        
        private async void ChangeScene(string scene)
        {
            try
            {
                if (scene.Equals(""))
                {
                    await SceneManager.UnloadSceneAsync(currentScene);
                }
                else if (currentScene.Equals(""))
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
            catch (Exception) { print("Error Loading Scene " + scene); }
        }

        private void DarkenScreen(bool state) { darkBG.SetActive(state); }

        public void CloseOverlay()
        {
            DarkenScreen(false);
            ToggleAssignment();
            closeUI.Invoke();
        }

        public void OnOpenUI()
        {
            DarkenScreen(true);
            currentPage = Page.Other;
        }
        
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

        public void ToggleAssignment()
        {
            switch (currentPage)
            {
                case Page.Other:
                    currentPage = Page.None;
                    break;
                case Page.None:
                    assignment_m.SetActive(true);
                    arrow_r.SetActive(true);
                    submit_button.SetActive(true);
                    currentPage = Page.Math;
                    DarkenScreen(true);
                    break;
                default:
                    assignment_m.SetActive(false);
                    assignment_l.SetActive(false);
                    arrow_r.SetActive(false);
                    arrow_l.SetActive(false);
                    submit_button.SetActive(false);
                    currentPage = Page.None;
                    DarkenScreen(false);
                    break;
            }
        }

        public void TurnPage()
        {
            if (currentPage == Page.Math)
            {
                assignment_m.SetActive(false);
                assignment_l.SetActive(true);
                arrow_r.SetActive(false);
                arrow_l.SetActive(true);
                currentPage = Page.Language;
            }
            else
            {
                assignment_m.SetActive(true);
                assignment_l.SetActive(false);
                arrow_r.SetActive(true);
                arrow_l.SetActive(false);
                currentPage = Page.Math;
            }
        }
        
        // ----------------------
        //      Submission
        // ----------------------

        public void Submit()
        {
            CloseOverlay();
            arrow_l.GetComponent<Button>().enabled = false;
            arrow_r.GetComponent<Button>().enabled = false;
            assignment_button.GetComponent<Button>().enabled = false;
            ChangeScene("");
        }
        
    }
}
