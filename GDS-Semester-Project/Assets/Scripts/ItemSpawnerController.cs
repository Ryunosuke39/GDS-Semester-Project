using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerController : MonoBehaviour
{
    public GameObject[] itemPrefabs; // 存放三种道具的预制体数组
    public float spawnInterval = 6f; // 道具生成间隔时间，单位：秒
    public Vector2 spawnRange; // 地图的道具生成范围
    

    private void Start()
    {
    //     foreach (GameObject itemPrefab in itemPrefabs)
    // {
    //     itemPrefab.SetActive(false);
    // }
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            int randomIndex = Random.Range(0, itemPrefabs.Length);
            Vector2 spawnPosition = new Vector2(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y));
           // GameObject itemObject = null;
            Instantiate(itemPrefabs[randomIndex], spawnPosition, Quaternion.identity);
             //itemObject.SetActive(true);
        }
    }
}
