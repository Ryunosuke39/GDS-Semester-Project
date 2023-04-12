using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //camera follow function added
    public CameraFollow cameraFollow;
    public Transform playerTransform;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //added for camera follow 
        cameraFollow.SetUp(() => playerTransform.position);
    }

    private void Update()
    {
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

    public void TimeIsUp()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;
        Debug.Log("Lose!");
        Time.timeScale = 0f;
    }
}
