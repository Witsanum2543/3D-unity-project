using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string itemName;

    public Sprite itemIcon;

    public int price;
    public int weight;

    public GameObject gameModel;
}
