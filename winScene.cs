using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winScene : MonoBehaviour
{
    //Call LoadWinScene function when box collider collides with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LoadWinScene("winScene");
        }
    }

    //Load Win Scene when box collider collides with player
    public void LoadWinScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
