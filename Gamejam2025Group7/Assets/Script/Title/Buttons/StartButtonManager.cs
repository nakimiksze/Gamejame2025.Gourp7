using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonManager : MonoBehaviour
{
    [SerializeField] private string sceneName = "Scene";
    public void GameStart()
    {
        SceneManager.LoadScene("Sene");
    }
}
