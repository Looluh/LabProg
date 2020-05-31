using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Recomecar : MonoBehaviour
{
    public void Recomeco()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

