using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonManager : MonoBehaviour
{
    [SerializeField] private string sceneName = "SampleScene";
    public void GameStart()
    {
        SceneManager.LoadScene(sceneName);
        
    }
}
