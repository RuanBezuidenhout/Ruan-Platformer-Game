using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    //Call LoadEndScene function when box collider collides with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LoadEndScene("EndScene");
        }
    }
    //Load End Scene when box collider collides with player
    public void LoadEndScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
