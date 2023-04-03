using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoilState
{
    SoilDry, SoilWatered
}

public class Soil : MonoBehaviour, ITimeTracker
{
    [Header ("Soil state")]
    [SerializeField] public Mesh SoilDry;
    [SerializeField] public Mesh soilWatered;
    public SoilState soilState;
    public int remainWateredTime = 45;
    private MeshFilter currentMesh;

    [Header ("Soil Seed")]
    [SerializeField] public Crop crop;

    [Header ("Selection UI")]
    // The selection gameobject to enable when the player is selecting the soil
    public GameObject select;

    // Cache the time the land was watered
    GameTimeStamp timeWatered;

    void Start()
    {
        TimeSystem.Instance.RegisterTracker(this);
        currentMesh = transform.GetComponentInChildren<MeshFilter>();
        SwitchSoilState(soilState);

        // Deactivate the soil selection by default
        Select(false);
    }

    public void SwitchSoilState(SoilState state) {
        soilState = state;

        switch(soilState)
        {
            case SoilState.SoilDry:
                currentMesh.mesh = SoilDry;
                break;
            case SoilState.SoilWatered:
                currentMesh.mesh = soilWatered;
                break;
        }
    }

    public void Select(bool toggle) {
        if (toggle == true)
        {
            crop.renderGrowBar();
        } else {
            crop.removeGrowBar();
        }
        select.SetActive(toggle);
    }

    public void Watering() {
        
        SwitchSoilState(SoilState.SoilWatered);
        // Cache time it got watered
        timeWatered = TimeSystem.Instance.GetGameTimeStamp();
        
    }

    public void Drying() {
        SwitchSoilState(SoilState.SoilDry);
    }

    // Return true if succesfully seeding
    // Return false if fail to seeding
    public bool Seeding(SeedData seedData) {
        return crop.initializeSeed(seedData);
    }

    public GameObject TryHarvesting() {
        return crop.Harvesting();
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        // Check if ... second passes since last watered
        if (soilState == SoilState.SoilWatered) {
            int secondElapsed = GameTimeStamp.CompareTimeStamps(timeWatered, timeStamp);
            
            if (secondElapsed >= remainWateredTime * UpgradeManager.Instance.findScale(EUpgradeName.DOUBLE_SOIL_WATERED_TIME))
            {
                // Dry the soil
                SwitchSoilState(SoilState.SoilDry);
            }

            if (crop.currentCropState != Crop.CropState.None)
            {
                crop.Growing();
            }
        }
    }
}

