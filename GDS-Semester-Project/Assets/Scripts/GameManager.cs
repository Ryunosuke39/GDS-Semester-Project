using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int enemiesLeft;
    private bool hasWon = false;
    private EnemyGenerator enemyGenerator;

    private void Awake()
    {
        enemyGenerator = FindObjectOfType<EnemyGenerator>();
    }

    private void Update()
    {
        enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (!hasWon && enemiesLeft == 0 && enemyGenerator.enemiesGenerated >= enemyGenerator.totalEnemies)
        {
            Debug.Log("Win!");
            hasWon = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
