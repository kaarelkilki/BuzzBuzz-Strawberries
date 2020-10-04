using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }
    public void Scores()
    {
        SceneManager.LoadScene("Scores");
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
    //public void Logo()
    //{
    //    StartCoroutine(WaitForIt(3.0F));

    //    IEnumerator WaitForIt(float waitTime)
    //    {
    //        yield return new WaitForSeconds(waitTime);
    //        SceneManager.LoadScene("StartMenu");
    //    }
    //}
    
}
