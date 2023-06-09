using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    [SerializeField]
    internal GameObject menu, audioPanel, inventoryPanel;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        audioPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        player.speed = 0;

    }
    public void CloseMenu()
    {
        menu.SetActive(false);
        player.speed = 20;
    }
    public void OpenAudioPanel()
    {
        audioPanel.SetActive(true);
    }
    public void CloseAudioPanel()
    {
        audioPanel.SetActive(false);
    }
    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        player.speed = 0;
    }
    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        player.speed = 20;

    }
    public void BackToMainScreen()
    {
        SceneManager.LoadScene(0);
    }
}
