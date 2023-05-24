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

    //Jacky for UI panel
    public GameObject winPanel;
    public GameObject losePanel;

    //for player death animation
    public GameObject player;//added



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
        cameraFollow.SetUp(() => 
        {
            if(playerTransform == null || !playerTransform.gameObject.activeInHierarchy)
            {
                return Vector3.zero;
            }
            return playerTransform.position;
        });
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
        losePanel.SetActive(true);//lose panel
    }

    public void Win()
    {
        if(isGameOver)
        {
            return;
        }

        isGameOver = true;
        Debug.Log("Win!");
        Time.timeScale = 0f;
        winPanel.SetActive(true);//win panel jacky
         PauseGame();//jacky
    }

    public void PlayerLost()
    {
        if(isGameOver)
        {
            return;
        }

        isGameOver = true;
        Debug.Log("Lose!");
        Time.timeScale = 0f;
         losePanel.SetActive(true);//lose panel jacky
         PauseGame();//jacky
    }

    //Jacky for Pause and Resume game when the win and lose panel showing
    public void PauseGame()
    {
       Time.timeScale = 0;
    //    Player player = FindObjectOfType<Player>();
    //    if (player != null)
    //    {
    //     player.isGamePaused = true;
    //    }
    }

    public void ResumeGame()
    {
       Time.timeScale = 1;
    //    Player player = FindObjectOfType<Player>();
    //    if (player != null)
    //    {
    //     player.isGamePaused = false;
    //    }
    }

}
