using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject inGameMenu, mainMenu, inGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        inGameMenu.SetActive(false);
        mainMenu.SetActive(true);
        inGameObjects.SetActive(false);
    }

    public void StartTheGame()
    {
        inGameMenu.SetActive(true);
        mainMenu.SetActive(false);
        inGameObjects.SetActive(true);
        mainMenu.GetComponent<AudioSource>().Stop();
    }

    public void ExitFromTheGame()
    {
        Application.Quit();
    }
}
