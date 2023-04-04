using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject[] spawnPoints;
    public float spawnInterval = 1f;
    public int totalEnemies = 100;
    public int enemiesGenerated { get; private set; } = 0;

    

    private void Start()
    {
        
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (enemiesGenerated < totalEnemies)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (enemiesGenerated >= totalEnemies)
                {
                    break;
                }

                GameObject enemyToSpawn = enemiesGenerated % 2 == 0 ? enemy1 : enemy2;
                Instantiate(enemyToSpawn, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
                enemiesGenerated++;

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}
