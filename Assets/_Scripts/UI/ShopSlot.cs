using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    ItemData itemData;

    public Image itemDisplayImage;
    public TextMeshProUGUI itemPriceText;
    public TextMeshProUGUI itemAmountText;
    public int amountBuy = 0;

    private GameObject addAmountButton;
    private GameObject subtractAmountButton;


    public void Display(ItemData item)
    {
        itemData = item;
        if (itemData != null)
        {
            itemDisplayImage.sprite = itemData.itemIcon;
            itemPriceText.text = "$ " + itemData.price.ToString();
            itemAmountText.text = amountBuy.ToString();
            itemDisplayImage.gameObject.SetActive(true);
            itemPriceText.gameObject.SetActive(true);
            itemAmountText.gameObject.SetActive(true);
            return;
        }

        itemDisplayImage.gameObject.SetActive(false);
        itemPriceText.gameObject.SetActive(false);
        itemAmountText.gameObject.SetActive(false);

    }

    public void addAmount()
    {
        AudioManager.Instance.PlaySound("add_amount_buy");
        amountBuy++;
        itemAmountText.text = amountBuy.ToString();
        ShopManager.Instance.addItem(itemData);
    }

    public void subtractAmount()
    {
        AudioManager.Instance.PlaySound("subtract_amount_buy");
        if (amountBuy - 1 < 0){
            return;
        }
        amountBuy--;
        itemAmountText.text = amountBuy.ToString();
        ShopManager.Instance.substractItem(itemData);
    }

    public void resetAmount()
    {
        amountBuy = 0;
    }

}
