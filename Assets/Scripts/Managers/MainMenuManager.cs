using TriInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    [HideMonoScript]
    public class MainMenuManager : MonoBehaviour
    {
        [Scene] [SerializeField] private string startScene;

        
        
        private static void ChangeScene(string sceneName) { SceneManager.LoadScene(sceneName, LoadSceneMode.Single); }

        public void StartGame() { ChangeScene(startScene); }

        public void QuitGame()
        {
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        
    }
}
