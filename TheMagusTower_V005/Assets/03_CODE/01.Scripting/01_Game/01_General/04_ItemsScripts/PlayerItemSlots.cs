using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSlots : MonoBehaviour
{
    // Player equipped items position
    [SerializeField] internal GameObject rightHandAccessoryPos, leftHandAccessoryPos;
    [SerializeField] internal GameObject rightHandWeaponPos, leftHandWeaponPos, backWeaponPos;

    // Bools to check items slots
    [SerializeField] internal bool accessoryEquippedRightHand, accessoryEquippedLeftHand, weaponEquipped;

    private void Update()
    {
        CheckSlots();
    }
    private void CheckSlots()
    {
        if (backWeaponPos.transform.childCount > 0)
        {
            weaponEquipped = false;
        }
        if (rightHandAccessoryPos.transform.childCount > 0)
        {
            accessoryEquippedRightHand = false;
        }
        if (leftHandAccessoryPos.transform.childCount > 0)
        {
            accessoryEquippedLeftHand = false;
        }
    }
}
