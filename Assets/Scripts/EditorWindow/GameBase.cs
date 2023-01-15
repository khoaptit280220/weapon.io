#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
public class GameBase : EditorWindow
{
    void OnGUI()
    {
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.EndHorizontal();
    }
    
    
    [MenuItem("GameBase/Open Scene/Splash Scene %F1")]
    public static void PlayFromSplashScene(){
        EditorSceneManager.OpenScene($"Assets/Scenes/{GameConst.SPLASH}.unity");
        Debug.Log($"<color=Green>Change scene succeed</color>");
    }

    [MenuItem("GameBase/Open Scene/Gameplay Scene %F2")]
    public static void PlayFromGamePlayScene(){
        EditorSceneManager.OpenScene($"Assets/Scenes/{GameConst.GAMEPLAY}.unity");
        Debug.Log($"<color=Green>Change scene succeed</color>");
    } 
    
    [MenuItem("GameBase/Data/Clear Data %F3")]
    public static void ClearAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log($"<color=Green>Clear data succeed</color>");
    }
    
}
#endif