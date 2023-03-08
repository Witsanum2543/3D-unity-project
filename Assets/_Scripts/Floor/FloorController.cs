using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{   
    [Header("Mesh Filter list test")]
    [SerializeField] private string[] strings;

    [Header("Mesh Filter list")]
    [SerializeField] private Mesh[] meshFilers;

    [Header("Floor list")]
    [SerializeField] private GameObject[] floors;

    private void Start() {
        changeAllFloorMesh();
    }

    private void changeAllFloorMesh() {
        for (int i = 0; i < floors.Length; i++)
        {
            MeshFilter originalMesh = floors[i].GetComponentInChildren<MeshFilter>();
            originalMesh.mesh = meshFilers[i];
        }
    }
}
