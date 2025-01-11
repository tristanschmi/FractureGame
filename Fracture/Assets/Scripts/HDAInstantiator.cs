using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDAInstantiator : MonoBehaviour
{
    public GameObject hdaPrefab; // Your HDA prefab
    public Terrain terrain; // The terrain on which to instantiate
    public int instanceCount = 10; // Number of instances to create

    void Start()
    {
        if (hdaPrefab == null || terrain == null)
        {
            Debug.LogError("HDA Prefab or Terrain is not assigned.");
            return;
        }

        for(int i = 0; i < instanceCount; i++)
        {
            InstantiateHDA();
        }
    }

    void InstantiateHDA()
    {
        Vector3 randomPosition = GetRandomPositionOnTerrain(terrain);
        
        // Instantiate the HDA prefab at the calculated position
        GameObject hdaInstance = Instantiate(hdaPrefab, randomPosition, Quaternion.identity);

        // Optionally adjust the instance, e.g., scale or rotation
        // hdaInstance.transform.localScale = new Vector3(1f, 1f, 1f);
        // hdaInstance.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    Vector3 GetRandomPositionOnTerrain(Terrain terrain)
    {
        TerrainData terrainData = terrain.terrainData;

        // Generate random x and z coordinates within terrain bounds
        float x = Random.Range(0, terrainData.size.x);
        float z = Random.Range(0, terrainData.size.z);

        // Compute the y coordinate based on the heightmap
        float y = terrain.SampleHeight(new Vector3(x, 0, z));

        // Return the calculated position
        return new Vector3(x, y, z) + terrain.GetPosition();
    }
}
