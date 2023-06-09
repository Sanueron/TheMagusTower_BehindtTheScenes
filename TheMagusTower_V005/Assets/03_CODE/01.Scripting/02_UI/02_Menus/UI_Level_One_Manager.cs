using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Level_One_Manager : MonoBehaviour
{
    public List<GameObject> textMessages;
    //public GameObject[] messages;

    // Start is called before the first frame update
    void Start()
    {
        DeactivateMessages();
    }

    internal void DeactivateMessages()
    {
        for (int i = 0; i < textMessages.Count; i++)
        {
            textMessages[i].SetActive(false);
        }
    }
}
