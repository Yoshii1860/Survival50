using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] GameObject bullets;
    [SerializeField] GameObject shells;
    [SerializeField] GameObject rifleBullets;
    [SerializeField] GameObject sniperBullets;
    [SerializeField] GameObject mediKit;
    public LayerMask terrainLayer;
    public float interval = 20;
    public float minTime = 10;
    private float nextTime;
    private GameObject prefab;

    void Start()
    {
        StartCoroutine(SpawnPickups());
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(170f, 230f);
        float z = Random.Range(50f, 90f);

        return new Vector3(x, 0f, z);
    }

    private float GetTerrainHeight(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * 100f, Vector3.down, out hit, 200f, terrainLayer))
        {
            return hit.point.y;
        }
        return 0f;
    }
    
    private IEnumerator SpawnPickups()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            int index = Random.Range(0, 100);
            if (index < 40) {
                prefab = bullets;
            }
            else if (index >= 40 && index < 65)
            {
                prefab = rifleBullets;
            }
            else if (index >= 65 && index < 85)
            {
                prefab = shells;
            }
            else if (index >= 85)
            {
                prefab = sniperBullets;
            }
            Vector3 randomPosition = GetRandomPosition();
            float terrainHeight = GetTerrainHeight(randomPosition);
            while (terrainHeight > 3f)
            {
                randomPosition = GetRandomPosition();
                terrainHeight = GetTerrainHeight(randomPosition);
            }
            Vector3 position = new Vector3(randomPosition.x, terrainHeight, randomPosition.z);
            Instantiate(prefab, position, Quaternion.identity);

            index = Random.Range(0, 100);
            if (index <= 20)
            {
                prefab = mediKit;
                position = SpawnMediKit();
                Instantiate(prefab, position, Quaternion.identity);
            }

            if (interval > minTime)
            {
                interval--;
            }
        }
    }

    Vector3 SpawnMediKit()
    {
        Vector3 randomPosition = GetRandomPosition();
        float terrainHeight = GetTerrainHeight(randomPosition);
        while (terrainHeight > 3f)
        {
            randomPosition = GetRandomPosition();
            terrainHeight = GetTerrainHeight(randomPosition);
        }
        return new Vector3(randomPosition.x, terrainHeight, randomPosition.z);
    }

}

