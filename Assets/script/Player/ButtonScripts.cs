using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    private GameObject winScreen;
    private void Start()
    {
        winScreen = GameObject.FindGameObjectWithTag("WinScreen");
    }
    #region Load Scenes
    public void LoadSceneMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void LoadScene1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void LoadScene2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void LoadScene3()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void LoadScene4()
    {
        SceneManager.LoadScene("Level 4 New");
    }
    public void LoadScene5()
    {
        SceneManager.LoadScene("Level 5 New");
    }
    #endregion
    public void NextLevel()
    {
        winScreen.SetActive(false);
        try
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.Log("Scene index out of bounds");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
