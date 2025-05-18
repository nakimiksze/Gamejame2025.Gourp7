using UnityEngine;
using UnityEngine.EventSystems;

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Game will quit.");

        // エディタ上で実行されている場合
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // ビルド後のアプリケーションを終了
        Application.Quit();
        #endif
    }
}
