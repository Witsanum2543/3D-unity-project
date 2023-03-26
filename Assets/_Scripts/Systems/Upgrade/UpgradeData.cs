using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Upgrade")]
public class UpgradeData : ScriptableObject
{
    public EUpgradeName upgradeName;
    public bool isUpgrade;
    public float ORIGINAL_SCALE_VALUE;
    public float scale;
    public int price;

    private void Start() {
        scale = 0;
    }

    public void upgrading()
    {
        isUpgrade = true;
        scale = ORIGINAL_SCALE_VALUE;
    }

    public void reset()
    {
        isUpgrade = false;
        scale = 0;
    }
}
