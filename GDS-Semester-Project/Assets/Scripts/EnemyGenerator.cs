using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject[] spawnPoints;
    public float spawnInterval = 1f;
    public int totalEnemies = 100;

    private int spawnedEnemies = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (spawnedEnemies < totalEnemies)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnedEnemies >= totalEnemies)
                {
                    break;
                }

                GameObject enemyToSpawn = spawnedEnemies % 2 == 0 ? enemy1 : enemy2;
                Instantiate(enemyToSpawn, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
                spawnedEnemies++;

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}
