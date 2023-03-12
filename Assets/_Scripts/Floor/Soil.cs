using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoilState
{
    SoilDry, SoilWatered
}

public class Soil : MonoBehaviour
{
    [Header ("Soil state")]
    [SerializeField] public Mesh SoilDry;
    [SerializeField] public Mesh soilWatered;
    public SoilState soilState;
    private MeshFilter currentMesh;

    [Header ("Soil Seed")]
    [SerializeField] public Crop crop;

    [Header ("Selection UI")]
    // The selection gameobject to enable when the player is selecting the soil
    public GameObject select;

    void Start()
    {
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
        select.SetActive(toggle);
    }

    public void Watering() {
        SwitchSoilState(SoilState.SoilWatered);
    }

    public void Drying() {
        SwitchSoilState(SoilState.SoilDry);
    }

    public void Seeding(GameObject seedPrefab) {
        crop.initializeSeed(seedPrefab);
    }
}
