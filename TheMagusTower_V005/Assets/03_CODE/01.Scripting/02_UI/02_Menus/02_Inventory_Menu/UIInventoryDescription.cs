using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text title, description, skill;

    public void SetDescriptionWindow(Sprite sprite, string itemName, string itemDescription, string itemSkill)
    {
        itemImage.sprite = sprite;
        title.text = itemName;
        description.text = itemDescription;
        skill.text = itemSkill;
    }
}
