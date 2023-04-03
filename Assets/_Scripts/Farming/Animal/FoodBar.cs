using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBar : MonoBehaviour
{
    [SerializeField] private Image foodBarSprite;

    public void UpdateFoodBar(float percentage)
    {
        // Scale from 0-100 to 0-1
       foodBarSprite.fillAmount = percentage / 100;
    }

    private void FixedUpdate() {
        transform.rotation = Quaternion.identity;
    }

}
