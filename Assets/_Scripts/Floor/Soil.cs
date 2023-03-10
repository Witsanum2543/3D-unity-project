using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    public enum SoilState
    {
        Soil, SoilWatered
    }

    [SerializeField] public Mesh soil;
    [SerializeField] public Mesh soilWatered;

    public SoilState soilState;
    private MeshFilter currentMesh;

    // The selection gameobject to enable when the player is selecting the soil
    public GameObject select;

    void Start()
    {
        currentMesh = transform.GetComponentInChildren<MeshFilter>();
        SwitchSoilState(soilState);
    }

    public void SwitchSoilState(SoilState state) {
        soilState = state;

        switch(soilState)
        {
            case SoilState.Soil:
                currentMesh.mesh = soil;
                break;
            case SoilState.SoilWatered:
                currentMesh.mesh = soilWatered;
                break;
        }
    }

    public void Select(bool toggle) {
        select.SetActive(toggle);
    }
}
