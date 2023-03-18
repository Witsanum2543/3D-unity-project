using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public enum CropState {
        None,
        Seed,
        Seedling,
        Harvestable
    }

    public CropState currentCropState = CropState.None;

    // SeedData
    private GameObject seedObject;
    private GameObject seedlingObject;
    private GameObject harvestableObject;
    private GameObject productObject;

    // Crop Mesh Filter and Mesh Renderer
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    float growthPoint = 0;
    float maxGrowthPoint;

    private void Start() {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    public bool initializeSeed(SeedData seedData) {

        if (currentCropState == CropState.None) {

            currentCropState = CropState.Seed;
            seedObject = seedData.seed;
            seedlingObject = seedData.seedling;
            harvestableObject = seedData.harvestable;
            productObject = seedData.product;
            maxGrowthPoint = seedData.maxGrowthPoint;

            // Renderer Mesh
            meshFilter.sharedMesh = seedObject.GetComponentInChildren<MeshFilter>().sharedMesh;
            meshRenderer.sharedMaterial = seedObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;

            // Successfully plant the seed return true
            return true;
        }
        // Their are a plant already cropped here, cant seed
        return false;
        
    }

    public void Growing()
    {
        growthPoint++;
        if (calculateGrowPercentage() >= 50 && currentCropState == CropState.Seed)
        {
            GrowUp();
        }
        if (calculateGrowPercentage() >= 100 && currentCropState == CropState.Seedling)
        {
            GrowUp();
        }
    }

    public void GrowUp() {
        // Seed --> Seedling
        if (currentCropState == CropState.Seed) {
            currentCropState = CropState.Seedling;
            meshFilter.sharedMesh = seedlingObject.GetComponentInChildren<MeshFilter>().sharedMesh;
            meshRenderer.sharedMaterial = seedlingObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;
            
        }
        // Seedling --> Harvestable
        else if (currentCropState == CropState.Seedling) {
            currentCropState = CropState.Harvestable;
            meshFilter.sharedMesh = harvestableObject.GetComponentInChildren<MeshFilter>().sharedMesh;
            meshRenderer.sharedMaterial = harvestableObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;
        }
    }

    public GameObject Harvesting() {
        if (currentCropState == CropState.Harvestable) {
            GameObject product = Instantiate(productObject, transform);
            resetCrop();
            return product;
        }
        return null;
    }

    public void resetCrop() {
        seedObject = null;
        seedlingObject = null;
        harvestableObject = null;
        productObject = null;
        currentCropState = CropState.None;

        meshFilter.mesh = null;
        meshRenderer.material = null;
    }

    private float calculateGrowPercentage()
    {
        return (growthPoint/maxGrowthPoint) * 100;
    }
}
