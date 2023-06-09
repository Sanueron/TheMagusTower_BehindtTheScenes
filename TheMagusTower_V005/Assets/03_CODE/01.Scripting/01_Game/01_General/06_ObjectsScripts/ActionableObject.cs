using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionableObject : MonoBehaviour
{
    [SerializeField]
    private MovingPlatforms movingPlatforms;

    private Animator _animator;

    [SerializeField] private GameObject messagePanel, messageText;

    // Bool to start action over activation
    internal bool activate, aObjectActioned;

    // Enum to establish type of Actionable object
    private enum TypeOfActionable { PlatformsLever, Button};
    [SerializeField] TypeOfActionable objectType;

    private void Update()
    {
        if (activate)
        {
            Activation();
        }
    }
    private void Activation()
    {
        switch (objectType)
        {
            case TypeOfActionable.PlatformsLever:
                _animator = GetComponent<Animator>();
                _animator.SetBool("Active", true);
                aObjectActioned = true;
                movingPlatforms.move = true;
                break;

            case TypeOfActionable.Button:
                break;
        }
    }
    internal void Reset()
    {
        activate = false;
        if (aObjectActioned)
        {
            _animator.SetBool("Active", false);
            aObjectActioned = false;
        }
    }
    #region Trigger Detection
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PlayerCheck")
        {
            if (!activate)
            {
                messagePanel.SetActive(true);
                messageText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.C))
                {
                    activate = true;
                    messagePanel.SetActive(false);
                    messageText.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCheck")
        {
            messagePanel.SetActive(false);
            messageText.SetActive(false);
        }
    }
    #endregion
}
