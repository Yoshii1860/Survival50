using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject zombie;
    [SerializeField] GameObject spawner;
    public LayerMask terrainLayer;
    public float startTime = 20; 
    public float interval = 20;
    public float minTime = 10;
    private float nextTime;
    int i = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
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

    IEnumerator SpawnZombies(Vector3 position)
    {
        Instantiate(zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(3);
        if (i > 0)
        {
            for (int j = 0; j < i; j++)
            {
                Vector3 randomPosition = GetRandomPosition();
                float terrainHeight = GetTerrainHeight(randomPosition);
                while (terrainHeight > 3f)
                {
                    randomPosition = GetRandomPosition();
                    terrainHeight = GetTerrainHeight(randomPosition);
                }
                Vector3 newPosition = new Vector3(randomPosition.x, terrainHeight, randomPosition.z);
                Instantiate(zombie, newPosition, Quaternion.identity);
                yield return new WaitForSeconds(2);
            }
        }
    }
    
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            Vector3 randomPosition = GetRandomPosition();
            float terrainHeight = GetTerrainHeight(randomPosition);
            while (terrainHeight > 3f)
            {
                randomPosition = GetRandomPosition();
                terrainHeight = GetTerrainHeight(randomPosition);
            }
            Vector3 position = new Vector3(randomPosition.x, terrainHeight, randomPosition.z);

            StartCoroutine(SpawnZombies(position));

            if (interval <= minTime)
            {
                i++;
                if (startTime - i > minTime)
                {
                    interval = startTime - i;
                }
            }
            else
            {
                interval--;
            }
        }
    }
}