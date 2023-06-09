using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "PlayerCheck")
        {
            Debug.Log("Floor 2 unlocked");
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
