using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Upgrade")]
public class UpgradeData : ScriptableObject
{
    public EUpgradeName upgradeName;
    public bool isUpgrade;
    public float TAKE_EFFECT_VALUE;
    public float RESET_VALUE;
    public float scale;
    public int price;

    [TextArea]
    public string description;

    private void Start() {
        scale = RESET_VALUE;
    }

    public void upgrading()
    {
        isUpgrade = true;
        scale = TAKE_EFFECT_VALUE;
        Debug.Log(scale);
    }

    public void reset()
    {
        isUpgrade = false;
        scale = RESET_VALUE;
    }
}
