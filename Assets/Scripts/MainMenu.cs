using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject newGameButton, optionsButton, exitButton, backButton, volumeText;
    //public Slider volumeSlider;
    public Image slikaZvuka;
    public Sprite soundOn, soundOff;
    private List<GameObject> mainMenuObjects;
    private List<GameObject> optionsObjects;
    [SerializeField] Slider volumeSlider;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("Volume")){
            SetVolume(PlayerPrefs.GetFloat("Volume"));
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        mainMenuObjects = new List<GameObject>(){newGameButton, optionsButton, exitButton};
        optionsObjects = new List<GameObject>() {volumeText, backButton, slikaZvuka.gameObject, volumeSlider.gameObject};
    }

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate {VolumeChange(); });
        PlayerPrefs.SetInt("level",1);
        PlayerPrefs.SetInt("score",0);
        PlayerPrefs.SetInt("life",3);
    }

    void VolumeChange()
    {
        if (volumeSlider.value==0)
        {
            slikaZvuka.sprite = soundOff;
        }
        else
        {
            slikaZvuka.sprite = soundOn;
        }
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
            if (newGameButton == clicked)
            {
                SceneManager.LoadScene("Level1", LoadSceneMode.Single);
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
    

    public void SetVolume(float volume){
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

}
