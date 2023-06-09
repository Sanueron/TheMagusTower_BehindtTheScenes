using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartAndMainScreen : MainScreens_Audio
{
    public List<GameObject> screens;
    public GameObject startButton, mainScreen, settingsScreen, spellsScreen, notAvailableMessage;
    public TMP_Dropdown dropdown;
    [SerializeField]
    private int floorSelected;


    // Start is called before the first frame update
    void Start()
    {
        screens = new List<GameObject>();
        screens.Add(startButton);
        screens.Add(mainScreen);
        screens.Add(settingsScreen);
        screens.Add(spellsScreen);

        screens[1].SetActive(false);
        screens[2].SetActive(false);
        screens[3].SetActive(false);

        notAvailableMessage.SetActive(false);

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    #region Start and Exit Game
    public void StartGame()
    {
        clicsMusic.Play();
        screens[0].SetActive(false);
        screens[1].SetActive(true);
    }
    public void ExitGame()
    {
        clicsMusic.Play();
        screens[0].SetActive(true);
        screens[1].SetActive(false);
    }
    #endregion

    #region Select and Start Level
    public void SelectFloor()
    {
        floorSelected = dropdown.value;
        clicsMusic.Play();
    }
    public void StartLevel()
    {
        clicsMusic.Play();
        //SceneManager.LoadScene(floorSelected, LoadSceneMode.Single);
        if (SceneManager.sceneCountInBuildSettings > floorSelected)
        {
            SceneManager.LoadScene(floorSelected, LoadSceneMode.Single);
        }
        else
        {
            notAvailableMessage.SetActive(true);
            StartCoroutine("DissappearMessage", notAvailableMessage);
            Debug.Log("Floor not available");
        }
    }
    #endregion

    public void OpenSettings()
    {
        clicsMusic.Play();
        screens[1].SetActive(false);
        screens[2].SetActive(true);
    }
    public void CloseSettings()
    {
        clicsMusic.Play();
        screens[1].SetActive(true);
        screens[2].SetActive(false);
    }
    public void OpenSpellsScreen()
    {
        clicsMusic.Play();
        screens[1].SetActive(false);
        screens[3].SetActive(true);
    }
    public void CloseSpellsScreen()
    {
        screens[1].SetActive(true);
        screens[3].SetActive(false);
    }
    public void DisplayNotAvailablemessage()
    {
        clicsMusic.Play();
        notAvailableMessage.SetActive(true);
        StartCoroutine("DissappearMessage", notAvailableMessage);
    }
    IEnumerator DissappearMessage(GameObject message)
    {
        float time = 2;
        yield return new WaitForSeconds(time);
        message.SetActive(false);
    }
}
