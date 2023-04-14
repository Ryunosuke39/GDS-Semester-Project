using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemSpawnerController : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public float spawnInterval = 6f;
    public Vector2 spawnRange;

    private List<GameObject> pooledItems = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
{
    while (true)
    {
        yield return new WaitForSeconds(spawnInterval);

        int randomIndex = Random.Range(0, itemPrefabs.Length);
        Vector2 spawnPosition = new Vector2(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y));

        GameObject itemObject = null;

        if (pooledItems.Count > 0)
        {
            itemObject = pooledItems[0];
            pooledItems.RemoveAt(0);

            // Check if the item object is null before activating it
            if (itemObject != null)
            {
                itemObject.SetActive(true);
            }
        }
        else
        {
            // Check if the item prefab is null before instantiating it
            if (itemPrefabs[randomIndex] != null)
            {
                itemObject = Instantiate(itemPrefabs[randomIndex], spawnPosition, Quaternion.identity);
            }
        }

        // Check if the item object is null before setting its position
        if (itemObject != null)
        {
            itemObject.transform.position = spawnPosition;
        }
    }
}

}



