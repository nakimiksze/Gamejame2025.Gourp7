using UnityEngine;

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

    /*public void BackTitle()
    {
        SeanManager.Load("Sean");
    }*/
}
