using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Update()
    {
        //escÉLÅ[Ç≈íÜíf
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isActive = pauseMenu.activeSelf;
            pauseMenu.SetActive(!isActive);
            Time.timeScale = isActive ? 1 : 0;
        }
    }

    public void OnResumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackTitle()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
    }
}
