using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour // from Jacky for win and lose UI panel
{
    public void RestartGame()
    {
        GameManager.Instance.ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void LoadLevelMenu()
    {
        SceneManager.LoadScene(0); 
    }



}
