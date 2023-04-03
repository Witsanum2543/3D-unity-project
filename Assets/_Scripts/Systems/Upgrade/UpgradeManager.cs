using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public Upgrade[] upgradeList;

    public float findScale(EUpgradeName upgradeName)
    {
        foreach(Upgrade upgrade in upgradeList)
        {
            if (upgrade.data.upgradeName == upgradeName)
            {
                return upgrade.data.scale; 
            }
        }
        return 0;
    }

    public void reset()
    {
        foreach(Upgrade upgrade in upgradeList)
        {
            upgrade.reset();
        }
    }

}
