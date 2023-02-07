using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject continueButton, newGameButton, optionsButton, exitButton, backButton, volumeText, volumeSlider;
    private List<GameObject> mainMenuObjects;
    private List<GameObject> optionsObjects;

    private void Awake()
    {
        mainMenuObjects = new List<GameObject>(){continueButton, newGameButton, optionsButton, exitButton};
        optionsObjects = new List<GameObject>() { volumeSlider, volumeText, backButton };
    }

    void PromjeniActive(List<GameObject> objekti, bool setActive)
    {
        foreach (GameObject objekt in objekti)
        {
            objekt.SetActive(setActive);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clicked = EventSystem.current.currentSelectedGameObject;
            // TODO: doraditi da actually nastavlja kada cemo imati vise levela
            if (continueButton == clicked)
            {
                SceneManager.LoadScene("TestLevel", LoadSceneMode.Single);
            }
            else if (newGameButton == clicked)
            {
                SceneManager.LoadScene("TestLevel", LoadSceneMode.Single);
            }
            else if (optionsButton == clicked)
            {
                PromjeniActive(mainMenuObjects,false);
                PromjeniActive(optionsObjects,true);
            }
            else if(exitButton == clicked)
            {
                Application.Quit();
            }
            else if (backButton == clicked)
            {
                PromjeniActive(optionsObjects,false);
                PromjeniActive(mainMenuObjects,true);
            }
        }
    }
}
