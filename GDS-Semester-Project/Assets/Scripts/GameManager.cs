using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    private int enemiesLeft;
    private bool hasWon = false;
    private EnemyGenerator enemyGenerator;
    private Player player;
    private bool hasLost = false;
    private bool isGameOver = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }


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

    public void GameOver()
    {
        if(isGameOver)
        {
            return;
        }

        isGameOver = true;
        Debug.Log("Lose!");
        Time.timeScale = 0f;
    }
}
