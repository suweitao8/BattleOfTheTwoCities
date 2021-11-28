using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMain
{
    public class AlwaysLoadLauncher
    {
        private const string ALWAY_LOAD_LAUNCHER_SCENE = "ALWAY_LOAD_LAUNCHER_SCENE";
        
        [RuntimeInitializeOnLoadMethod]
        private static void AlwayLoadLauncher()
        {
            if (EditorPrefs.GetBool(ALWAY_LOAD_LAUNCHER_SCENE) == false) return;
            if (SceneManager.GetActiveScene().buildIndex == 0) return;
            SceneManager.LoadScene(0);
        }

        [MenuItem("GameMain/Always Load Launcher/Enable", priority = 0)]
        private static void EnableAlwayLoadLauncher()
        {
            EditorPrefs.SetBool(ALWAY_LOAD_LAUNCHER_SCENE, true);
        }
        
        [MenuItem("GameMain/Always Load Launcher/Disable", priority = 1)]
        private static void DisableAlwayLoadLauncher()
        {
            EditorPrefs.SetBool(ALWAY_LOAD_LAUNCHER_SCENE, false);
        }
    }
}