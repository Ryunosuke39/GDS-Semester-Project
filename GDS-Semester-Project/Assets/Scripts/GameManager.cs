using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int enemiesLeft;
    private bool hasWon = false;
    private EnemyGenerator enemyGenerator;
    private Player player;
    private bool hasLost = false;

    private void Awake()
    {
        enemyGenerator = FindObjectOfType<EnemyGenerator>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (!hasWon && enemiesLeft == 0 && enemyGenerator.enemiesGenerated >= enemyGenerator.totalEnemies)
        {
            Debug.Log("Win!");
            hasWon = true;
        }

        if(!hasLost && player == null)
        {
            Debug.Log("Lose!");
            hasLost = true;
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
