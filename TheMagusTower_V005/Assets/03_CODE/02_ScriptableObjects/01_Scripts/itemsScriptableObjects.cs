using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "ScriptableObjeccts/ items / StandardItem")]
public class itemsScriptableObjects : ScriptableObject
{
    [SerializeField] internal int iD;
    [SerializeField] internal string itemName, description, skill;
    [SerializeField] internal bool misionItem;
    internal enum Type { Accesory, Weapon, CollectableItem };
    [SerializeField] internal Type type;
    internal enum Rarity { Common, Rare, VeryRare, UltraRare, Legendary }
    [SerializeField] internal Rarity rarity;
    internal enum State { Passive, Active };
    [SerializeField] internal State state;
    internal enum Effect { None, HealingHP, HealingMP, IncreaseAttack };
    [SerializeField] internal Effect effect;

    [SerializeField] internal Sprite itemIcon;
}
