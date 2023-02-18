using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endgame : MonoBehaviour
{
    //Load Next Scene(the parameter determines which scene it will be)
    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Close the game
    public void EndGame()
    {
        Application.Quit();
    }
}
