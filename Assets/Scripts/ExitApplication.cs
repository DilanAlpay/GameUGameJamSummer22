using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitApplication : MonoBehaviour
{
    public void Quit()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
