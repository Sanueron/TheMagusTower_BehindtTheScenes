using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSecretDoor : UI_Level_One_Manager
{
    public Items rustedKey, silverKey, goldKey;
    [SerializeField] private GameObject messagePanel, message, notEnoughKeysMessage;
    public Collider exitWallCollider;
    Animator doorAnimator;
    private bool openDoor, reloading, unlockedDoorRustedKey, unlockedDoorSilverKey, unlockedDoorGoldKey;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        if (messagePanel.activeSelf)
        {
            messagePanel.SetActive(false);
        }
    }
    private void Update()
    {
        if (unlockedDoorRustedKey)
        {
            if (unlockedDoorSilverKey)
            {
                if (unlockedDoorGoldKey)
                {
                    openDoor = true;
                    message.SetActive(false);
                }
            }
        }
        if (openDoor)
        {
            doorAnimator.SetBool("OpenDoor", true);
            exitWallCollider.isTrigger = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerCheck")
        {
            DeactivateMessages();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PlayerCheck")
        {
            if (!reloading)
            {
                messagePanel.SetActive(true);
                notEnoughKeysMessage.SetActive(false);
                message.SetActive(true);

                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (rustedKey.obtained)
                    {
                        //rustedKey.animator.SetBool("OpenDoor", true);
                        unlockedDoorRustedKey = true;
                    }
                    if (silverKey.obtained)
                    {
                        //silverKey.animator.SetBool("OpenDoor", true);
                        unlockedDoorSilverKey = true;
                    }
                    if (goldKey.obtained)
                    {
                        //goldKey.animator.SetBool("OpenDoor", true);
                        unlockedDoorGoldKey = true;
                    }
                    else
                    {
                        message.SetActive(false);
                        notEnoughKeysMessage.SetActive(true);
                        reloading = true;
                        StartCoroutine("MessageReset", 3f);
                        //Debug.Log("You are missing one or more keys");
                    }
                }
            }

        }
    }

    IEnumerator MessageReset(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        reloading = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCheck")
        {
            notEnoughKeysMessage.SetActive(false);
            message.SetActive(false);
            DeactivateMessages();
            messagePanel.SetActive(false);
        }
    }
}
