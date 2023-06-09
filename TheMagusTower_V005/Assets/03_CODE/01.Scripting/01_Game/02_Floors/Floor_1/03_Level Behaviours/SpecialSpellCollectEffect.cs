using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialSpellCollectEffect : MonoBehaviour
{
    [SerializeField] PlayerController playerStats;
    [SerializeField] GameObject book, messagePanel, message;
    [SerializeField] Sprite fireballIcon;
    [SerializeField] Image spellUI;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PlayerCheck")
        {
            messagePanel.SetActive(true);
            message.SetActive(true);
            if (Input.GetKeyDown(KeyCode.C))
            {
                spellUI.sprite = fireballIcon;
                playerStats.specialSpellUnlocked = true;
                message.SetActive(false);
                messagePanel.SetActive(false);
                book.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCheck")
        {
            message.SetActive(false);
            messagePanel.SetActive(false);
        }
    }
}
