using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonManager : MonoBehaviour
{
    [SerializeField] private string sceneName = "NextScene";
    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName);
    }
}
