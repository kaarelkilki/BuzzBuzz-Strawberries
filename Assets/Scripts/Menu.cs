using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
}
