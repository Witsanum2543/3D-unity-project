using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    ItemData itemData;

    public Image itemDisplayImage;
    public TextMeshProUGUI itemPrice;

    public void Display(ItemData item)
    {
        itemData = item;
        if (itemData != null)
        {
            itemDisplayImage.sprite = itemData.itemIcon;
            itemPrice.text = "$ " + itemData.price.ToString();
            itemDisplayImage.gameObject.SetActive(true);
            itemPrice.gameObject.SetActive(true);
            return;
        }

        itemDisplayImage.gameObject.SetActive(false);
        itemPrice.gameObject.SetActive(false);
    }

}
