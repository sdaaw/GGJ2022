using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartRandom()
    {
        Application.LoadLevel(2);
    }

    public void StartLevel1()
    {
        Application.LoadLevel(1);
    }
}
